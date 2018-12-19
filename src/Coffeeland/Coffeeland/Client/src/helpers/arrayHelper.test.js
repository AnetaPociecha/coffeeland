import { isArrayEmpty } from './arrayHelper'

it('should check if array is empty', () => {
    // given
    const emptyArr = []
    const arr = [1,2,3]

    // when
    const resultEmpty = isArrayEmpty(emptyArr)
    const result = isArrayEmpty(arr)

    // then
    expect(resultEmpty).toEqual(true);
    expect(result).toEqual(false);

})