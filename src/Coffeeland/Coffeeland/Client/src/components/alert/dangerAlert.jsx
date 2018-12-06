import React from "react";
import PropTypes from "prop-types";

const DangerAlert = ({children}) => (
  <div className="alert alert-danger text-center" role="alert">
    {children}
  </div>
);

DangerAlert.propTypes = {
    children: PropTypes.string.isRequired
}

export default DangerAlert;
