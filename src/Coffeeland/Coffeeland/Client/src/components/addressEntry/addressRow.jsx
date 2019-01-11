import React from "react";
import { getZIPCode } from "../../helpers/addressHelper";

export default function AddressRow({ address }) {
  const {
    country,
    city,
    ZIPCode,
    street,
    buildingNumber,
    appartmentNumber
  } = address;
  return (
    <div className="col-12">
      <div className="row">
        <div className="p-1">{country}</div>
        <div className="p-1">{city}</div>
        <div className="p-1">{getZIPCode(ZIPCode)}</div>
        <div className="p-1">{street}</div>
        <div className="p-1">{buildingNumber}</div>
        {appartmentNumber && <div className="p-1">/ {appartmentNumber}</div>}
      </div>
    </div>
  );
}
