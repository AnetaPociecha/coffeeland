import { FETCH_ADDRESS_BOOK, UPDATE_ADDRESS_BOOK, REMOVE_ADDRESS } from "./types";
import addressBook from "../addressBook";

export const fetchAddressBook = () => dispatch => {

  // ask server for data and use .then(dispatch ... )
    
  dispatch({
    type: FETCH_ADDRESS_BOOK,
    payload: addressBook
  });
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