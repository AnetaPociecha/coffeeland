import { FETCH_SHOP_ITEMS } from "./types";
import { items } from "../items";
import MessageProcessor from "./../messageProcessor/messageProcessor";

export const fetchShopItems = () => dispatch => {
  const rq = {
    $type: "GetShopItemsQuery"
  };
  const mp = MessageProcessor.getInstance();
  mp.processQuery(rq).then(rs => {
    //hack please fix it
    dispatch({
      type: FETCH_SHOP_ITEMS,
      payload: rs.isSuccess ? {items: rs.shopItems || []} : {items: []}
    });
  });
};
