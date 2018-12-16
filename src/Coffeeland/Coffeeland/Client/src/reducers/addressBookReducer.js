import { FETCH_ADDRESS_BOOK, UPDATE_ADDRESS_BOOK, REMOVE_ADDRESS } from "../actions/types";
import initialState from './initialState'

export default function(state = initialState, action) {
  const payload = {addressBook: action.payload}
  switch (action.type) {
    case FETCH_ADDRESS_BOOK:
      return Object.assign({}, state, payload); 
    case UPDATE_ADDRESS_BOOK:
      return Object.assign({}, state, payload); 
    case REMOVE_ADDRESS:
      return Object.assign({}, state, payload); 
    default:
      return state;
  }
  
}
