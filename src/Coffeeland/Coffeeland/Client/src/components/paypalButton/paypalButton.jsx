import React from "react";

class PaypalButton extends React.Component {
  render() {
    return (
      <form
        action="https://www.paypal.com/cgi-bin/webscr"
        method="post"
        target="_top"
      >
        <input type="hidden" name="business" value="anhayla12@gmail.com" />
        <input type="hidden" name="cmd" value="_xclick" />

        <input type="hidden" name="coffee" value="23" />
        <input type="hidden" name="amount" value="2" />
        <input type="hidden" name="currency_code" value="PLN" />
        <input id="invoice" type="hidden" value="unique" name="invoice" />
        <input type="hidden" name="notify_url" value="#" />
        <input type="hidden" name="cancel_return" value="#" />
        <input type="hidden" name="return" value="#" />
        <input
          type="image"
          name="submit"
          border="0"
          src="https://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif"
          alt="PayPal - The safer, easier way to pay online"
        />
      </form>
    );
  }
}

export default PaypalButton;
