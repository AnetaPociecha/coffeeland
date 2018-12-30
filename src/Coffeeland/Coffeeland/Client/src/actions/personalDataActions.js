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
      payload: rs.isSuccess ? 
        {orders: rs.orders} : {orders: []}
    });
  });
};

export const updatePersonalData = async rq => dispatch => {
  //sent to server and receive newData as response and use .then(dispatch ... )
  const mp = MessageProcessor.getInstance();
  const rs = mp.processCommand(rq);
  /* const rs = {
    isSuccess: true,
    email: rq.email,
    firstName: rq.firstName,
    lastName: rq.lastName,
    receiveNewsletterEmail: false, // bug
    newsletterEmail: ''
} */
  dispatch({
    type: UPDATE_PERSONAL_DATA,
    payload: rs
  });
};

export const setNewsletterEmail = data => dispatch => {
  const newData = { ...personalData, ...data };

  // sent to server and receive newData as response and use .then(dispatch ... )

  dispatch({
    type: SET_NEWSLETTER_EMAIL,
    payload: newData
  });
};

export const removeNewsletterEmail = () => dispatch => {
  const newData = {
    ...personalData,
    newsletter: false,
    receiveNewsletterEmail: ""
  };

  // sent to server and receive newData as response and use .then(dispatch ... )

  dispatch({
    type: REMOVE_NEWSLETTER_EMAIL,
    payload: newData
  });
};
