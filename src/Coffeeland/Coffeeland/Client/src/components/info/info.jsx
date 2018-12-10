import React from 'react';
import PropTypes from 'prop-types';

const Info = ({children, className}) => (<p className={className}>{children}</p>);

Info.propTypes = {
    children: PropTypes.any.isRequired,
    className: PropTypes.string,
}

Info.defaultProps = {
    className: "pt-2"
}
export default Info;