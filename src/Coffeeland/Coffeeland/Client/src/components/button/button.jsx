import React from 'react';
import PropTypes from "prop-types";

const Button = ({onClick, className, disabled, children}) => (
    <button
        type="button"
        className={className}
        onClick={onClick}
        disabled={disabled}
    > 
        {children} 
    </button>
);

Button.propTypes = {
    onClick: PropTypes.func,
    className: PropTypes.string,
    children: PropTypes.node.isRequired,
    disabled: PropTypes.bool,
}

Button.defaultProps = {
    className: 'btn btn-dark',
    disabled: false
}
 
export default Button;