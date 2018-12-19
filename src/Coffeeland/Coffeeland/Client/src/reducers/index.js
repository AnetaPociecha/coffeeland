import { combineReducers } from "redux";
import shopItemsReducer from "./shopItemsReducer";
import personalDataReducer from './personalDataReducer'
import addressBookReducer from './addressBookReducer'
import signInReducer from './signInReducer'

export default combineReducers({
  shopItems: shopItemsReducer,
  personalData: personalDataReducer,
  addressBook: addressBookReducer,
  token: signInReducer,
});
