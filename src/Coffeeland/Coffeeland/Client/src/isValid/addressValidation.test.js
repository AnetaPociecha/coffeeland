import {isCountryValid, isCityValid, isZIPCodeValid ,isApartmentNumberValid, isBuildingNumberValid, isStreetValid} from './addressValidation'

it('should check if country is valid', () => {
    //then
    expect(isCountryValid('Polska')).toEqual(true)
    expect(isCountryValid('123')).toEqual(false)
    expect(isCountryValid('P')).toEqual(false)
    expect(isCountryValid('aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa')).toEqual(false)
    expect(isCountryValid('Polska1')).toEqual(false)
})

it('should check if city is valid', () => {
    //then
    expect(isCityValid('Krakow')).toEqual(true)
    expect(isCityValid('123')).toEqual(false)
    expect(isCityValid('P')).toEqual(false)
    expect(isCityValid('aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa')).toEqual(false)
    expect(isCityValid('Krakow1')).toEqual(false)
})

it('should check if street is valid', () => {
    //then
    expect(isStreetValid('Budryka')).toEqual(true)
    expect(isStreetValid('')).toEqual(true)
    expect(isStreetValid('123')).toEqual(false)
    expect(isStreetValid('aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa')).toEqual(false)
})

it('should check if ZIPCode is valid', () => {
    //then
    expect(isZIPCodeValid('32-654')).toEqual(true)
    expect(isZIPCodeValid('')).toEqual(false)
    expect(isZIPCodeValid('as-asd')).toEqual(false)
    expect(isZIPCodeValid('as-')).toEqual(false)
    expect(isZIPCodeValid('12345')).toEqual(false)
})

it('should check if building number is valid', () => {
    //then
    expect(isBuildingNumberValid('123')).toEqual(true)
    expect(isBuildingNumberValid('1')).toEqual(true)
    expect(isBuildingNumberValid('')).toEqual(false)
    expect(isBuildingNumberValid('as')).toEqual(false)
    expect(isBuildingNumberValid('123a')).toEqual(false)
    expect(isBuildingNumberValid('1234522')).toEqual(false)
})

it('should check if apartment number is valid', () => {
    //then
    expect(isApartmentNumberValid('1000A')).toEqual(true)
    expect(isApartmentNumberValid('')).toEqual(true)
    expect(isApartmentNumberValid('12345678912')).toEqual(false)
})

