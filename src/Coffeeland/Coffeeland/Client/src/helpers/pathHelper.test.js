import { getItemPath } from './pathHelper'
import { SHOP_ITEM } from "./../constants/paths";

it('should return path', () => {
    // when
    const simplePath = getItemPath('name')
    const complexPath = getItemPath('my name')
    // then
    expect(simplePath ).toEqual(SHOP_ITEM+'name')
    expect(complexPath ).toEqual(SHOP_ITEM+'my-name')
})