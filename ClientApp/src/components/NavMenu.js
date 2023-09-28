import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import logo from '../images/logo.png';
import LoginWithGoogle from './google/GoogleLogin';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.loginCallback = this.loginCallback.bind(this);
    this.logoutCallback = this.logoutCallback.bind(this);

    this.state = {
      collapsed: true,
      loggedIn :false
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  loginCallback = (loginSuccess) => {
    this.setState({ loggedIn: true });
    this.props.loginCallback(loginSuccess);
  }

  logoutCallback = () => {
    this.setState({ loggedIn: false });
    this.props.logoutCallback();
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
                {!this.state.loggedIn && <LoginWithGoogle onSuccessfulLogin={this.loginCallback} /> }
                {this.state.loggedIn && <NavLink onClick={()=> this.logoutCallback()}>Logout</NavLink>}
              </NavItem>
            </ul>
          </Collapse>
        </Navbar>
      </header>
    );
  }
}
