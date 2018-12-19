import rootReducer from "./index";
import { SIGN_IN, SIGN_OUT } from "../actions/types"

const state = {}

it('should sign in', () => {
  // when
  const newState = rootReducer(state, {
    type: SIGN_IN,
    payload: {token: 'abc'}
  })

  // then
  expect(newState.signIn.token).toEqual({token: 'abc'})
})

it('should sign out', () => {
  // when
  const newState = rootReducer(state, {
    type: SIGN_OUT,
    payload: {token: ''}
  })

  // then
  expect(newState.signIn.token).toEqual({token: ''})
})
