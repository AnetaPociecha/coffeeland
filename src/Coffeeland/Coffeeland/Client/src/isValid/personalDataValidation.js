export const isNameValid = str => !!str.match("^[A-z]{2,20}$");
export const isEmailValid = str =>
  !!str.match(
    "^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:.[a-zA-Z0-9-]+)*$"
  );

export const isPasswordValid = password =>
  password.length >= 8 &&
  !!password.match("[a-z]+") &&
  !!password.match("[A-Z]+") &&
  !!password.match("[0-9]+");

export const isRepeatedPasswordValid = (repeatedPassword, password) => {
  return password === repeatedPassword;
};

export const isRegisterFormValid = (
  firstName,
  lastName,
  email,
  password,
  repeatedPassword,
  cond=true
) =>
  isNameValid(firstName) &&
  isNameValid(lastName) &&
  isEmailValid(email) &&
  isPasswordValid(password) &&
  isRepeatedPasswordValid(repeatedPassword, password) &&
  cond;
