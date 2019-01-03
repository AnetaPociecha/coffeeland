import {
  FETCH_PERSONAL_DATA,
  UPDATE_PERSONAL_DATA,
  SET_NEWSLETTER_EMAIL,
  REMOVE_NEWSLETTER_EMAIL,
  FETCH_ORDERS
} from "./types";
import personalData from "../personalData";
import MessageProcessor from "./../messageProcessor/messageProcessor";
const mp = MessageProcessor.getInstance();

// received payload is always completed personalData

export const fetchPersonalData = token => dispatch => {
  const rq = {
    $type: "GetPersonalDataQuery",
    sessionToken: token
  };

  mp.processQuery(rq).then(rs => {
    console.log("GetPersonalDataQuery rs", rs);
    dispatch({
      type: FETCH_PERSONAL_DATA,
      payload: rs
    });
  });
};

export const fetchOrders = token => dispatch => {
  const rq = {
    $type: "GetOrdersQuery",
    sessionToken: token
  };
  mp.processQuery(rq).then(rs => {
    dispatch({
      type: FETCH_ORDERS,
      payload: rs.isSuccess ? { orders: rs.orders } : { orders: [] }
    });
  });
};

export const updatePersonalData = data => dispatch => {
  dispatch({
    type: UPDATE_PERSONAL_DATA,
    payload: data
  });
};
