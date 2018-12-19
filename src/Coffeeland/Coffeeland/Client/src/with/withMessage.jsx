import React from "react";
import { SecondaryAlert } from "./../components/alert";

const withMessage = Component => ({ shouldDisplayMsg, msg, ...props }) =>
  shouldDisplayMsg ? (
    <div className="pt-5 col-12">
      <SecondaryAlert>{msg}</SecondaryAlert>
    </div>
  ) : (
    <Component {...props} />
  );

  export { withMessage };
