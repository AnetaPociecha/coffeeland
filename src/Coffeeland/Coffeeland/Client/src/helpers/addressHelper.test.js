import { getZIPCode } from './addressHelper'
import { UNDEFINED } from './../constants/titles'

it('should return zip code', () => {
    // given
    const z1 = 12345
    const z2 = ''
    const z3 = '1a345'

    // then
    expect(getZIPCode(z1)).toEqual('12-345')
    expect(getZIPCode(z2)).toEqual(UNDEFINED)
    expect(getZIPCode(z3)).toEqual(UNDEFINED)
    expect(getZIPCode(undefined)).toEqual(UNDEFINED)
})