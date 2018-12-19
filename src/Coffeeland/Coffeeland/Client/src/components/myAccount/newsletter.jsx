import React, { Component } from "react";
import { OrderTitle } from "../orderTitle";
import { Button } from "./../button";
import FormGroup from "../formGroup";
import { isEmailValid } from "./../../isValid";
import { INVALID_EMAIL } from "../../constants/messages";
import { getInputClass } from "./../../helpers/inputHelper";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import {
  setNewsletterEmail,
  removeNewsletterEmail
} from "./../../actions/personalDataActions";
import {
  NEWSLETTER_MAIN_INFO,
  NEWSLETTER_SMALL_INFO
} from "./../../constants/messages";

class Newsletter extends Component {
  state = { newsletterEmail: "", isChecked: false };
  render() {
    const { newsletterEmail } = this.state;
    const { receiveNewsletterEmail } = this.props;
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
                <div className="col-12 p-1">{receiveNewsletterEmail}</div>
              </div>
              <div className="col-5 my-auto text-center">
                <Button onClick={this.newsletterSignOut}>Sign out</Button>
              </div>
            </div>
          ) : (
            <div className="row p-2 border">
              <div className="col-7 my-auto">
                <FormGroup
                  id="newsletterEmail"
                  title="Email"
                  className={getInputClass(
                    this.shouldEmailBeChecked,
                    newsletterEmail
                  )}
                  invalidInputMessage={INVALID_EMAIL}
                  handleInputChange={this.onChange}
                  colLength={12}
                  value={newsletterEmail}
                />
              </div>

              <div className="col-5 my-auto text-center">
                <Button onClick={this.newsletterSignIn} disabled={!this.shouldEmailBeChecked(newsletterEmail)}>Sign in</Button>
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
    const { newsletterEmail } = this.state;
    this.setState({ isChecked: true });
    isEmailValid(newsletterEmail) &&
      this.props.setNewsletterEmail({
        receiveNewsletterEmail: newsletterEmail
      });
  };

  newsletterSignOut = () => {
    this.setState({ isChecked: false, newsletterEmail: "" });
    this.props.removeNewsletterEmail();
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
  setNewsletterEmail: PropTypes.func.isRequired,
  removeNewsletterEmail: PropTypes.func.isRequired
};

const mapStateToProps = state => ({});

export default connect(
  mapStateToProps,
  { setNewsletterEmail, removeNewsletterEmail }
)(Newsletter);
