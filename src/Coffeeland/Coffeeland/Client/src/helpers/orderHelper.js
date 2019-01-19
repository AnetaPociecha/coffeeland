import { PROCESSING, COMPLETED, PAID, UNDEFINED } from './../constants/titles'

export const getOrderStatus = (status) => {
    switch(status) {
        case 0: {
            return PROCESSING
        }
        case 1: {
            return PAID
        }
        case 2: {
            return COMPLETED
        }
        default: {
            return UNDEFINED
        }
    }
}
