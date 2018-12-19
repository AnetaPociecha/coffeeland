import React from 'react';
import PropTypes from 'prop-types';
import './../../style.css'

const FilterTitle = ({children, className}) => (<p className={"lead title "+className}>{children}</p>)

FilterTitle.propTypes = {
    children: PropTypes.any.isRequired,
    className: PropTypes.string,
}

FilterTitle.defaultProps = {
    className: ''
}

export default FilterTitle;