import React from "react";
import PropTypes from "prop-types";
import { NavLink } from "react-router-dom";

const ItemNameLink = ({ children, path }) => (
  <NavLink to={path} className="nav-link text-dark">
    <h6 className="text-center">{children}</h6>
  </NavLink>
);

ItemNameLink.propTypes = {
  children: PropTypes.any.isRequired,
  path: PropTypes.string,
};

ItemNameLink.defaultProps = {
  path: '#'
}

export default ItemNameLink;
