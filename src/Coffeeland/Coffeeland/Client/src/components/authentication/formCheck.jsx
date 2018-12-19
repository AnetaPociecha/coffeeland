import React, { Component } from 'react'
import TermModal from './termsModal';
import {Link} from 'react-router-dom';

export default class FormCheck extends Component {
  
  state = {isModalActive: false}

  render() {
    const {
      checked,
      id,
      className,
      handleInputChange,
      title,
      invalidInputMessage
    } = this.props

    return (
      <div className="form-check col-12 ml-3">
        <input
          className={className}
          type="checkbox"
          checked={checked}
          id={id}
          name={id}
          onChange={handleInputChange}
          required
        />
        <label className="form-check-label" htmlFor={id}>
          <Link className="text-dark" to="#" onClick={this.onModalOpen}>{title}</Link>
        </label>
        {invalidInputMessage && (
          <div className="invalid-feedback">{invalidInputMessage}</div>
        )}
        <TermModal isActive={this.state.isModalActive} onModalClose={this.onModalClose} />
    </div>
    )
  }

  onModalClose = () => {
    this.setState({ isModalActive: false });
  }
  onModalOpen = () => {
    this.setState({ isModalActive: true });
  }

}
