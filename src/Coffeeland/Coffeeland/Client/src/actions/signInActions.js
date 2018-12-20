import { SIGN_IN, SIGN_OUT } from "./types";
import MessageProcessor from './../../messageProcessor/messageProcessor'
const mp = MessageProcessor.getInstance();

export const signIn = async (rq) => dispatch => {

  const rs = await mp.processQuery(rq)
    /* const rs = {
      isSuccess: true,
      sessionToken: 'xyz'
  }*/
    dispatch({
      type: SIGN_IN,
      payload: {token: rs.sessionToken}
    });
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