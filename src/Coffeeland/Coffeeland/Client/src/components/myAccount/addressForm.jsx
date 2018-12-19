import React, { Component } from "react";
import FormGroup from "../formGroup/formGroup";
import {
  isCountryValid,
  isCityValid,
  isZIPCodeValid,
  isApartmentNumberValid,
  isBuildingNumberValid,
  isStreetValid
} from "./../../isValid";
import { getInputClass } from "./../../helpers/inputHelper";
import {
  INVALID_COUNTRY,
  INVALID_CITY,
  INVALID_ZIPCODE,
  INVALID_STREET,
  INVALID_APARTMENT_NUMBER,
  INVALID_BUILDING_NUMBER
} from "./../../constants/messages";

export default class AddressForm extends Component {
  render() {
    const {
      country,
      city,
      ZIPCode,
      street,
      buildingNumber,
      apartmentNumber,
      onChange,
      isChecked
    } = this.props;
    return (
      <div>
        <div className="row">
          <FormGroup
            id="country"
            title="Country"
            className={getInputClass(isCountryValid, country, isChecked)}
            invalidInputMessage={INVALID_COUNTRY}
            handleInputChange={onChange}
            value={country}
            colLength={6}
          />
          <FormGroup
            id="city"
            title="City"
            className={getInputClass(isCityValid, city, isChecked)}
            invalidInputMessage={INVALID_CITY}
            handleInputChange={onChange}
            value={city}
            colLength={6}
          />
        </div>

        <div className="row">
          <FormGroup
            id="ZIPCode"
            title="ZIP Code"
            className={getInputClass(isZIPCodeValid, ZIPCode, isChecked)}
            invalidInputMessage={INVALID_ZIPCODE}
            handleInputChange={onChange}
            value={ZIPCode}
            colLength={6}
          />
          <FormGroup
            id="street"
            title="Street"
            className={getInputClass(isStreetValid, street, isChecked)}
            invalidInputMessage={INVALID_STREET}
            handleInputChange={onChange}
            value={street}
            colLength={6}
          />
        </div>

        <div className="row">
          <FormGroup
            id="buildingNumber"
            title="Building number"
            className={getInputClass(isBuildingNumberValid, buildingNumber, isChecked)}
            invalidInputMessage={INVALID_BUILDING_NUMBER}
            handleInputChange={onChange}
            value={buildingNumber}
            colLength={6}
          />
          <FormGroup
            id="apartmentNumber"
            title="Apartment number"
            className={getInputClass(isApartmentNumberValid, apartmentNumber, isChecked)}
            invalidInputMessage={INVALID_APARTMENT_NUMBER}
            handleInputChange={onChange}
            value={apartmentNumber}
            colLength={6}
          />
        </div>
      </div>
    );
  }
}
