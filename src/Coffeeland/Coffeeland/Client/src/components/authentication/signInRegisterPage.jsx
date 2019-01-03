import React, { Component } from "react";
import SignIn from "./signIn";
import Register from "./register";
import { SUCCESSFUL_REGISTRATION, FAILURE_SIGN_IN} from "../../constants/messages";
import { SuccessAlert, DangerAlert } from "./../alert";
import { withRedirect } from "./../../with/withRedirect";
import { SHOP } from "../../constants/paths";
import PropTypes from "prop-types";
import { connect } from "react-redux";

class SignInRegisterPage extends Component {
  state = {
    shouldSuccessfulRegistrationMessageBeDisplayed: false,
    shouldFailureSignInMessageBeDisplayed: false,
  };

  render() {
    const {
      shouldSuccessfulRegistrationMessageBeDisplayed,
      shouldFailureSignInMessageBeDisplayed,
    } = this.state;

    const {
      isSignIn,
      style
    } = this.props;

    return (
      <div className="row mx-auto" style={style}>
        {shouldSuccessfulRegistrationMessageBeDisplayed && (
          <div className="col-12 mt-4">
            <SuccessAlert>{SUCCESSFUL_REGISTRATION}</SuccessAlert>{" "}
          </div>
        )}

        {shouldFailureSignInMessageBeDisplayed && (
          <div className="col-12 mt-4">
            <DangerAlert>{FAILURE_SIGN_IN}</DangerAlert>{" "}
          </div>
        )}

        <div className="col-6">
          <SignInWithRedirect
            shouldRedirect={isSignIn}
            to={SHOP}
            displayFailureSignInMassage={() =>
              this.displayMessage("shouldFailureSignInMessageBeDisplayed")
            }
          />
        </div>
        <div className="col-6">
          <Register
            displaySuccessfulRegistationMassage={() =>
              this.displayMessage(
                "shouldSuccessfulRegistrationMessageBeDisplayed"
              )
            }
          />
        </div>
      </div>
    );
  }

  displayMessage = displayConditionName => {
    this.setState({ [displayConditionName]: true });
    window.scrollTo(0, 0);

    setTimeout(() => {
      this.setState({ [displayConditionName]: false });
    }, 4000);
  };

  getMessageClassName = color => {
    return "col-12 pt-2 pb-2 mt-3 text-white text-center " + color;
  };
}

const SignInWithRedirect = withRedirect(SignIn);

SignInRegisterPage.propTypes = {
  isSignIn: PropTypes.bool.isRequired
};

const mapStateToProps = state => ({
  isSignIn: state.token.token.isSignIn
});

export default connect(
  mapStateToProps,
  {}
)( SignInRegisterPage );