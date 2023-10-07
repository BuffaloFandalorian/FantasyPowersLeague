import React, { useState } from 'react';
import { Collapse, DropdownItem, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, UncontrolledDropdown, DropdownToggle, DropdownMenu, Nav } from 'reactstrap';
import { Link, useNavigate } from 'react-router-dom';
import logo from '../images/logo.png';
import LoginWithGoogle from './google/GoogleLogin';
import constants from '../models/constants';

const NavMenu = (props) => {

  const [collapsed, setCollapsed] = useState(true);
  const navigate = useNavigate();

  const toggleNavbar = () => {
    setCollapsed(!collapsed);
  }

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
        <NavbarToggler onClick={toggleNavbar} className="mr-2" />
        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
          <Nav className="me-auto" navbar>
          <ul className="navbar-nav flex-grow">
          {(props.loggedInState === constants.LoginState.LoggedIn) &&
            <UncontrolledDropdown nav inNavbar>
            <DropdownToggle nav caret>
              Leagues
            </DropdownToggle>
            <DropdownMenu right>
              <DropdownItem onClick={() => navigate("/new-league")}>New</DropdownItem>
              <DropdownItem>Join</DropdownItem>
              <DropdownItem divider />
              <DropdownItem header>My Leagues</DropdownItem>
            </DropdownMenu>
          </UncontrolledDropdown>}
            {(props.loggedInState === constants.LoginState.LoggedIn) && <NavItem><NavLink onClick={() => navigate("/")}>Home</NavLink></NavItem>}
            <NavItem>
              {(props.loggedInState === constants.LoginState.LoggedOut) && <LoginWithGoogle onSuccessfulLogin={props.loginCallback} />}
              {(props.loggedInState === constants.LoginState.LoggedIn) && <NavLink onClick={props.logoutCallback}>Logout</NavLink>}
              {(props.loggedInState === constants.LoginState.LoggingIn) && <NavLink >Logging In...</NavLink>}
            </NavItem>
            </ul>
            </Nav>
        </Collapse>
      </Navbar>
    </header>
  );
}

export default NavMenu;
