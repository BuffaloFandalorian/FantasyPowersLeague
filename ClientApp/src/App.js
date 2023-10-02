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
      loggedInState : "LoggedOut"
    };
  }
  
  loginCallback = async (response) => {
    this.setState({ loggedInState : "LoggingIn" });
    const loggedIn = await IdentityService.LoginGoogle(response.clientId, response.credential);
    if(loggedIn){
      this.setState({ loggedInState: "LoggedIn"});
      return "LoggedIn";
    }
    else{
      this.state.loggedInState = "LoggedOut";
      //todo present error
      this.initiateLogout();
      return "LoggedOut";
    }
  }

  logoutCallback = (response) => {
    //todo logout
    this.setState({ loggedInState: "LoggedOut"});
    return "LoggedOut";
  }

  render() {
    return (
      <Layout loggedInState={this.state.loggedInState} loginCallback={this.loginCallback} logoutCallback={this.logoutCallback}>
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
