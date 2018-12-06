import React from "react";
import PropTypes from "prop-types";

const SuccessAlert = ({children}) => (
  <div className="alert alert-success text-center" role="alert">
    {children}
  </div>
);

SuccessAlert.propTypes = {
    children: PropTypes.string.isRequired
}

export default SuccessAlert;
