import { REGISTER } from "./types";

export const register = (data) => dispatch => {
    const registerResponse = {
        isSuccess: true,
        emailTaken: false
    }
    // ask server for data and use .then(dispatch ... )
    dispatch({
      type: REGISTER,
      payload: registerResponse
    });
};