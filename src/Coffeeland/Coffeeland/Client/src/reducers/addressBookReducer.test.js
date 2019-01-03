import rootReducer from "./index";
import { FETCH_ADDRESS_BOOK, UPDATE_ADDRESS_BOOK, REMOVE_ADDRESS } from "../actions/types";
import addressBook from "./../addressBook";

const state = {
  addressBook: {}
};

it('should fetch address book', () => {
  // when
  const newState = rootReducer(state, {
    type: FETCH_ADDRESS_BOOK,
    payload: addressBook
  })

  // then
  expect(newState.addressBook).toEqual({addressBook})
})

it('should update address book', () => {
  // when
  const newState = rootReducer(state, {
    type: UPDATE_ADDRESS_BOOK,
    payload: addressBook
  })

  // then
  expect(newState.addressBook).toEqual({addressBook})
})

it('should remove address from address book', () => {
    // when
    const newState = rootReducer(state, {
      type: REMOVE_ADDRESS,
      payload: addressBook
    })
  
    // then
    expect(newState.addressBook).toEqual({addressBook})
  })