import React from "react";
import PropTypes from 'prop-types';

const CloseButton = ({className, onClick}) => (
  <button type="button" className={className} onClick={onClick} aria-label="Close">
    <div aria-hidden="true">&times;</div>
  </button>
);

CloseButton.propTypes = {
    className: PropTypes.string,
    onClick: PropTypes.func
}

CloseButton.defaultProps = {
    className: "close mr-4 mb-1"
}

export default CloseButton;