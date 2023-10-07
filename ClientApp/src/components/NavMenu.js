import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import logo from '../images/logo.png';
import LoginWithGoogle from './google/GoogleLogin';
import constants from '../models/constants';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.loginCallback = this.loginCallback.bind(this);
    this.logoutCallback = this.logoutCallback.bind(this);

    this.state = {
      collapsed: true,
      loggedInState : this.props.loggedInState
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  loginCallback = async (response) => {
    this.setState({ loggedInState: await this.props.loginCallback(response)});
  }

  logoutCallback = async () => {
    this.setState({ loggedInState: await this.props.logoutCallback()});
  }

  render() {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm  border-bottom box-shadow mb-3" container color="primary">
          <NavbarBrand tag={Link} to="/"><img
                      alt="logo"
                      src={logo}
                      style={{
                        height: 50,
                        width: 50
                      }}
          /> Fantasy Powers League</NavbarBrand>
          <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                {(this.props.loggedInState === constants.LoginState.LoggedOut) && <LoginWithGoogle onSuccessfulLogin={this.loginCallback} /> }
                {(this.props.loggedInState === constants.LoginState.LoggedIn) && <NavLink onClick={()=> this.logoutCallback()}>Logout</NavLink>}
                {(this.props.loggedInState === constants.LoginState.LoggingIn) && <NavLink >Logging In...</NavLink>}
              </NavItem>
            </ul>
          </Collapse>
        </Navbar>
      </header>
    );
  }
}
