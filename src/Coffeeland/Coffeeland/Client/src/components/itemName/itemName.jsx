import React from "react";
import PropTypes from "prop-types";

const ItemName = ({ children }) => (
    <h6 className="text-center">{children}</h6>
);

ItemName.propTypes = {
  children: PropTypes.any.isRequired,
};

export default ItemName;
