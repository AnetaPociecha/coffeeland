export const getInputClass = (isValid, str, cond = true) =>
    "form-control" + ( (!cond || isValid(str)) ? "" : " is-invalid");

