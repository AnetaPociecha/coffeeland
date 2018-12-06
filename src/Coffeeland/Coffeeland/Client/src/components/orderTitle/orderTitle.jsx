import React from 'react';
import PropTypes from 'prop-types';

const OrderTitle = ({children, textAlign}) => (
    <h3 className={`text-muted m-2 text-${textAlign}`}>{children}</h3>
);

OrderTitle.propTypes = {
    textAlign: PropTypes.string
}
OrderTitle.defaultProps = {
    textAlign: 'center'
}

export default OrderTitle;