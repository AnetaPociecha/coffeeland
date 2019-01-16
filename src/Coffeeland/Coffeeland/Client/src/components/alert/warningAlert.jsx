import React from "react";
import PropTypes from "prop-types";

const WarningAlert = ({children}) => (
  <div className="alert alert-warning text-center" role="alert">
    {children}
  </div>
);

WarningAlert.propTypes = {
    children: PropTypes.string.isRequired
}

export default WarningAlert;
