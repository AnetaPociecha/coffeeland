import React, { Component } from "react";
import { Button } from "./../button";
import FormGroup from "../formGroup";
import { OrderTitle } from "../orderTitle";
import {
  EMAIL,
  FIRST_NAME,
  LAST_NAME,
  CANCEL,
  EDIT,
  SAVE,
  PERSONAL_DATA
} from "../../constants/titles";
import { isNameValid, isEmailValid } from "./../../isValid";
import {
  INVALID_EMAIL,
  INVALID_FIRST_NAME,
  INVALID_LAST_NAME
} from "../../constants/messages";
import {
  updatePersonalData
} from "./../../actions/personalDataActions";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import MessageProcessor from "./../../messageProcessor/messageProcessor";
const mp = MessageProcessor.getInstance();

class Profile extends Component {
  state = {
    isEditMode: false,
    firstName: "",
    lastName: "",
    email: "",
    firstNameCopy: "",
    lastNameCopy: "",
    emailCopy: ""
  };

  componentWillReceiveProps(nextProps) {
    if (nextProps.personalData) {
      this.setState({
        firstName: nextProps.personalData.firstName,
        lastName: nextProps.personalData.lastName,
        email: nextProps.personalData.email,
        firstNameCopy: nextProps.personalData.firstName,
        lastNameCopy: nextProps.personalData.lastName,
        emailCopy: nextProps.personalData.email
      });
    }
  }

  render() {
    const { personalData } = this.props;
    const { isEditMode, firstName, lastName, email } = this.state;
    return (
      <div>
        <div className="col-12 pb-4">
          <OrderTitle>{PERSONAL_DATA}</OrderTitle>
        </div>

        {isEditMode ? (
          
          <div className=" col-12">
          
          <div className="row p-2 pt-3 border">
            <FormGroup
              id={"firstName"}
              title={FIRST_NAME}
              className={this.getInputClass(isNameValid, firstName)}
              invalidInputMessage={INVALID_FIRST_NAME}
              handleInputChange={this.onChange}
              colLength={6}
              value={firstName}
            />

            <FormGroup
              id="lastName"
              title={LAST_NAME}
              className={this.getInputClass(isNameValid, lastName)}
              invalidInputMessage={INVALID_LAST_NAME}
              handleInputChange={this.onChange}
              colLength={6}
              value={lastName}
            />

            <FormGroup
              id="email"
              title={EMAIL}
              className={this.getInputClass(isEmailValid, email)}
              invalidInputMessage={INVALID_EMAIL}
              handleInputChange={this.onChange}
              colLength={12}
              value={email}
            />
            </div>
            <div className="row mt-3">
            
            <div className="col-6 pt-3">
              <Button onClick={this.onSave} disabled={this.isSaveDisabled()}>
                {SAVE}
              </Button>
            </div>
            <div className="col-6 text-right pt-3">
              <Button onClick={this.onCancel}>{CANCEL}</Button>
            </div>

            </div>
          </div>
        ) : (
          <div className="row">
            <div className="col-12 border">
            <div className="col-12 pt-3 pl-3 pr-3">
              {personalData.firstName} {personalData.lastName}
            </div>
            <div className="col-12 p-3">{personalData.email}</div>
            </div>

            <div className="col-12 text-right p-3 mt-3">
              <Button onClick={this.onEdit}>{EDIT}</Button>
            </div>
          </div>
        )}
      </div>
    );
  }

  isSaveDisabled = () => (
     !(
      isNameValid(this.state.firstName) &&
      isNameValid(this.state.lastName) &&
      isEmailValid(this.state.email)
    )
  );

  onChange = event => {
    const target = event.target;
    const value = target.value;
    const name = target.name;
    this.setState({
      [name]: value
    });
  };

  getInputClass = (isValid, str) =>
    "form-control" + (isValid(str) ? "" : " is-invalid");

  onEdit = () => {
    this.setState(prevState => ({
      isEditMode: true,
      firstNameCopy: prevState.firstName,
      lastNameCopy: prevState.lastName,
      emailCopy: prevState.email
    }));
  };

  onCancel = () => {
    this.setState(prevState => ({
      isEditMode: false,
      firstName: prevState.firstNameCopy,
      lastName: prevState.lastNameCopy,
      email: prevState.emailCopy
    }));
  };

  onSave = () => {
    this.setState({
      isEditMode: false
    });
    const rq = {
      $type: "UpdatePersonalDataCommand",
      sessionToken: this.props.token,
      email: this.state.email,
      firstName: this.state.firstName,
      lastName: this.state.lastName,
      changePassword: false,
      newPassword: '',
      receiveNewsletterEmail: this.props.receiveNewsletterEmail,
      newsletterEmail: this.props.newsletterEmail
    }
    mp.processCommand(rq).then(rs => {
      if(rs.isSuccess) {
        this.props.onPersonalDataChange(rs);
      } else {
          this.displayFailureMessage()
      }     
    })
  }
  
  // TO DO
  displayFailureMessage = () => { 
    console.log("UpdatePersonalDataCommand fail")
  }
}

Profile.propTypes = {
  token: PropTypes.string.isRequired,
  updatePersonalData: PropTypes.func.isRequired,
  newsletterEmail: PropTypes.string.isRequired,
  receiveNewsletterEmail: PropTypes.bool.isRequired,
};

const mapStateToProps = state => ({
  token: state.token.token.token,
  newsletterEmail: state.personalData.personalData.newsletterEmail,
  receiveNewsletterEmail: state.personalData.personalData.receiveNewsletterEmail,
});

export default connect(
  mapStateToProps,
  { updatePersonalData }
)( Profile );
