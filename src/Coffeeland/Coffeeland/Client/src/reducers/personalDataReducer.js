import { FETCH_PERSONAL_DATA, UPDATE_PERSONAL_DATA, SET_NEWSLETTER_EMAIL, REMOVE_NEWSLETTER_EMAIL, FETCH_ORDERS } from "../actions/types";

const initialState = {
  personalData: {},
  orders: {}
};

export default function(state = initialState, action) {
  switch (action.type) {
    case FETCH_PERSONAL_DATA:
      return {
        ...state,
        personalData: action.payload
      };
    case FETCH_ORDERS:
      return {
        ...state,
        orders: action.payload
      };
    case UPDATE_PERSONAL_DATA:
      return {
        ...state,
        personalData: action.payload
      };
    case SET_NEWSLETTER_EMAIL:
      return {
        ...state,
        personalData: action.payload
      };
    case REMOVE_NEWSLETTER_EMAIL:
      return {
        ...state,
        personalData: action.payload
      };
      
    default:
      return state;
  }
}
