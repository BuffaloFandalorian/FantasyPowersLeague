import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  constructor(props){
    super(props);
    this.loginCallback = this.loginCallback.bind(this);
    this.logoutCallback = this.logoutCallback.bind(this);
  }


  loginCallback(response) {
    this.props.loginCallback(response);
  }

  logoutCallback(response) {
    this.props.logoutCallback(response);
  }

  render() {
    return (
      <div>
        <NavMenu loginCallback={this.loginCallback} logoutCallback={this.logoutCallback} />
        <Container tag="main">
          {this.props.children}
        </Container>
      </div>
    );
  }
}
