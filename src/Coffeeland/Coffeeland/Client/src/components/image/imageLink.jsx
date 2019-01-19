import React from "react";
import {NavLink} from 'react-router-dom';
import Img from "react-image";
import PropTypes from "prop-types";


const ImageLink = ({src, path}) => (
  <figure>
    <NavLink to={path}>
        <Img className="img-fluid" src={require('./coffee.jpg')} />
    </NavLink>
  </figure>
);

ImageLink.propTypes = {
  src: PropTypes.string.isRequired,
  path: PropTypes.string,
}

ImageLink.defaultProps = {
  path: "#"
}

export default ImageLink;
