import React, { Component } from "react";
import { ORDERS } from "./../../constants/titles";
import { OrderTitle } from "../orderTitle";
import { Button } from "./../button";
import { getOrderStatus } from "./../../helpers/orderHelper";
import SectionTitle from "../orderTitle/sectionTitle";
import OrderEntries from "./orderEntries";
import AddressEntry from "./addressEntry";
import ProductHeader from "./productHeader";
import TotalPriceRow from "./totalPriceRow";
import ComplainForm from "./complainForm";
import { isComplainValid } from "./../../isValid";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { fetchOrders } from "./../../actions/personalDataActions";
import MessageProcessor from "./../../messageProcessor/messageProcessor";
const mp = MessageProcessor.getInstance();

class Orders extends Component {
  constructor(props) {
    super(props);

    this.state = {
      editMode: null,
      complains: null,
      complain: "",
      showComplainMsg: false,
      complainSucc: false
    };

    this.edit = this.edit.bind(this);
    this.onChange = this.onChange.bind(this);
    this.saveComplain = this.saveComplain.bind(this);
    this.cancelComplain = this.cancelComplain.bind(this);
  }

  componentWillMount() {
    this.props.fetchOrders(this.props.token);
  }

  render() {
    const { orders } = this.props;
    const isEditMode = this.state.editMode;
    const complain = this.state.complain;
    const showComplainMsg = this.state.showComplainMsg;
    const complainSucc = this.state.complainSucc;

    return (
      <div className="row mb-4 ">
        <div className="col-12">
          <OrderTitle>{ORDERS}</OrderTitle>
        </div>
        <div className="col-12">
          {orders &&
            orders.map(
              ({
                key,
                orderEntries,
                totalPrice,
                address,
                status,
                openDate,
                closeDate
              }) => (
                <div key={key} className="border m-3 p-3 col-12">
                  <ProductHeader />

                  <OrderEntries orderEntries={orderEntries} />

                  <TotalPriceRow totalPrice={totalPrice} />

                  <SectionTitle>Address</SectionTitle>
                  <AddressEntry address={address} />

                  <SectionTitle>Status</SectionTitle>

                  <div className="col-12 pt-2 pr-4 pl-4">
                    {getOrderStatus(status)}
                  </div>

                  <SectionTitle>Open date</SectionTitle>
                  <div className="col-12 pt-2 pr-4 pl-4"> {openDate} </div>

                  {closeDate && <SectionTitle>Close date</SectionTitle>}
                  <div className="col-12 pt-2 pr-4 pl-4"> {closeDate} </div>

                  {isEditMode && isEditMode[key] ? (
                    <div className="col-12 pt-2 pr-4 pl-4 pb-3">
                      <ComplainForm
                        complain={complain}
                        onChange={this.onChange}
                      />

                      <div className="row mb-2 pl">
                        <div className="col-6 text-left pl-4 pt-3 pb-3">
                          <Button
                            disabled={!isComplainValid(complain)}
                            onClick={() => this.saveComplain(complain, key)}
                          >
                            Send
                          </Button>
                        </div>
                        <div className="col-6 text-right pr-4 pt-3 pb-3">
                          <Button onClick={() => this.cancelComplain(key)}>
                            Cancel
                          </Button>
                        </div>
                      </div>
                    </div>
                  ) : (
                    <div className="col-12 text-center pt-3 pb-2">
                      {showComplainMsg && complainSucc && (
                        <div className="col-12 pb-3 text-center text-success text-weight-bold">
                          Complain was send. We will contact you via email.
                        </div>
                      )}

                      {showComplainMsg && !complainSucc && (
                        <div className="col-12 pb-3 text-center text-danger text-weight-bold">
                          We cannot accept your complain. We accept only one
                          complain per order. <br />
                          You can contact us via email coffeeland1234@gmail.com
                          if you have any questions.
                        </div>
                      )}

                      <Button
                        disabled={!closeDate}
                        onClick={() => this.edit(isEditMode, key)}
                      >
                        Complain
                      </Button>
                    </div>
                  )}
                </div>
              )
            )}
        </div>
      </div>
    );
  }
  edit(isEditMode, key) {
    const editModeForKeys =
      this.state.editMode == !null ? this.state.editMode : [];
    isEditMode ? (isEditMode = isEditMode) : (isEditMode = []);
    isEditMode[key] ? (isEditMode[key] = false) : (isEditMode[key] = true);
    this.setState({ editMode: isEditMode });
  }

  onChange = event => {
    const target = event.target;
    const value = target.value;
    const name = target.name;
    this.setState({
      [name]: value
    });
  };

  saveComplain(complain, key) {
    const complains_ = this.state.complains ? this.state.complains : [];
    this.setState({
      complains: { ...complains_, [key]: complain },
      editMode: { ...this.state.editMode, [key]: false }
    });

    const rq = {
      $type: "AddComplaintCommand",
      orderId: key,
      description: complain,
      sessionToken: this.props.token
    };

    mp.processCommand(rq).then(rs => {
      console.log("rs", rs);
      if (rs.isSuccess) {
        this.setState({ complainSucc: true });
        setTimeout(() => this.setState({ complainSucc: false }), 7000);
      } else {
        this.setState({ complainFail: true });
        setTimeout(() => this.setState({ complainFail: false }), 7000);
      }
      this.setState({ showComplainMsg: true });
      setTimeout(() => this.setState({ showComplainMsg: false }), 7000);
    });
  }

  cancelComplain(complain, key) {
    this.setState({ editMode: { ...this.state.EditMode, [key]: false } });
  }
}

Orders.propTypes = {
  fetchOrders: PropTypes.func.isRequired,
  token: PropTypes.string.isRequired,
  orders: PropTypes.array.isRequired
};

const mapStateToProps = state => ({
  orders: state.personalData.orders.orders,
  token: state.token.token.token
});

export default connect(
  mapStateToProps,
  { fetchOrders }
)(Orders);
