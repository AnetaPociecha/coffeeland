import {
  FETCH_SHOP_ITEMS,
} from "../actions/types";
import initialState from './initialState'

export default function(state = initialState, action) {
  switch (action.type) {
    case FETCH_SHOP_ITEMS:
      return {
        ...state,
        items: [...action.payload]
      };
    default:
      return state;
  }
}
