import { FETCH_ADDRESS_BOOK, REMOVE_ADDRESS } from "../actions/types";

const initialState = {
  addressBook: {},
};

export default function(state = initialState, action) {
  switch (action.type) {
    case FETCH_ADDRESS_BOOK:
      return {
        ...state,
        addressBook: action.payload
      }
    case REMOVE_ADDRESS:
      return {
        ...state,
        addressBook: action.payload
      }
    default:
      return state;
  }
  
}
