import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import IdentityService from './servcies/IdentityService';
import './custom.css';

export default class App extends Component {

  static displayName = App.name;

  loginCallback(response){
    IdentityService.LoginGoogle(response.clientId, response.credential).then( (result) => {console.log(result)} );
  }

  logoutCallback(response){
    //todo
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
