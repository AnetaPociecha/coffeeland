import React from "react";
import Item from "./item";

const Table = ({ items }) =>
  items.map( item=> <Item item={item} key={item.name} />);

export default Table;