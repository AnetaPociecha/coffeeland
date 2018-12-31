import {
  FETCH_ADDRESS_BOOK
} from "./types";
import addressBook from "../addressBook";
import MessageProcessor from "./../messageProcessor/messageProcessor";
const mp = MessageProcessor.getInstance();

export const fetchAddressBook = token => dispatch => {
  const rq = {
    $type: "GetAddressBookQuery",
    sessionToken: token
  };
  mp.processQuery(rq).then(rs => {
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
  const newAddresses = addressBook.addressBook.filter(a => a.key !== rq.addressKey);
  mp.processCommand(rq).then(rs => {
    console.log("InactivateAddressCommand rs", rs)
    if(rs.isSuccess) {
      dispatch({
        type: FETCH_ADDRESS_BOOK,
        payload: { addressBook: [...newAddresses] }
      });
    }
  })

};
