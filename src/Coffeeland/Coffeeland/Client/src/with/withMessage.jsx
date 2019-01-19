import React from "react";
import { SecondaryAlert } from "./../components/alert";

const withMessage = Component => ({ shouldDisplayMsg, msg, ...props }) =>
  shouldDisplayMsg ? (
    <div className="pt-5 row">
      <SecondaryAlert>{msg}</SecondaryAlert>
    </div>
  ) : (
    <Component {...props} />
  );

  export { withMessage };
