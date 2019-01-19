import React, { Component } from "react";
import AddressRow from "../addressEntry/addressRow";
import SectionTitle from "./../orderTitle/sectionTitle";
import { isArrayEmpty } from "../../helpers/arrayHelper";
import {
  ButtonDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem
} from "reactstrap";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { fetchPersonalData } from "./../../actions/personalDataActions";

class BillingDetails extends Component {
  state = {
    dropdownOpen: false,
    alternativeAddresses: [],
    allAddresses: []
  };

  componentWillMount() {
    this.props.fetchPersonalData(this.props.token);
  }

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
    const { alternativeAddresses, dropdownOpen, allAddresses } = this.state;

    const { selectedAddress, personalData } = this.props;

    return (
      <div className="col-12 p-2">
        <SectionTitle>Billing Details</SectionTitle>

        <div className="col-12 pt-3 pr-4 pl-3">
          { personalData && personalData.firstName} { personalData && personalData.lastName }
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
    this.props.setSelectedAddress(selectedAddress);
  };
}

BillingDetails.propTypes = {
  fetchPersonalData: PropTypes.func.isRequired,
  token: PropTypes.string.isRequired
};

const mapStateToProps = state => ({
  personalData: state.personalData.personalData,
  token: state.token.token.token
});

export default connect(
  mapStateToProps,
  { fetchPersonalData }
)(BillingDetails);
