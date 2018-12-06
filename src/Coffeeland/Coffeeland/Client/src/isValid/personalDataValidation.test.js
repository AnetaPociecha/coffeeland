import {isNameValid, isEmailValid} from './personalDataValidation'

it('should check if name is valid', () => {
    // given 
    const valName = "An"
    const longName = "Anna"
    const tooLongName = "Annaaaaaaaaaaaaaaaaaa"
    const invalName = "Ana1"
    const emptyName = ''
    const shortName = 'A'

    // then
    expect(isNameValid(valName)).toEqual(true)
    expect(isNameValid(longName)).toEqual(true)
    expect(isNameValid(invalName)).toEqual(false)
    expect(isNameValid(emptyName)).toEqual(false)
    expect(isNameValid(shortName)).toEqual(false)
    expect(isNameValid(tooLongName)).toEqual(false)
})

it('should check if email is valid', () => {
    // given
    const valEmail = 'anna@gmail.com'
    const invalEmail1 = '@gmail.com'
    const invalEmail2 = 'anna@.com'
    const invalEmail3 = 'anna@.'
    const invalEmail4 = 'anna@gmail.'
    const invalEmail5 = 'annagmail.com'
    const invalEmail6 = ''

    // then
    expect(isEmailValid(valEmail)).toEqual(true)
    expect(isEmailValid(invalEmail1)).toEqual(false)
    expect(isEmailValid(invalEmail2)).toEqual(false)
    expect(isEmailValid(invalEmail3)).toEqual(false)
    expect(isEmailValid(invalEmail4)).toEqual(false)
    expect(isEmailValid(invalEmail5)).toEqual(false)
    expect(isEmailValid(invalEmail6)).toEqual(false)
})