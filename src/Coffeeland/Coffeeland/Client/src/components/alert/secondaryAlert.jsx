import React from "react";
import PropTypes from "prop-types";

const SecondaryAlert = ({ children }) => (
  <div className="alert alert-secondary text-center col-12" role="alert">
    {children}
  </div>
);

SecondaryAlert.propTypes = {
  children: PropTypes.string.isRequired
};

export default SecondaryAlert;
