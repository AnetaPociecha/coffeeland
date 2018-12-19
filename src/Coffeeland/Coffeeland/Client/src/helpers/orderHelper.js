import { PROCESSING, COMPLETED, UNDEFINED } from './../constants/titles'

export const getOrderStatus = (status) => {
    switch(status) {
        case 0: {
            return PROCESSING
        }
        case 1: {
            return COMPLETED
        }
        default: {
            return UNDEFINED
        }
    }
}
