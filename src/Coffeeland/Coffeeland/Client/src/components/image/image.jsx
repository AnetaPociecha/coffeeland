import React from "react";
import Img from "react-image";
import PropTypes from "prop-types";

/*
  change src={src} when you have url like 'https://upload.wikimedia.org/wikipedia/commons/d/de/Bananavarieties.jpg' 
  it will work
  hack :)
 */

const Image = ({src}) => (
  <figure>
        <Img className="img-fluid" src={require('./coffee.jpg')} />
  </figure>
);

Image.propTypes = {
  src: PropTypes.string.isRequired,
}

export default Image;
