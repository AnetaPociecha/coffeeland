import React from "react";
import {Price} from "./../price";
import {ItemNameLink} from "../itemName";
import { ImageLink } from "./../image";
import * as config from "../../constants/config";
import { getItemPath } from './../../helpers/pathHelper';

const Item = ({ item }) => {
  const itemPath = getItemPath(item.name)
  return (
    <div className="col-sm-6 col-md-4 col-md-offset-1 pr-2 pt-2 pl-2">
    <div className="border p-2">
      <ImageLink src={item.img} path={itemPath} />
      <ItemNameLink path={itemPath}> {item.name} </ItemNameLink>
      <p className="text-center small">{item.type}</p>
      <Price quantity={config.INITIAL_QUANTITY} price={item.price}/>
      </div>
    </div>
  );
};

export default Item;
