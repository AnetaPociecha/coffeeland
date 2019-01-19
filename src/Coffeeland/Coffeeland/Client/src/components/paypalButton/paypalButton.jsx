import React from 'react';
import ReactPaypalButton from './reactPaypalButton';
import PropTypes from "prop-types";
import { connect } from "react-redux";
import MessageProcessor from "./../../messageProcessor/messageProcessor";
const mp = MessageProcessor.getInstance();

const CLIENT = {
  sandbox: 'Ab-3JbhxORmrrglfbOo1h_zx-w1_9tfqNPHIhK8_mSo57DZiVKVmXZ3snTqpDplrNgI2J0ye9vWgTm_5',
  production: '',
};
const ENV = 'sandbox'
const CURRENCY = 'USD'

class PayPalButton extends React.Component {
  
  onSuccess = (payment) => {

    const rq = {
      $type: "AddPayment",
      sessionToken: this.props.token,
      paymentId: payment.paymentID
    }

    mp.processCommand(rq).then(rs => console.log('rs', rs))
    console.log('Successful payment!', payment);
  }

  render() {

    
    const onError = (error) => console.log('Erroneous payment OR failed to load script!', error);
    const onCancel = (data) => console.log('Cancelled payment!', data);

    return (
      <div className="col-12 text-center p-4">
        <ReactPaypalButton
          client={CLIENT}
          env={ENV}
          commit={true}
          currency={CURRENCY}
          total={this.props.total}
          onSuccess={this.onSuccess}
          onError={onError}
          onCancel={onCancel}
        />
      </div>
    );
  }
}

PayPalButton.propTypes = {
  token: PropTypes.string.isRequired
};

const mapStateToProps = state => ({
  token: state.token.token.token
});

export default connect(
  mapStateToProps,
  {}
)(PayPalButton);
