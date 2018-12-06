import { FETCH_ADDRESS_BOOK, UPDATE_ADDRESS_BOOK, REMOVE_ADDRESS } from "../actions/types";
import initialState from './initialState'

export default function(state = initialState, action) {
    console.log('payload', action.payload)
  switch (action.type) {
    case FETCH_ADDRESS_BOOK:
      return {
        ...state,
        addressBook: action.payload
      }
    case UPDATE_ADDRESS_BOOK:
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
