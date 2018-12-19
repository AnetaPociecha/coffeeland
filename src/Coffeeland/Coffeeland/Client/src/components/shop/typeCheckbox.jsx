import React, { Component } from "react";

class TypeCheckbox extends Component {
  state = {
    checked: false
  };
  render() {
    const { type } = this.props;
    const { checked } = this.state;
    return (
      <div className="col-12">
        <div className="form-check p-2">
          <input
            className="form-check-input"
            type="checkbox"
            checked={checked}
            id={type}
            name={type}
            onChange={this.handleCheckboxChange}
            required
          />
          <label className="form-check-label text-muted" htmlFor={type}>
            {type}
          </label>
        </div>
      </div>
    );
  }

  handleCheckboxChange = () => {
    this.props.onCheckboxChange(this.props.type, !this.state.checked);
    this.setState(prevState => ({ checked: !prevState.checked }));
  };
}

export default TypeCheckbox;
