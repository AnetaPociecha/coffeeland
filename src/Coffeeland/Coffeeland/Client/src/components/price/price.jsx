import React from "react";
import PropTypes from "prop-types";
import { getTotalPrice } from './../../helpers/priceHelper';

const Price = ({ price, quantity, className }) => {
  return <p className={className}>$ {getTotalPrice(price, quantity)}</p>;
}

Price.propTypes = {
  price: PropTypes.any.isRequired,
  quantity: PropTypes.number,
  className: PropTypes.string,
};

Price.defaultProps = {
  quantity: 1,
  className: "text-center",
};

export default Price;
