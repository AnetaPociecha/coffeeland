import { SIGN_IN, SIGN_OUT } from "./types";


export const signIn = (data) => dispatch => {
    const token = {token: 'xyz'}
    // ask server for data and use .then(dispatch ... )
    dispatch({
      type: SIGN_IN,
      payload: token
    });
};

export const signOut = () => dispatch => {
  const token = {token: ''}
  // ask server for data and use .then(dispatch ... ) // NOT EVEN THAT ?
  dispatch({
    type: SIGN_OUT,
    payload: token
  });
};

/*
export const register = (data) => dispatch => {
    const result = 'aaa'
    // ask server for data and use .then(dispatch ... )
    dispatch({
      type: REGISTER,
      payload: result 
    });
};
*/