import React from 'react';
import PropTypes from 'prop-types';

const Info = ({children, className}) => (<div className={className}>{children}</div>);

Info.propTypes = {
    children: PropTypes.any.isRequired,
    className: PropTypes.string,
}

Info.defaultProps = {
    className: "pt-2"
}
export default Info;