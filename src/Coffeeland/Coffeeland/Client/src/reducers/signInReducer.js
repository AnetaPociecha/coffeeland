import { SIGN_IN, SIGN_OUT } from "../actions/types";

const initialState = {
  token: ''
};

export default function(state = initialState, action) {

  switch (action.type) {
    case SIGN_IN:
      return {
        ...state,
        token: action.payload
      }
      case SIGN_OUT:
      return {
        ...state,
        token: action.payload
      }
    default:
      return state;
  }
  
}