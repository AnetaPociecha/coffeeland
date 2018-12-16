import Shop from "../components/shop/shopPage";
import ShopItemPage from "../components/shop/shopItemPage";
import SignInRegisterPage from "../components/authentication/signInRegisterPage";
import CartPage from "../components/cart/cartPage";
import MyAccount from "../components/myAccount";
import Information from "../components/pages/information";
import { SHOP, SIGN_IN, INFORMATION, CART, MY_ACCOUNT, SHOP_ITEM_FULL } from "../constants/paths";

export const routes = [
    {
      path: SHOP,
      component: Shop
    },
    {
      path: SIGN_IN,
      component: SignInRegisterPage
    },
    {
      path: INFORMATION,
      component: Information
    },
    {
      path: CART,
      component: CartPage
    },
    {
      path: MY_ACCOUNT,
      component: MyAccount
    },
    {
      path: SHOP_ITEM_FULL,
      component: ShopItemPage
    }
  ];