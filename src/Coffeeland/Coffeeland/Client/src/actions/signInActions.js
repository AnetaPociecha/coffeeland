import { SIGN_IN, SIGN_OUT } from "./types";
import MessageProcessor from './../messageProcessor/MessageProcessor'
const mp = MessageProcessor.getInstance();

export const signIn = rs => dispatch => {

  /*mp.processQuery(rq).then(rs => {
    
    return rs.isSuccess
    }
   ) */

   dispatch({
      type: SIGN_IN,
      payload: rs.isSuccess ? {token: rs.sessionToken} : ''
    })
  
    /* const rs = {
      isSuccess: true,
      sessionToken: 'xyz'
  }*/
};

export const signOut = () => dispatch => {
  const token = {token: ''}
  
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