import React, { Component } from "react";
import Table from "./table";
import Filter from "./filter";
import { NO_ITEMS_FOUND } from "../../constants/messages";
import { withMessage } from "../../with/withMessage";
import { isArrayEmpty } from "../../helpers/arrayHelper";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { fetchShopItems } from "./../../actions/shopItemsActions";
import { getTypes, getMinMaxPrice} from './../../helpers/filterHelper'

class Shop extends Component {
  constructor(props) {
    super(props);
    window.scrollTo(0, 0);
    this.state = this.getInitialState();
  }

  componentWillMount() {
    this.props.fetchShopItems();
  }

  componentWillReceiveProps(nextProps) {
    if (nextProps.items) {
       this.setState({
        allItems: nextProps.items.items ? [...nextProps.items.items] : [],
        typeFilterItems:  nextProps.items.items ? [...nextProps.items.items] : [],
        priceFilterItems: nextProps.items.items ? [...nextProps.items.items] : [],
        visibleItems:  nextProps.items.items ? [...nextProps.items.items] : [],
      });
    }
  }

  render() {
    const { visibleItems, allItems } = this.state;
    const minMax = getMinMaxPrice(allItems)
    return (
      <div className="row mx-auto" style={this.props.style}>
        <div className="col-md-2 col-sm-12">
          <Filter
            types={getTypes(allItems)}
            minMaxPrice={minMax}
            initialMinPrice={minMax[0]}
            initialMaxPrice={minMax[1]}
            handleFilterByTypeChange={this.handleFilterByTypeChange}
            handleFilterByPriceChange={this.handleFilterByPriceChange}
          />
        </div>
        <div className="col-md-10 col-sm-12">
          <div className="row pt-3 pr-5 pl-3 pb-5">
            <TableWithMessage
              shouldDisplayMsg={isArrayEmpty(visibleItems)}
              msg={NO_ITEMS_FOUND}
              items={visibleItems}
            />
          </div>
        </div>
      </div>
    );
  }

  handleFilterByTypeChange = types => {
    const allItems = [...this.state.allItems];
    if (isArrayEmpty(types)) {
      this.setState({ typeFilterItems: allItems });
      this.updateVisibleItems(allItems, this.state.priceFilterItems);
    } else {
      const newItems = allItems.filter(i => types.includes(i.type));
      this.setState({ typeFilterItems: newItems });
      this.updateVisibleItems(newItems, this.state.priceFilterItems);
    }
  };

  handleFilterByPriceChange = (minPrice, maxPrice) => {
    const allItems = [...this.state.allItems];
    const newItems = allItems.filter(
      i =>
        Number(i.price) >= Number(minPrice) &&
        Number(i.price) <= Number(maxPrice)
    );
    this.setState({ priceFilterItems: newItems });
    this.updateVisibleItems(this.state.typeFilterItems, newItems);
  };

  updateVisibleItems = (types, prices) => {
    const typesCopy = [...types];
    const common = typesCopy.filter(i => prices.includes(i));
    this.setState({ visibleItems: common });
  };

  getInitialState = () => ({
    allItems: [],
    typeFilterItems: [],
    priceFilterItems: [],
    visibleItems: []
  })
}

const TableWithMessage = withMessage(Table);

Shop.propTypes = {
  fetchShopItems: PropTypes.func.isRequired,
  items: PropTypes.object.isRequired
};

const mapStateToProps = state => ({
  items: state.items.items
});

export default connect(
  mapStateToProps,
  { fetchShopItems }
)(Shop);
