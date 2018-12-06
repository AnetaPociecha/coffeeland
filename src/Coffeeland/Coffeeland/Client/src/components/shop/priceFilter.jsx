import React from "react";
import PriceFormGroup from "./priceFormGroup";
import { MIN_PRICE, MAX_PRICE } from "../../constants/titles";



const PriceFilter = ({
  min,
  max,
  selectedMax,
  selectedMin,
  onPriceChange,
  minId,
  maxId
}) => (
  <form>
    <PriceFormGroup
      id={minId}
      value={selectedMin}
      onChange={onPriceChange}
      label={MIN_PRICE}
      min={min}
      max={selectedMax}
    />

    <PriceFormGroup
      id={maxId}
      value={selectedMax}
      onChange={onPriceChange}
      label={MAX_PRICE}
      min={selectedMin}
      max={max}
    />
  </form>
);

export default PriceFilter;
