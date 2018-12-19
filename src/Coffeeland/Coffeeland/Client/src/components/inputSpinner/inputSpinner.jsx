import React from "react";

const InputSpinner = ({ onPlus, onMinus, value, max, min }) => (
  <div>
    <div className="input-group">
      <span className="input-group-btn">
        <button
          type="button"
          className="btn btn-outline-dark"
          onClick={onMinus}
          disabled={value < min + 1}
        >
          <span>-</span>
        </button>
      </span>

      <input
        type="text"
        className="form-control input-number"
        value={value}
        style={{ maxWidth: 45 }}
        readOnly="readonly"
      />

      <span className="input-group-btn">
        <button
          type="button"
          className="btn btn-outline-dark"
          onClick={onPlus}
          disabled={value > max - 1}
        >
          <span>+</span>
        </button>
      </span>
    </div>
  </div>
);

export default InputSpinner;
