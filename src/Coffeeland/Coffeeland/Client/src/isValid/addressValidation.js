const isNameValid = str => !!str.match("^[A-z]{2,30}$");
export const isCountryValid = country => isNameValid(country);
export const isCityValid = city => isNameValid(city);
export const isZIPCodeValid = zip => !!zip.match("^[0-9]{2}-[0-9]{3}$");
export const isStreetValid = street => !!street.match("^[A-z]{0,30}$");
export const isBuildingNumberValid = number => !!number.match("^[0-9]{1,6}$");
export const isApartmentNumberValid = number =>
  !!number.match("^[A-z0-9]{0,10}$");

export const isAddressValid = (
  country,
  city,
  ZIPCode,
  street,
  buildingNumber,
  apartmentNumber
) =>
  isCountryValid(country) &&
  isCityValid(city) &&
  isZIPCodeValid(ZIPCode) &&
  isStreetValid(street) &&
  isBuildingNumberValid(buildingNumber) &&
  isApartmentNumberValid(apartmentNumber);
