import React from "react";
import Img from "react-image";
import PropTypes from "prop-types";


const Image = ({src}) => (
  <figure>
        <Img className="img-fluid" src={require('./coffee.jpg')} />
  </figure>
);

Image.propTypes = {
  src: PropTypes.string.isRequired,
}

export default Image;
