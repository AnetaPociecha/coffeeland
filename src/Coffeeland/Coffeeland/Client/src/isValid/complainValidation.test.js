import {isComplainValid} from './complainValidation'

it('should valid complain', () => {
    // then
    expect(isComplainValid('your coffee is tragic, sorry :(')).toEqual(true)
    expect(isComplainValid('1) bad flavour 2) too expensive')).toEqual(true)
    expect(isComplainValid('I need more money/time')).toEqual(true)
    expect(isComplainValid('')).toEqual(false)
    expect(isComplainValid('you')).toEqual(false)
})