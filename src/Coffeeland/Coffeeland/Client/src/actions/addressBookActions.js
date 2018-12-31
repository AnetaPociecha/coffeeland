import {
  FETCH_ADDRESS_BOOK,
  REMOVE_ADDRESS
} from "./types";
import MessageProcessor from "./../messageProcessor/messageProcessor";
const mp = MessageProcessor.getInstance();

export const fetchAddressBook = token => dispatch => {
  const rq = {
    $type: "GetAddressBookQuery",
    sessionToken: token
  };
  mp.processQuery(rq).then(rs => {
    console.log("getAddressBookQuery rs", rs);
    dispatch({
      type: FETCH_ADDRESS_BOOK,
      payload: rs.isSuccess
        ? { addressBook: rs.addresses }
        : { addressBook: [] }
    });
  });
};

export const updateAddressBook = rq => dispatch => {
  mp.processCommand(rq).then(rs => {
    console.log("AddAddressCommand rs", rs);
    if (rs.isSuccess) {
      dispatch({
        type: FETCH_ADDRESS_BOOK,
        payload: { addressBook: rs.addresses }
      });
    }
  });
};

export const removeAddress = rq => dispatch => {
  mp.processCommand(rq).then(rs => {
    console.log("InactivateAddressCommand rs", rs)
    if(rs.isSuccess) {
      dispatch({
        type: REMOVE_ADDRESS,
        payload: { addressBook: rs.addresses }
      });
    }
  })
};
