import React, { Component } from "react";
import TextArea from "../textArea/textArea.jsx";
import {
  isComplainValid,
} from "./../../isValid";
import { getInputClass } from "./../../helpers/inputHelper";
import {
  INVALID_COMPLAIN
} from "./../../constants/messages";

export default class ComplainForm extends Component {
  render() {
    const {
      complain,
      onChange,
    } = this.props;
    return (
      <div className="p-2" /*className="row"*/>

          <TextArea
            id="complain"
            title="Complain"
            className={getInputClass(isComplainValid, complain)}
            invalidInputMessage={INVALID_COMPLAIN}
            handleInputChange={onChange}
            value={complain}

          />
        
      </div>
    );
  }
}
