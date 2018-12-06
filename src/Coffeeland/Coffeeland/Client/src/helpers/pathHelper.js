import {SHOP_ITEM} from "./../constants/paths";
export const getItemPath = (name) => (SHOP_ITEM + name.split(" ").join("-"))