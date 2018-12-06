import React, { Component } from "react";
import TypeCheckbox from "./typeCheckbox";
import PriceFilter from "./priceFilter";
import FilterTitle from "./filterTitle";
import * as title from "../../constants/titles";

const minId = "selectedMinPrice";
const maxId = "selectedMaxPrice";

class Filter extends Component {
  state = {
    selectedTypes: [],
    selectedMinPrice: 0,
    selectedMaxPrice: 0,
  };

  componentWillReceiveProps(nextProps) {
    this.state.selectedMinPrice === 0 && this.setState({selectedMinPrice: nextProps.initialMinPrice})
    this.state.selectedMaxPrice === 0 && this.setState({selectedMaxPrice: nextProps.initialMaxPrice})
  }

  render() {
    const { types, minMaxPrice } = this.props;
    const { selectedMinPrice, selectedMaxPrice } = this.state;
    const min = minMaxPrice[0];
    const max = minMaxPrice[1];
    return (
      <div className="row">
        <div className="col-md-12 col-sm-6 my-auto pt-4">
          <FilterTitle>{title.FILTER_BY_TYPE}</FilterTitle>
          {types.map(type => (
            <TypeCheckbox
              type={type}
              onCheckboxChange={this.onCheckboxChange}
              key={type}
            />
          ))}
        </div>

        <div className="col-md-12 col-sm-6 my-auto pt-4">
          <FilterTitle>{title.FILTER_BY_PRICE}</FilterTitle>
          <PriceFilter
            min={min}
            max={max}
            selectedMin={selectedMinPrice}
            selectedMax={selectedMaxPrice}
            onPriceChange={this.onPriceChange}
            minId={minId}
            maxId={maxId}
          />
        </div>
      </div>
    );
  }

  onCheckboxChange = (type, checked) => {
    let newTypes = checked
      ? this.getNewTypeForAdding(type)
      : this.getNewTypesForRemoving(type);
    this.setState({ selectedTypes: newTypes });
    this.props.handleFilterByTypeChange(newTypes);
  };

  getNewTypeForAdding = type => {
    return [...this.state.selectedTypes, type];
  };

  getNewTypesForRemoving = type => {
    const oldTypes = [...this.state.selectedTypes];
    return oldTypes.filter(t => t !== type);
  };

  onPriceChange = event => {
    const { handleFilterByPriceChange } = this.props;
    const { selectedMinPrice, selectedMaxPrice } = this.state;
    const target = event.target;
    const value = target.value;
    const name = target.name;
    this.setState({ [name]: value });
    name === minId
      ? handleFilterByPriceChange(value, selectedMaxPrice)
      : handleFilterByPriceChange(selectedMinPrice, value);
  };
}

export default Filter;
