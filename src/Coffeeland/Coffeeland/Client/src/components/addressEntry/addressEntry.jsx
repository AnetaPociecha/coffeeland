import React from "react";
import { getZIPCode } from '../../helpers/addressHelper'

export default function AddressEntry({address}) {
    const {country, city, ZIPCode, street, buildingNumber, apartmentNumber} = address
  return (
    <div className="col-12">
      <div className="row">
        <div className="col-4 text-muted p-2 pl-4">Country:</div>
        <div className="col-4 text-muted p-2">City:</div>
        <div className="col-4 text-muted p-2 pr-4">ZIP code:</div>
        <div className="col-4 pt-1 pb-1 pr-2 pl-4">{country}</div>
        <div className="col-4 pt-1 pb-1 pr-4 pl-2">{city}</div>
        <div className="col-4 pt-1 pb-1 pr-4 pl-2">
          {getZIPCode(ZIPCode)}
        </div>
      </div>
      <div className="row">
        <div className="col-4 text-muted p-2 pl-4">Street:</div>
        <div className="col-4 text-muted p-2">Building number:</div>
        <div className="col-4 text-muted p-2 pr-4">Apartment number:</div>
        <div className="col-4 pt-1 pb-1 pr-2 pl-4">{street}</div>
        <div className="col-4 pt-1 pb-1 pr-4 pl-2">
          {buildingNumber}
        </div>
        <div className="col-4 pt-1 pb-1 pr-4 pl-2">
          {apartmentNumber}
        </div>
      </div>
    </div>
  );
}
