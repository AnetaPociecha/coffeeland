import React from 'react';
import ReactPaypalButton from './reactPaypalButton';

const CLIENT = {
  sandbox: 'Ab-3JbhxORmrrglfbOo1h_zx-w1_9tfqNPHIhK8_mSo57DZiVKVmXZ3snTqpDplrNgI2J0ye9vWgTm_5',
  production: '',
};
const ENV = 'sandbox'
const CURRENCY = 'PLN'

class PayPalButton extends React.Component {
  
  render() {

    const onSuccess = (payment) => console.log('Successful payment!', payment);
    const onError = (error) => console.log('Erroneous payment OR failed to load script!', error);
    const onCancel = (data) => console.log('Cancelled payment!', data);

    return (
      <div>
        <ReactPaypalButton
          client={CLIENT}
          env={ENV}
          commit={true}
          currency={CURRENCY}
          total={20}
          onSuccess={onSuccess}
          onError={onError}
          onCancel={onCancel}
        />
      </div>
    );
  }
}

export default PayPalButton;