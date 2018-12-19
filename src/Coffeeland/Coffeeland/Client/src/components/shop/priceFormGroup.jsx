import React from "react";
import { getTotalPrice } from "./../../helpers/priceHelper";
import { PRICE_FILTER_STEP } from "../../constants/config";
import './style.css'

const PriceFormGroup = ({ id, value, onChange, label, min, max }) => (
  <div className="form-group pb-2">
    <label className="form-check-label text-muted pb-3 row" htmlFor={id}>
      <div className="small col-6"> {label}</div>{" "}
      <div className="text-right col-6">$ {getTotalPrice(value)}</div>
    </label>

    <input
      type="range"
      className="form-control-range slider"
      id={id}
      name={id}
      value={value}
      onChange={onChange}
      min={min}
      max={max}
      step={PRICE_FILTER_STEP}
    />
    <div className="row pt-2">
      <div className="text-left small col-6 text-muted">
        {getTotalPrice(min)}
      </div>
      <div className="text-right small col-6 text-muted">
        {getTotalPrice(max)}
      </div>
    </div>
  </div>
);

export default PriceFormGroup;
