import React, { Component } from "react";
import { Redirect } from "react-router";
import { SHOP } from "../../constants/paths";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import {
  fetchPersonalData,
  updatePersonalData
} from "./../../actions/personalDataActions";
import { Nav } from "./nav";
import Profile from "./profile";
import Orders from "./orders";
import AddressBook from "./addressBook";
import Newsletter from "./newsletter";


class MyAccount extends Component {
  state = {
    mode: "PROFILE"
  };

  componentWillMount() {
    this.props.fetchPersonalData();
  }

  render() {
    const { isSignIn } = this.props;
    const { mode } = this.state;
    return !isSignIn ? (
      <Redirect to={SHOP} />
    ) : (
      <div className="row mx-auto mt-5" style={this.props.style}>
        <div className="col-md-3 col-sm-12">
          <Nav setMode={this.setMode} mode={mode} />
        </div>
        <div className="col-md-9 col-sm-12">
          <div className="tab-content" id="v-pills-tabContent">
            <div
              className={this.getTabClass("PROFILE")}
              id="profile"
              role="tabpanel"
              aria-labelledby="profile"
            >
              <Profile
                personalData={this.props.personalData}
                onPersonalDataChange={this.onPersonalDataChange}
              />
            </div>
            <div
              className={this.getTabClass("ORDERS")}
              id="orders"
              role="tabpanel"
              aria-labelledby="orders"
            >
              <Orders orders={this.props.personalData.orders} />
            </div>
            <div
              className={this.getTabClass("ADDRESSBOOK")}
              id="addressbook"
              role="tabpanel"
              aria-labelledby="addressbook"
            >
              <AddressBook />
            </div>
            <div
              className={this.getTabClass("NEWSLETTER")}
              id="newsletter"
              role="tabpanel"
              aria-labelledby="newsletter"
            >
              <Newsletter receiveNewsletterEmail={this.props.personalData.receiveNewsletterEmail} /> 
            </div>
          </div>
        </div>
      </div>
    );
  }

  setMode = mode => {
    this.setState({ mode });
  };

  onPersonalDataChange = obj => {
    this.props.updatePersonalData(obj);
  };

  getTabClass = mode =>
    "tab-pane fade" + (mode === this.state.mode ? " show active" : "");
}

MyAccount.propTypes = {
  fetchPersonalData: PropTypes.func.isRequired,
  updatePersonalData: PropTypes.func.isRequired,
  personalData: PropTypes.object.isRequired
};

const mapStateToProps = state => ({
  personalData: state.personalData.personalData
});

export default connect(
  mapStateToProps,
  { fetchPersonalData, updatePersonalData }
)(MyAccount);
