export const isNameValid = str => !!str.match('^[A-z]{2,20}$')
export const isEmailValid = str => !!str.match('^.+@.+[.].+$')

