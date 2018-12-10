import React from "react";
import { Link } from "react-router-dom";
import {
  PROFILE,
  ORDERS,
  ADDRESS_BOOK,
  NEWSLETTER
} from "../../constants/titles";

const Nav = ({ setMode, mode }) => (
    <div
      className="nav flex-column nav-pills p-2"
      id="v-pills-tab"
      role="tablist"
      aria-orientation="vertical"
    >
      <Link
        className={getLinkClass("PROFILE", mode)}
        id="profile"
        onClick={() => setMode("PROFILE")}
        data-toggle="pill"
        to="#profile"
        role="tab"
        aria-controls="profile"
      >
        {PROFILE}
      </Link>
      <Link
        className={getLinkClass("ORDERS", mode)}
        id="orders"
        onClick={() => setMode("ORDERS")}
        data-toggle="pill"
        to="#orders"
        role="tab"
        aria-controls="orders"
      >
        {ORDERS}
      </Link>
      <Link
        className={getLinkClass("ADDRESSBOOK", mode)}
        id="addressbook"
        onClick={() => setMode("ADDRESSBOOK")}
        data-toggle="pill"
        to="#addressbook"
        role="tab"
        aria-controls="addressbook"
      >
        {ADDRESS_BOOK}
      </Link>
      <Link
        className={getLinkClass("NEWSLETTER", mode)}
        id="newsletter"
        onClick={() => setMode("NEWSLETTER")}
        data-toggle="pill"
        to="#newsletter"
        role="tab"
        aria-controls="newsletter"
      >
        {NEWSLETTER}
      </Link>
    </div>
);

const getLinkClass = (currMode, mode) => ("nav-link" + (currMode === mode ? " active" : ""))

export { Nav };
