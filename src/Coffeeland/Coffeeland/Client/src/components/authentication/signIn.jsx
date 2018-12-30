import React, { Component } from "react";
import {SIGN_IN, EMAIL, PASSWORD} from "../../constants/titles";
import FormGroup from "../formGroup/formGroup";
import FormTitle from "./formTitle";
import { Button } from "./../button";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { dispatchToken } from "./../../actions/signInActions";
import MessageProcessor from './../../messageProcessor/MessageProcessor'

const mp = MessageProcessor.getInstance();

const signInEmailId = "singInEmail";
const signInPasswordId = "signInPassword";

class SignIn extends Component {
  constructor(props) {
    super(props);
    this.state = this.getInitialState();
  }

  render() {
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
      signInPassword: ""
    };
  };

  onSignIn = async () => {
    const rq = {
      $type: "SignInCommand",
      email: this.state.singInEmail, 
      password: this.state.signInPassword
    }
    mp.processCommand(rq).then(rs => {
        const result = rs.isSuccess
        this.updateAppStateAferSignIn(result);
        this.props.dispatchToken(rs)
      }
     ) 
  };

  updateAppStateAferSignIn = isSignInSuccessful => {
    this.setState(this.getInitialState());
    !isSignInSuccessful && this.props.displayFailureSignInMassage();
  };
}

SignIn.propTypes = {
  dispatchToken: PropTypes.func.isRequired,
  token: PropTypes.string.isRequired
};

const mapStateToProps = state => ({
  token: state.token.token
});

export default connect(
  mapStateToProps,
  { dispatchToken }
)( SignIn );
