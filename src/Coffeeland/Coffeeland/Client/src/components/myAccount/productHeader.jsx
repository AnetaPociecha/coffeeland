import React from "react";
import SectionTitle from "./../orderTitle/sectionTitle";

export default function ProductHeader() {
  return (
    <div>
      <SectionTitle>Products</SectionTitle>
      <div className="row">
        <div className="col-4 text-muted p-2 pl-4">Name:</div>
        <div className="col-4 text-muted p-2">Quantity:</div>
        <div className="col-4 text-muted p-2 pr-4">Price:</div>
      </div>
    </div>
  );
}
