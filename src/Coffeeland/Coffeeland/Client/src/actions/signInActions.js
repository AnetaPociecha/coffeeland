import { SIGN_IN, SIGN_OUT } from "./types";
import MessageProcessor from './../messageProcessor/MessageProcessor'
const mp = MessageProcessor.getInstance();

export const dispatchToken = rs => dispatch => {
   dispatch({
      type: SIGN_IN,
      payload: rs.isSuccess ? {token: rs.sessionToken, isSignIn: true} 
          : {token: '', isSignIn: false}
    })
  
};

export const signOut = (rq) => dispatch => {
  mp.processQuery(rq).then(rs => {
    console.log('signOut rs', rs)
    if(rs.isSuccess) {
      
      dispatch({
        type: SIGN_OUT,
        payload: {token: '', isSignIn: false} 
      });
    } 
  })
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