import React from 'react';
import PropTypes from 'prop-types';
import './../../style.css'

const OrderTitle = ({children, textAlign}) => (
    <h4 className={`title m-2 text-${textAlign}`}>{children}</h4>
);

OrderTitle.propTypes = {
    textAlign: PropTypes.string
}
OrderTitle.defaultProps = {
    textAlign: 'center'
}

export default OrderTitle;