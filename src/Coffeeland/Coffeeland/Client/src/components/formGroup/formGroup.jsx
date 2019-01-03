import React from "react";
import classNames from "classnames";
import PropTypes from "prop-types";

const FormGroup = ({
  id,
  title,
  className,
  invalidInputMessage,
  handleInputChange,
  colLength,
  value,
  type
}) => {
  const col = "col-" + colLength;
  const formClass = classNames("form-group", { [col]: !!colLength });
  return (
    <div className={formClass}>
      <label htmlFor={id}>{title}</label>
      <input
        className={className}
        id={id}
        name={id}
        type={type}
        placeholder={title}
        value={value}
        onChange={handleInputChange}
        required
      />
      <div className="invalid-feedback">{invalidInputMessage}</div>
    </div>
  );
};

FormGroup.propTypes = {
  id: PropTypes.string.isRequired,
  title: PropTypes.string.isRequired,
  className: PropTypes.string,
  invalidInputMessage: PropTypes.string.isRequired,
  handleInputChange: PropTypes.func.isRequired,
  colLength: PropTypes.number,
  value: PropTypes.string.isRequired,
  type: PropTypes.string,
}

FormGroup.defaultProps = {
  colLength: 12,
  type: "text",
  className: ''
}

export default FormGroup;
