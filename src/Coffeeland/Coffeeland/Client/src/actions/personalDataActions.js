import { FETCH_PERSONAL_DATA, UPDATE_PERSONAL_DATA, SET_NEWSLETTER_EMAIL, REMOVE_NEWSLETTER_EMAIL } from "./types";
import personalData from "../personalData";


// received payload is always completed personalData

export const fetchPersonalData = () => dispatch => {

  // ask server for data and use .then(dispatch ... )

  dispatch({
    type: FETCH_PERSONAL_DATA,
    payload: personalData
  });
};

export const updatePersonalData = data => dispatch => {
  const newData = Object.assign({}, personalData, data) 

  // sent to server and receive newData as response and use .then(dispatch ... )

  dispatch({
    type: UPDATE_PERSONAL_DATA,
    payload: newData
  });
};

export const setNewsletterEmail = data => dispatch => {
  const newData = Object.assign({}, personalData, data) 

  // sent to server and receive newData as response and use .then(dispatch ... )

  dispatch({
    type: SET_NEWSLETTER_EMAIL,
    payload: newData
  });
};

export const removeNewsletterEmail = () => dispatch => {
  const newData = Object.assign({}, personalData, {newsletter: false, receiveNewsletterEmail: ''}) 

  // sent to server and receive newData as response and use .then(dispatch ... )

  dispatch({
    type: REMOVE_NEWSLETTER_EMAIL,
    payload: newData
  });
};

