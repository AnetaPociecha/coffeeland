import { isArrayEmpty } from "./arrayHelper";

export const getTypes = items => {
  let types = [];
  items &&
    items.forEach(item => {
      if (!types.includes(item.type)) {
        types.push(item.type);
      }
    });
  return types;
};

export const getMinMaxPrice = items => {
  if (!items || isArrayEmpty(items)) return [0, 0];
  let min = Number(items[0].price),
    max = Number(items[0].price);
  for (let i = 1, len = items.length; i < len; i++) {
    let v = Number(items[i].price);
    min = v < min ? v : min;
    max = v > max ? v : max;
  }
  return [min, max];
};
