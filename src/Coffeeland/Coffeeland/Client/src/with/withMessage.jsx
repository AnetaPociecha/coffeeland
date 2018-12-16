import React from "react";
import { SecondaryAlert } from "./../components/alert";

const withMessage = Component => {
  const { shouldDisplayMsg, msg } = this.props

  return () =>
    shouldDisplayMsg ? (
      <div className="pt-5 col-12">
        <SecondaryAlert>{msg}</SecondaryAlert>
      </div>
    ) : (
      React.createElement(Component, Object.assign({}, this.props, {}))
    );
};

export { withMessage };
