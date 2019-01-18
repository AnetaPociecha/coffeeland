import React, { Component } from "react";
import personaData from "./personalDataMock";
import AddressRow from "../addressEntry/addressRow";
import SectionTitle from "./../orderTitle/sectionTitle";
import { isArrayEmpty } from "../../helpers/arrayHelper";
import {
  ButtonDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem
} from "reactstrap";

export default class BillingDetails extends Component {
  state = {
    dropdownOpen: false,
    alternativeAddresses: [],
    allAddresses: []
  };

  componentDidMount() {
    if (this.props.addresses && !isArrayEmpty(this.props.addresses)) {
      const rest = this.props.addresses.filter(
        a => a !== this.props.addresses[0]
      );
      this.setState({
        alternativeAddresses: rest,
        allAddresses: this.props.addresses
      });
    }
  }

  render() {
    const {
      alternativeAddresses,
      dropdownOpen,
      allAddresses
    } = this.state;
    const { selectedAddress } = this.props

    return (
      <div className="col-12 p-2">
        <SectionTitle>Billing Details</SectionTitle>

        <div className="col-12 pt-3 pr-4 pl-3">
          {personaData.firstName} {personaData.lastName}
        </div>

        <div className="p-1 mt-2 mb-1 p-3 col-12">
          {!isArrayEmpty(allAddresses) && (
            <div>
              {!isArrayEmpty(alternativeAddresses) ? (
                <ButtonDropdown isOpen={dropdownOpen} toggle={this.toggle}>
                  <DropdownToggle color="light" caret>
                    <AddressRow address={selectedAddress} />
                  </DropdownToggle>
                  <DropdownMenu>
                    {alternativeAddresses.map(a => (
                      <DropdownItem key={a.key} onClick={() => this.select(a)}>
                        <AddressRow address={a} />
                      </DropdownItem>
                    ))}
                  </DropdownMenu>
                </ButtonDropdown>
              ) : (
                <AddressRow address={selectedAddress} />
              )}
            </div>
          )}
        </div>
      </div>
    );
  }
  toggle = () => {
    this.setState({
      dropdownOpen: !this.state.dropdownOpen
    });
  };
  select = selectedAddress => {
    const alternativeAddresses = this.state.allAddresses.filter(
      a => a !== selectedAddress
    );
    this.setState({ alternativeAddresses });
    this.props.setSelectedAddress(selectedAddress)
  };
}
