import React, { Component } from "react";
import { OrderTitle } from "../orderTitle";
import { Button } from "./../button";
import FormGroup from "../formGroup";
import { isEmailValid } from "./../../isValid";
import { INVALID_EMAIL } from "../../constants/messages";
import { getInputClass } from "./../../helpers/inputHelper";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { updatePersonalData } from "./../../actions/personalDataActions";
import {
  NEWSLETTER_MAIN_INFO,
  NEWSLETTER_SMALL_INFO
} from "./../../constants/messages";
import MessageProcessor from "./../../messageProcessor/messageProcessor";
const mp = MessageProcessor.getInstance();

class Newsletter extends Component {
  state = { newNewsletterEmail: "", isChecked: false };
  render() {
    const { newNewsletterEmail } = this.state;
    const { newsletterEmail, receiveNewsletterEmail } = this.props;

    return (
      <div className="col-12 mb-4 ">
        <OrderTitle>Newsletter</OrderTitle>
        <div className="col-12 p-3">
          <p>{NEWSLETTER_MAIN_INFO}</p>

          {receiveNewsletterEmail ? (
            <div className="row p-2 border my-auto">
              <div className="col-7 p-3">
                <div className="col-12 text-muted p-1">
                  Newsletter email:
                  <br />
                </div>
                <div className="col-12 p-1">{newsletterEmail}</div>
              </div>
              <div className="col-5 my-auto text-center">
                <Button onClick={this.newsletterSignOut}>Sign out</Button>
              </div>
            </div>
          ) : (
            <div className="row p-2 border">
              <div className="col-7 my-auto">
                <FormGroup
                  id="newNewsletterEmail"
                  title="Email"
                  className={getInputClass(
                    this.shouldEmailBeChecked,
                    newNewsletterEmail
                  )}
                  invalidInputMessage={INVALID_EMAIL}
                  handleInputChange={this.onChange}
                  colLength={12}
                  value={newNewsletterEmail}
                />
              </div>

              <div className="col-5 my-auto text-center">
                <Button
                  onClick={this.newsletterSignIn}
                  disabled={!this.shouldEmailBeChecked(newNewsletterEmail)}
                >
                  Sign in
                </Button>
              </div>
            </div>
          )}

          <div className="col-12 p-3 mt-2 small text-center">
            <p>{NEWSLETTER_SMALL_INFO}</p>
          </div>
        </div>
      </div>
    );
  }

  shouldEmailBeChecked = email => !this.state.isChecked || isEmailValid(email);

  newsletterSignIn = () => {
    const { newNewsletterEmail } = this.state;
    this.setState({ isChecked: true });

    if (isEmailValid(newNewsletterEmail)) { 
      const rq = {
        $type: "UpdatePersonalDataCommand",
        sessionToken: this.props.token,
        email: this.props.personalData.email,
        firstName: this.props.personalData.firstName,
        lastName: this.props.personalData.lastName,
        changePassword: false,
        newPassword: "",
        receiveNewsletterEmail: true,
        newsletterEmail: newNewsletterEmail
      };
      mp.processCommand(rq).then(rs => {
        if (rs.isSuccess) {
          this.props.updatePersonalData(rs);
        } else {
          this.displayFailureMessage();
        }
      });
    }
  };

  // TO DO
  displayFailureMessage = () => {
    console.log("UpdatePersonalDataCommand fail");
  };

  newsletterSignOut = () => {
    this.setState({ isChecked: false, newNewsletterEmail: "" });
    const rq = {
      $type: "UpdatePersonalDataCommand",
      sessionToken: this.props.token,
      email: this.props.personalData.email,
      firstName: this.props.personalData.firstName,
      lastName: this.props.personalData.lastName,
      changePassword: false,
      newPassword: "",
      receiveNewsletterEmail: false,
      newsletterEmail: ""
    };
    mp.processCommand(rq).then(rs => {
      console.log("UpdatePersonalDataCommand newsletter sign out rs",rs)
      if (rs.isSuccess) {
        this.props.updatePersonalData(rs);
      } else {
        this.displayFailureMessage();
      }
    });
  };

  onChange = event => {
    const target = event.target;
    const value = target.value;
    const name = target.name;
    this.setState({
      [name]: value
    });
  };
}

Newsletter.propTypes = {
  updatePersonalData: PropTypes.func.isRequired,
  newsletterEmail: PropTypes.string.isRequired,
  receiveNewsletterEmail: PropTypes.bool.isRequired
};

const mapStateToProps = state => ({
  newsletterEmail: state.personalData.personalData.newsletterEmail,
  receiveNewsletterEmail:
    state.personalData.personalData.receiveNewsletterEmail,
  token: state.token.token.token
});

export default connect(
  mapStateToProps,
  { updatePersonalData }
)(Newsletter);
