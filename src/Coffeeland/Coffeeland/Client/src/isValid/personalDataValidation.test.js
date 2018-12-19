import {isNameValid, isEmailValid, isPasswordValid, isRepeatedPasswordValid} from './personalDataValidation'

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

it('should valid email', () => {
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

it('should valid password', () => {
    // given
    const pwd1 = 'asd12A'
    const pwd2 = ''
    const pwd3 = 'Haslodssddfg'
    const pwd4 = 'Grtuy45#23tr'
    const pwd5 = 'AddfdfdRE34'

    // then
    expect(isPasswordValid(pwd1)).toEqual(false)
    expect(isPasswordValid(pwd2)).toEqual(false)
    expect(isPasswordValid(pwd3)).toEqual(false)
    expect(isPasswordValid(pwd4)).toEqual(true)
    expect(isPasswordValid(pwd5)).toEqual(true)
})

it('should valid repeated password', () => {
    // given
    const pwd1 = 'asd12A'
    const pwd2 = ''
    const pwd3 = 'Haslodssddfg'
    const pwd4 = 'Grtuy45#23tr'
    const pwd5 = 'Haslodssddfg1'

    // then
    expect(isRepeatedPasswordValid(pwd1, pwd2)).toEqual(false)
    expect(isRepeatedPasswordValid(pwd2, pwd3)).toEqual(false)
    expect(isRepeatedPasswordValid(pwd3, pwd5)).toEqual(false)
    expect(isRepeatedPasswordValid(pwd4, pwd4)).toEqual(true)
    expect(isRepeatedPasswordValid(pwd1, pwd1)).toEqual(true)
})