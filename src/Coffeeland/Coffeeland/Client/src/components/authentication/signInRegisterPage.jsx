import React, { Component } from "react";
import SignIn from "./signIn";
import Register from "./register";
import { SUCCESSFUL_REGISTRATION, FAILURE_SIGN_IN} from "../../constants/messages";
import { SuccessAlert, DangerAlert } from "./../alert";
import { withRedirect } from "./../../with/withRedirect";
import { SHOP } from "../../constants/paths";

class SignInRegisterPage extends Component {
  state = {
    shouldSuccessfulRegistrationMessageBeDisplayed: false,
    shouldFailureSignInMessageBeDisplayed: false
  };

  render() {
    const {
      shouldSuccessfulRegistrationMessageBeDisplayed,
      shouldFailureSignInMessageBeDisplayed
    } = this.state;

    const {
      handleSignIn, 
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
            handleSignIn={handleSignIn}
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

export default SignInRegisterPage;
