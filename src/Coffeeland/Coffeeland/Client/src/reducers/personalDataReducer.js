import { FETCH_PERSONAL_DATA, UPDATE_PERSONAL_DATA, SET_NEWSLETTER_EMAIL, REMOVE_NEWSLETTER_EMAIL } from "../actions/types";
import initialState from './initialState'

export default function(state = initialState, action) {
  const payload = {personalData: action.payload}
  switch (action.type) {
    case FETCH_PERSONAL_DATA:
      return Object.assign({}, state, payload); 
    case UPDATE_PERSONAL_DATA:
      return Object.assign({}, state, payload); 
    case SET_NEWSLETTER_EMAIL:
      return Object.assign({}, state, payload); 
    case REMOVE_NEWSLETTER_EMAIL:
      return Object.assign({}, state, payload); 
      
    default:
      return state;
  }
}
