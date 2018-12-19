import React, { Component } from "react";
import Modal from "react-modal";
import { Button } from "../button";

export default class TermsModal extends Component {
  componentWillMount() {
    Modal.setAppElement("body");
  }

  render() {
    const { isActive, onModalClose } = this.props;
    return (
      <Modal isOpen={isActive} onRequestClose={onModalClose}>
      <div className="row ml-2 mr-2">
        <h5 className="text-center col-12 p-2">Terms</h5>
        <p className="text-justify col-12 p-2">
          Information and content you provide. We collect the content,
          communications and other information you provide when you use our
          Products, including when you sign up for an account, create or share
          content, and message or communicate with others. This can include
          information in or about the content you provide (like metadata), such
          as the location of a photo or the date a file was created. It can also
          include what you see through features we provide, such as our camera,
          so we can do things like suggest masks and filters that you might
          like, or give you tips on using portrait mode. Our systems
          automatically process content and communications you and others
          provide to analyze context and what's in them for the purposes
          described below. Learn more about how you can control who can see the
          things you share. Data with special protections: You can choose to
          provide information in your Facebook profile fields or Life Events
          about your religious views, political views, who you are "interested
          in," or your health. This and other information (such as racial or
          ethnic origin, philosophical beliefs or trade union membership) is
          subject to special protections under EU law. Networks and connections.
          We collect information about the people, Pages, accounts, hashtags and
          groups you are connected to and how you interact with them across our
          Products, such as people you communicate with the most or groups you
          are part of. We also collect contact information if you choose to
          upload, sync or import it from a device (such as an address book or
          call log or SMS log history), which we use for things like helping you
          and others find people you may know and for the other purposes listed
          below. Your usage. We collect information about how you use our
          Products, such as the types of content you view or engage with; the
          features you use; the actions you take; the people or accounts you
          interact with; and the time, frequency and duration of your
          activities. For example, we log when you're using and have last used
          our Products, and what posts, videos and other content you view on our
          Products. We also collect information about how you use features like
          our camera. Information about transactions made on our Products. If
          you use our Products for purchases or other financial transactions
          (such as when you make a purchase in a game or make a donation), we
          collect information about the purchase or transaction. This includes
          payment information, such as your credit or debit card number and
          other card information; other account and authentication information;
          and billing, shipping and contact details.
        </p>
        <div className="col-12 text-center p-2">
            <Button onClick={onModalClose}>OK</Button>
        </div>
        </div>
      </Modal>
    );
  }
}
