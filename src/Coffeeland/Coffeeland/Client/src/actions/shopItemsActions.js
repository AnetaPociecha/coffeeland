import {FETCH_SHOP_ITEMS} from './types'
import { items } from '../items';

export const fetchShopItems = () => dispatch => {
    dispatch({
        type: FETCH_SHOP_ITEMS,
        payload: items
    })
}