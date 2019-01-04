import React from "react";
import * as title from "./../../constants/titles";
import * as config from "./../../constants/config";
import * as message from "./../../constants/messages";
import { Image } from "./../image";
import { Price } from "./../price";
import { SuccessAlert } from "./../alert";
import { Button } from "./../button";
import { InputSpinner } from "./../inputSpinner";
import { OrderTitle } from "../orderTitle";
import { Info } from "../info";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { Redirect } from "react-router";
import { SHOP } from "../../constants/paths";
import { fetchShopItems } from "./../../actions/shopItemsActions";
import { getItemName } from "../../helpers/pathHelper";

class ShopItemPage extends React.Component {
  constructor(props) {
    super(props);
    window.scrollTo(0, 0);
  }

  componentWillMount() {
    //this.props.fetchShopItems();
  }

  state = {
    quantity: 1,
    shouldAddToCartMsgBeDisplayed: false
  };

  render() {
    const {
      match: { params },
      items
    } = this.props;

    const name = getItemName(params.name);
    const item = items.items ? items.items.find(i => i.name === name) : {}
    const { quantity, shouldAddToCartMsgBeDisplayed } = this.state;

    return item ? (
      <div className="row mx-auto border mb-5 mt-3" style={this.props.style}>
        {shouldAddToCartMsgBeDisplayed && (
          <div className="col-12 mt-4">
            <SuccessAlert>{message.PRODUCT_ADDED_TO_CART}</SuccessAlert>
          </div>
        )}

        <div className="col-md-6 col-md-offset-5 pb-3 pl-3 pr-3 mt-3">
          <Image src={item.img} />
        </div>
        <div className="col-md-6 col-md-offset-5 p-3 mt-3">
          <OrderTitle>{item.name}</OrderTitle>
          <Info>Type - {item.type}</Info>
          <Info>Weight - {config.WEIGTH}kg</Info>
          <Price className="pt-2" price={item.price} />
          <div className="row">
            <div display="block" className="p-3">
              <InputSpinner
                value={quantity}
                onMinus={this.onQuantityDecrease}
                onPlus={this.onQuantityIncrease}
                min={config.MIN_QUANTITY}
                max={config.MAX_QUANTITY}
              />
            </div>
            <div display="block" className="p-3">
              <Button onClick={() => this.onAddToCart(item)}>
                {title.ADD_TO_CART}
              </Button>
            </div>
          </div>
        </div>
        <Info className="p-2 text-justify"> {item.description}</Info>
      </div>
    ) : <Redirect to={SHOP} />;
  }

  onAddToCart = item => {
    this.props.onAddToCart(item, this.state.quantity);
    this.setState({ quantity: 1 });
    this.displaySuccessMsg();
  };

  onQuantityIncrease = () => {
    this.state.quantity < config.MAX_QUANTITY &&
      this.setState(prevState => ({ quantity: prevState.quantity + 1 }));
  };

  onQuantityDecrease = () => {
    this.state.quantity > config.MIN_QUANTITY &&
      this.setState(prevState => ({ quantity: prevState.quantity - 1 }));
  };

  displaySuccessMsg = () => {
    this.setState({ shouldAddToCartMsgBeDisplayed: true });
    setTimeout(
      () => this.setState({ shouldAddToCartMsgBeDisplayed: false }),
      4000
    );
    window.scrollTo(0, 0);
  };
}

ShopItemPage.propTypes = {
  fetchShopItems: PropTypes.func.isRequired,
  items: PropTypes.array.isRequired
};

const mapStateToProps = state => ({
  items: state.items.items
});

export default connect(
  mapStateToProps,
  { fetchShopItems }
)(ShopItemPage);