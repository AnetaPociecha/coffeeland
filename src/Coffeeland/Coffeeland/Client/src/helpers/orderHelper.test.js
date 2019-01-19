import { getOrderStatus } from './orderHelper'
import { PROCESSING, COMPLETED, UNDEFINED, PAID } from './../constants/titles'

it('should return order status', () => {
    // then
    expect(getOrderStatus(0)).toEqual(PROCESSING)
    expect(getOrderStatus(1)).toEqual(PAID)
    expect(getOrderStatus(2)).toEqual(COMPLETED)
    expect(getOrderStatus(2.8)).toEqual(UNDEFINED)
    expect(getOrderStatus(undefined)).toEqual(UNDEFINED)
})