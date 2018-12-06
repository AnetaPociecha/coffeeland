import React from "react";

const FormCheck = ({
  checked,
  id,
  className,
  handleInputChange,
  title,
  invalidInputMessage
}) => (
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
      {title}
    </label>
    {invalidInputMessage && (
      <div className="invalid-feedback">{invalidInputMessage}</div>
    )}
  </div>
);
export default FormCheck;
