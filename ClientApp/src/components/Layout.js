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


  loginCallback = async (response) =>  {
    return await this.props.loginCallback(response);
  }

  logoutCallback = async () => {
    return await this.props.logoutCallback();
  }

  render() {
    return (
      <div>
        <NavMenu loggedInState={this.props.loggedInState} loginCallback={this.loginCallback} logoutCallback={this.logoutCallback} />
        <Container tag="main">
          {this.props.children}
        </Container>
      </div>
    );
  }
}
