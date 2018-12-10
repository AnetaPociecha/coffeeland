import rootReducer from "./index";
import { FETCH_SHOP_ITEMS } from "../actions/types";

it("should return shop items", () => {
  // GIVEN
  const initialState = {
    shopItems: { items: [] }
  };
  const item = {
    key: 1,
    name: "Guatemala Antigua",
    price: 1425,
    img: "./coffee.jpg",
    type: "Single Origin",
    description:
      "Guatemala is known for delicious coffee, especially from the region of Antigua. That is why we have chosen to offer this coffee – it is complex, rich, and unique, an all-around pleasing cup with a fragrant aroma."
  };
  const state = {
    shopItems: { items: [item] }
  };
  const newItems = [
    {
      key: 1,
      name: "Guatemala Antigua",
      price: 1425,
      img: "./coffee.jpg",
      type: "Single Origin",
      description:
        "Guatemala is known for delicious coffee, especially from the region of Antigua. That is why we have chosen to offer this coffee – it is complex, rich, and unique, an all-around pleasing cup with a fragrant aroma."
    },
    {
      key: 2,
      name: "Driven House Blend",
      price: 1350,
      img: "./coffee.jpg",
      type: "Blends",
      description:
        "A smooth light roasted dynamic powerhouse. No bitterness or aftertaste on this gem. We use portions of both our Brazil and Costa Rica for this coffee. Costa Rican coffees set the standard for washed (wet processed) bright Central American coffees in both the bean and at the mill. Costa Rican coffees are exceptionally high grown in amazing volcanic soil. These two factors come together to produce a very bright and very clean cup. The best Costas are the cups that develop a bit of berry fruitiness to compliment the straight-out brightness. Costa Rican coffees serve as an excellent bright single origin coffee and will definitely add life to various blends. Additionally, these slower grown, dense, high altitude beans can take the heat of a French roast."
    }
  ];

  // WHEN
  const stateInit = rootReducer(initialState, { type: "NOT_ACTION" });
  const stateNew = rootReducer(state, {
    type: FETCH_SHOP_ITEMS,
    payload: newItems
  });
  const stateUndef = rootReducer(undefined, {
    type: FETCH_SHOP_ITEMS,
    payload: newItems
  });

  // THEN
  expect(stateInit.shopItems).toEqual(initialState.shopItems);
  expect(stateNew.shopItems).toEqual({ items: [...newItems] });
  expect(stateUndef.shopItems).toEqual({
    items: newItems
  });
});
