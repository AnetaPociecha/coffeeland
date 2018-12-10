export const getTotalPrice = (priceInCent, quantity = 1) =>
  parseFloat((Math.round(priceInCent) / 100) * quantity).toFixed(2);

export const getSumPrice = cartEntries =>
  cartEntries
    .reduce(
      (s, e) =>
        (s = Number(s) + Number(getTotalPrice(e.item.price, e.quantity, 0))),
      0
    )
    .toFixed(2);
