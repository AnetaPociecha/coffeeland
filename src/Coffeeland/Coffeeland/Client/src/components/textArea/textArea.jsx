import React from "react";
import classNames from "classnames";
import PropTypes from "prop-types";

const TextArea = ({
  id,
  title,
  className,
  invalidInputMessage,
  handleInputChange,
  colLength,
  value,
  type
}) => {
  const formClass = classNames("form-control");
  return (
    <div /*className={formClass}*/>
      <label htmlFor={id}>{title}</label>
      <textarea
        width="100%"
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

TextArea.propTypes = {
  id: PropTypes.string.isRequired,
  title: PropTypes.string.isRequired,
  className: PropTypes.string,
  invalidInputMessage: PropTypes.string.isRequired,
  handleInputChange: PropTypes.func.isRequired,
  colLength: PropTypes.number,
  value: PropTypes.string.isRequired,
  type: PropTypes.string,
}

TextArea.defaultProps = {
  //colLength: 24,
  type: "text",
  className: ''
}

export default TextArea;
