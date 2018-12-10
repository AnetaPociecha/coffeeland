import { getTypes, getMinMaxPrice} from './filterHelper'

it('should get item types', () => {
    // given 
    const items = [{type: 'A'}, {type: 'B'}, {type: 'A'}, {type: 'C'}]

    // when 
    const typeEmpty = getTypes([])
    const typeUndef = getTypes(undefined)
    const types = getTypes(items)

    // then
    expect(typeEmpty).toEqual([])
    expect(typeUndef).toEqual([])
    expect(types).toEqual(['A','B','C'])
})

it('should get min max price', () => {
    // given
    const items = [{price: 1234}, {price: 2345}, {price: 1000}]

    // when
    const priceEmpty = getMinMaxPrice([])
    const priceUndef = getMinMaxPrice(undefined)
    const price = getMinMaxPrice(items)

    //then
    expect(priceEmpty).toEqual([0,0])
    expect(priceUndef).toEqual([0,0])
    expect(price).toEqual([1000,2345])
})