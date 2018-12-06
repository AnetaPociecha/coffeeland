import rootReducer from "./index";
import { FETCH_PERSONAL_DATA, UPDATE_PERSONAL_DATA, SET_NEWSLETTER_EMAIL, REMOVE_NEWSLETTER_EMAIL } from "../actions/types";
import personalData from "./../personalData";

const state = {
  personalData: {},
  addressBook: {}
};


it("should fetch personal data", () => {
  //when
  const newState = rootReducer(state, {
    type: FETCH_PERSONAL_DATA,
    payload: personalData
  });
  const undefState = rootReducer(undefined, {
    type: FETCH_PERSONAL_DATA,
    payload: personalData
  });
  const noActionState = rootReducer(state, { type: "NO TYPE" });
  // then
  expect(newState.personalData).toEqual({ personalData });
  expect(undefState.personalData).toEqual({ personalData });
  expect(noActionState.personalData).toEqual({});
});

it('should update personal data', () => {
  // when
  const newState = rootReducer(state, {
    type: UPDATE_PERSONAL_DATA,
    payload: personalData
  })

  // then
  expect(newState.personalData).toEqual({personalData})
})

it('should set newsletter email', () => {
  // when
  const newState = rootReducer(state, {
    type: SET_NEWSLETTER_EMAIL,
    payload: personalData
  })

  // then
  expect(newState.personalData).toEqual({personalData})
})

it('should remove newsletter email', () => {
  // when
  const newState = rootReducer(state, {
    type: REMOVE_NEWSLETTER_EMAIL,
    payload: personalData
  })

  // then
  expect(newState.personalData).toEqual({personalData})
})