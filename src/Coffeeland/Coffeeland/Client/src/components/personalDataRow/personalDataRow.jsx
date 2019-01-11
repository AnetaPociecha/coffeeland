import React from "react";

export default function PersonalDataRow({personalData}) {
  return (
    <div className="col-12 border">
      <div className="col-12 pt-3 pl-3 pr-3">
        {personalData.firstName} {personalData.lastName}
      </div>
      <div className="col-12 p-3">{personalData.email}</div>
    </div>
  );
}
