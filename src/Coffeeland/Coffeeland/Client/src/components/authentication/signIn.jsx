import React, { Component } from "react";
import {SIGN_IN, EMAIL, PASSWORD} from "../../constants/titles";
import FormGroup from "../formGroup/formGroup";
import FormTitle from "./formTitle";
import { Button } from "./../button";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { signIn, signOut } from "./../../actions/signInActions";

const signInEmailId = "singInEmail";
const signInPasswordId = "signInPassword";

class SignIn extends Component {
  constructor(props) {
    super(props);
    this.state = this.getInitialState();
  }

  componentWillReceiveProps(nextProps) {
    if (nextProps.token && nextProps.token.token !== '') {
      this.updateAppStateAferSignIn(true);
    }
  }

  render() {
    console.log('token', this.props.token)
    const { singInEmail, signInPassword } = this.state;

    return (
      <div className="pt-5 pl-5 pr-2 pb-5">
        <FormTitle>{SIGN_IN}</FormTitle>
        <form>
          <FormGroup
            id={signInEmailId}
            title={EMAIL}
            className="form-control"
            handleInputChange={this.handleInputChange}
            value={singInEmail}
          />

          <FormGroup
            id={signInPasswordId}
            title={PASSWORD}
            className="form-control"
            handleInputChange={this.handleInputChange}
            value={signInPassword}
            type="password"
          />

          <Button onClick={this.onSignIn} className="btn btn-dark mt-3 ml-3">
            {SIGN_IN}
          </Button>
        </form>
      </div>
    );
  }

  handleInputChange = event => {
    const target = event.target;
    const value = target.value;
    const name = target.name;

    this.setState({
      [name]: value
    });
  };

  getInitialState = () => {
    return {
      singInEmail: "",
      signInPassword: "",
      isSignInSuccessful: false
    };
  };

  onSignIn = () => {
    this.props.signIn({email: this.state.signInEmail, password: this.state.password})
  };


  updateAppStateAferSignIn = isSignInSuccessful => {
    this.setState(this.getInitialState());
    isSignInSuccessful
      ? this.props.handleSignIn()
      : this.props.displayFailureSignInMassage();
  };
}

SignIn.propTypes = {
  signIn: PropTypes.func.isRequired,
  signOut: PropTypes.func.isRequired,
  token: PropTypes.object.isRequired
};

const mapStateToProps = state => ({
  token: state.token.token
});

export default connect(
  mapStateToProps,
  { signIn, signOut }
)( SignIn );
