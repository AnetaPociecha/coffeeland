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
import {isComplainValid} from "./../../isValid";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import {
  fetchOrders
} from "./../../actions/personalDataActions";

class Orders extends Component {
  constructor(props){
    super(props);

    this.state = {editMode: null,
                  complains: null,
                  complain:""};

    this.edit = this.edit.bind(this);
    this.onChange = this.onChange.bind(this);
    this.saveComplain = this.saveComplain.bind(this);
    this.cancelComplain = this.cancelComplain.bind(this);
}

  componentWillMount() {
    this.props.fetchOrders(this.props.token)
  }
  
  render() {
    const { orders } = this.props;
    const isEditMode = this.state.editMode;
    const complain = this.state.complain;

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

		{isEditMode && isEditMode[key] ?
                      <div className="col-12 pt-2 pr-4 pl-4">

                          <ComplainForm
                            complain={complain}
                            onChange={this.onChange}
                          />

                        <div className="row">
                          <div className="col-6 text-left">
                            <Button disabled={!isComplainValid(complain)} onClick = {() => this.saveComplain(complain, key)}>Send</Button>
                          </div>
                          <div className="col-6 text-right">
                            <Button onClick = {() => this.cancelComplain(key)}>Cancel</Button>
                          </div>
                        </div>
                      </div>
                   :
                  <div className="col-12 text-center pt-3 pb-2">
                    <Button disabled={!closeDate} onClick={() => this.edit(isEditMode, key)}>Complain</Button>
                  </div>
                }
                </div>
              )
            )}
        </div>
      </div>
    );
  }
  edit(isEditMode, key){
    const editModeForKeys = this.state.editMode ==! null ? this.state.editMode : [];
    isEditMode ? isEditMode=isEditMode : isEditMode=[];
    isEditMode[key] ? isEditMode[key]=false : isEditMode[key]=true;
    this.setState({editMode: isEditMode});
    console.log("Key: "+key);
  }

  onChange = event => {
    const target = event.target;
    const value = target.value;
    const name = target.name;
    this.setState({
      [name]: value
    });
  };

  saveComplain (complain, key) {
    const complains_ = this.state.complains ? this.state.complains : [];
    this.setState({complains : {...complains_,
      [key] : complain
    }, editMode : {...this.state.editMode, [key] : false}});
  };

  cancelComplain(complain, key){
    this.setState({editMode: {...this.state.EditMode, [key]: false}});
  };

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
