import { FETCH_PERSONAL_DATA, UPDATE_PERSONAL_DATA, SET_NEWSLETTER_EMAIL, REMOVE_NEWSLETTER_EMAIL } from "../actions/types";

const initialState = {
  personalData: {},
};

export default function(state = initialState, action) {
  switch (action.type) {
    case FETCH_PERSONAL_DATA:
      return {
        ...state,
        personalData: action.payload
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
