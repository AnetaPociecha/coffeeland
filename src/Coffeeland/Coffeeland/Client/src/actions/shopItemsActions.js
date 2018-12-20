import { FETCH_SHOP_ITEMS } from "./types";
import { items } from "../items";
import MessageProcessor from "./../messageProcessor/messageProcessor";

export const fetchShopItems = () => dispatch => {
  const rq = {
    $type: "GetShopItemsQuery"
  };
  const mp = MessageProcessor.getInstance();
  mp.processCommand(rq).then(rs => {
    console.log(rs);
    dispatch({
      type: FETCH_SHOP_ITEMS,
      payload: rs.isSuccess ? rs.items : []
    });
  });
};
