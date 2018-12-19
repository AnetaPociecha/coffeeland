import React, { Component } from "react";
import * as message from "../../constants/messages";
import * as title from "../../constants/titles";
import FormGroup from "../formGroup/formGroup";
import FormRow from "./formRow";
import FormCheck from "./formCheck";
import FormTitle from "./formTitle";
import { Button } from "./../button";
import {isRegisterFormValid, isPasswordValid, isRepeatedPasswordValid, isNameValid, isEmailValid} from './../../isValid'


const firstNameId = "firstName";
const lastNameId = "lastName";
const registerEmailId = "registerEmail";
const registerPasswordId = "registerPassword";
const repeatedPasswordId = "repeatedPassword";
const checkboxId = "checked";

class Register extends Component {
  constructor(props) {
    super(props);
    this.state = this.getInitialState();
  }

  render() {
    const {
      firstName,
      lastName,
      registerEmail,
      registerPassword,
      repeatedPassword,
      checked
    } = this.state;

    return (
      <div className="pt-5 pr-5 pl-2 pb-5">
        <FormTitle>{title.REGISTER}</FormTitle>

        <form className="register-form">
          <FormRow>
            <FormGroup
              id={firstNameId}
              title={title.FIRST_NAME}
              className={this.getTextInputClass(() => isNameValid(firstName))}
              invalidInputMessage={message.INVALID_FIRST_NAME}
              handleInputChange={this.handleInputChange}
              colLength={6}
              value={firstName}
            />
            <FormGroup
              id={lastNameId}
              title={title.LAST_NAME}
              className={this.getTextInputClass(() => isNameValid(lastName))}
              invalidInputMessage={message.INVALID_LAST_NAME}
              handleInputChange={this.handleInputChange}
              colLength={6}
              value={lastName}
            />
          </FormRow>

          <FormRow>
            <FormGroup
              id={registerEmailId}
              title={title.EMAIL}
              className={this.getTextInputClass(this.isEmailAvailable)}
              invalidInputMessage={this.getInvalidEmailMessage()}
              handleInputChange={this.handleInputChange}
              colLength={12}
              value={registerEmail}
            />
          </FormRow>

          <FormRow>
            <FormGroup
              id={registerPasswordId}
              title={title.PASSWORD}
              className={this.getTextInputClass(() => isPasswordValid(registerPassword))}
              invalidInputMessage={message.INVALID_PASSWORD}
              handleInputChange={this.handleInputChange}
              colLength={6}
              value={registerPassword}
              type="password"
            />
            <FormGroup
              id={repeatedPasswordId}
              title={title.REPEAT_PASSWORD}
              className={this.getTextInputClass(() => isRepeatedPasswordValid(registerPassword, repeatedPassword))}
              invalidInputMessage={message.INVALID_REPEATED_PASSWORD}
              handleInputChange={this.handleInputChange}
              colLength={6}
              value={repeatedPassword}
              type="password"
            />
          </FormRow>

          <FormRow>
            <FormCheck
              checked={checked}
              id={checkboxId}
              className={this.getCheckboxInputClassName()}
              handleInputChange={this.handleInputChange}
              title={title.AGREE_TO_TERMS}
              invalidInputMessage={message.INVALID_TERMS_AGREMENT}
            />
          </FormRow>

          <Button onClick={this.handleRegister}>{title.REGISTER}</Button>
        </form>
      </div>
    );
  }

  handleRegister = () => {
    this.setState({ isFormValidated: true });
    if (this.isFormValid()) {
      var isRqSuccessful = true;
      this.updateAppStateAfterRegister(isRqSuccessful);
    }
  };

  handleInputChange = event => {
    const target = event.target;
    const value = target.type === "checkbox" ? target.checked : target.value;
    const name = target.name;

    this.setState({
      [name]: value
    });

    if (name === registerEmailId && this.state.isEmailTaken)
      this.setState({ isEmailTaken: false });
  };

  isEmailAvailable = () => {
    return isEmailValid(this.state.registerEmail) && !this.state.isEmailTaken;
  };

  isFormValid = () => 
    isRegisterFormValid(this.state.firstName, this.state.lastName, this.state.registerEmail, this.state.registerPassword, this.state.repeatedPassword, this.state.checked)

  send = () => {
    // rq
    // rs
    return this.state.registerEmail !== "apociecha@interia.pl";
  };

  updateAppStateAfterRegister = isRqSuccessful => {
    if (isRqSuccessful) {
      this.setState(this.getInitialState());
      this.props.displaySuccessfulRegistationMassage();
    } else {
      this.setState({ isEmailTaken: true });
    }
  };

  getInitialState = () => ({
    isFormValidated: false,
    firstName: "",
    lastName: "",
    registerEmail: "",
    registerPassword: "",
    repeatedPassword: "",
    checked: false,
    isEmailTaken: false
  });

  shouldFieldBeValidated = validationFunction => {
    return this.state.isFormValidated && !validationFunction();
  };

  getTextInputClass = validationFunction => {
    return (
      "form-control" +
      (this.shouldFieldBeValidated(validationFunction) ? " is-invalid" : "")
    );
  };

  getCheckboxInputClassName = () => {
    return (
      "form-check-input" +
      (this.state.isFormValidated && !this.state.checked ? " is-invalid" : "")
    );
  };

  getInvalidEmailMessage = () => {
    return (
      (isEmailValid(this.state.registerEmail) ? "" : message.INVALID_EMAIL) +
      (this.state.isEmailTaken ? message.TAKEN_EMAIL : "")
    );
  };

  getFormRowClass = () => {
    return "row form-group " + this.getPB();
  };
  getPB = () => {
    return "pb-3";
  };
}

export default Register;
