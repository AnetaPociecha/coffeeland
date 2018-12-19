import { getTotalPrice, getSumPrice } from "./priceHelper";

it("should get total price", () => {
  //given
  const intPrice = 1234;
  const strPrice = "1234";
  const quantity = 2;

  // when
  const totalIntPriceQ = getTotalPrice(intPrice, quantity);
  const totalIntPrice = getTotalPrice(intPrice);

  const totalStrPriceQ = getTotalPrice(strPrice, quantity);
  const totalStrPrice = getTotalPrice(strPrice);

  // then
  expect(totalIntPriceQ).toEqual("24.68");
  expect(totalStrPriceQ).toEqual("24.68");
  expect(totalIntPrice).toEqual("12.34");
  expect(totalStrPrice).toEqual("12.34");
});

it("should get sum price", () => {
  // given
  const cartEntries = [
    { item: { price: 1234 }, quantity: 2 },
    { item: { price: 5678 }, quantity: 3 },
    { item: { price: 1111 }, quantity: 1 }
  ]
  const emptyCartEntries = []

  // when
  const sumPrice = getSumPrice(cartEntries)
  const emptySumPrice = getSumPrice(emptyCartEntries)

  // then 
  expect(sumPrice).toEqual('206.13')
  expect(emptySumPrice).toEqual('0.00')
});
