import { UNDEFINED } from './../constants/titles'
export const getZIPCode = (code) => (
    (String(code).length === 5 && String(code).match('^[0-9]{5}$')) ? (String(code).slice(0,2) + '-' + String(code).slice(2,5)) : UNDEFINED
)

export const getZIPCodeForDB = (code) => Number(`${code.slice(0,2)}${code.slice(3,6)}`)