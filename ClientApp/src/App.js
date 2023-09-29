import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import IdentityService from './services/IdentityService';
import './custom.css';

export default class App extends Component {

  static displayName = App.name;

  constructor(props){
    super(props);
    this.state = {
      loggedIn : false
    };
  }
  
  loginCallback = async (response) => {
    const loggedIn = await IdentityService.LoginGoogle(response.clientId, response.credential);
    this.initiateLogin();
  }

  logoutCallback = (response) => {
    this.initiateLogout();
  }

  initiateLogin = () => {
    this.setState({ loggedIn : true});
  }

  initiateLogout = () => {
    this.setState({ loggedIn : false});
  }

  render() {
    return (
      <Layout loginCallback={this.loginCallback} logoutCallback={this.logoutCallback}>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}
        </Routes>
      </Layout>
    );
  }
}
