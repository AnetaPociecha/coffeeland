import { FETCH_ADDRESS_BOOK, UPDATE_ADDRESS_BOOK, REMOVE_ADDRESS } from "./types";
import addressBook from "../addressBook";
import MessageProcessor from "./../messageProcessor/messageProcessor";
const mp = MessageProcessor.getInstance();

export const fetchAddressBook = (token) => dispatch => {
  const rq = {
    $type: "GetAddressBookQuery",
    sessionToken: token
  }
  mp.processQuery(rq).then(rs => {
    dispatch({
      type: FETCH_ADDRESS_BOOK,
      payload: rs.isSuccess ? {addressBook: rs.addresses} : {addressBook: []} 
    })
  })
};

export const updateAddressBook = data => dispatch => {

  const newData = { addressBook:[...addressBook.addressBook, data] };

  // sent to server and receive newData as response and use .then(dispatch ... )

  dispatch({
    type: UPDATE_ADDRESS_BOOK,
    payload: newData
  });
};


export const removeAddress = data => dispatch => {

    const newAdd = addressBook.addressBook.filter(d => (d !== data))
    const newData = { addressBook: [...newAdd] };
  
    // sent to server and receive newData as response and use .then(dispatch ... )
  
    dispatch({
      type: REMOVE_ADDRESS,
      payload: newData
    });
  };