import {
  FETCH_PERSONAL_DATA,
  UPDATE_PERSONAL_DATA,
  FETCH_ORDERS
} from "./types";
import MessageProcessor from "./../messageProcessor/messageProcessor";
const mp = MessageProcessor.getInstance();

export const fetchPersonalData = token => dispatch => {
  const rq = {
    $type: "GetPersonalDataQuery",
    sessionToken: token
  };

  mp.processQuery(rq).then(rs => {
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
    console.log('GetOrdersQuery rs',rs)
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
