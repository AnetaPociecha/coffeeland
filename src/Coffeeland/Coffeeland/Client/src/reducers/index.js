import { combineReducers } from "redux";
import shopItemsReducer from "./shopItemsReducer";
import personalDataReducer from './personalDataReducer'
import addressBookReducer from './addressBookReducer'

export default combineReducers({
  shopItems: shopItemsReducer,
  personalData: personalDataReducer,
  addressBook: addressBookReducer,
});
