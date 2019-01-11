import React, { Component } from "react";
import personaData from "./personalDataMock";
import AddressRow from "../addressEntry/addressRow";
import addressBook from "./addressBookMock";
import SectionTitle from "./../orderTitle/sectionTitle";
import { isArrayEmpty } from "./../../helpers/arrayHelper";
import {
  ButtonDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem
} from "reactstrap";

export default class BillingDetails extends Component {
  state = {
    dropdownOpen: false,
    selectedAddress: "",
    alternativeAddresses: [],
    allAddresses: []
  };

  componentWillMount() {
    //fetch addressBook and personalData in parent
    const addresses = addressBook.addressBook;
    if (addresses.length > 0) {
      this.setState({ selectedAddress: addresses[0] });
      const rest = addresses.filter(a => a !== addresses[0]);
      this.setState({ alternativeAddresses: rest, allAddresses: addresses });
    }
    else {
      this.props.disableSubmitButton(true)
    }
  }

  render() {
    const isAddressAvaible = !isArrayEmpty(this.state.allAddresses);

    return (
      <div className="col-12 border p-3">
        <SectionTitle>Billing Details</SectionTitle>

        <div className="col-12 pt-3 pr-3 pl-3 text-center">
          {personaData.firstName} {personaData.lastName}
        </div>

        <div className="p-1 mt-2 mb-3 p-3 col-12 text-center">
          {isAddressAvaible ? (
            <div>
              <ButtonDropdown
                isOpen={this.state.dropdownOpen}
                toggle={this.toggle}
              >
                <DropdownToggle color="light" caret>
                  <AddressRow address={this.state.selectedAddress} />
                </DropdownToggle>

                <DropdownMenu>
                  {this.state.alternativeAddresses.map(a => (
                    <DropdownItem key={a.key} onClick={() => this.select(a)}>
                      <AddressRow address={a} />
                    </DropdownItem>
                  ))}
                </DropdownMenu>
              </ButtonDropdown>
            </div>
          ) : (
            <div className="text-danger font-weight-bold">
              Please add new address to address book first.
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
    this.setState({ selectedAddress, alternativeAddresses });
  };
}
