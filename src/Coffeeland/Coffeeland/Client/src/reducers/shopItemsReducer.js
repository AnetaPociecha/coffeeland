import {
  FETCH_SHOP_ITEMS,
} from "../actions/types";
import initialState from './initialState'

export default function(state = initialState, action) {
  const items = {items: [...action.payload]}
  switch (action.type) {
    case FETCH_SHOP_ITEMS:
      return Object.assign({}, state, items); 
    default:
      return state;
  }
}
