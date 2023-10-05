import React, { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import IdentityService from './services/IdentityService';
import constants from './models/constants';
import AlertFloat from './components/AlertFloat';
import './custom.css';

const App = (props) => {

  const displayName = App.name;
  const [loggedIn, setLogginState] = useState(constants.LoginState.LoggedOut);
  const [alerts, setAlerts] = useState([]);

  useEffect(()=>{
    const timer = window.setInterval(() => checkLoggedIn(), 10000);
    return () => window.clearInterval(timer);
  });

  const keepAlive = () => {
    if(window.sessionStorage.getItem("loggedIn") == constants.LoginState.LoggedIn){
      IdentityService.KeepAlive();
    }
  }

  const checkLoggedIn = () =>{
    const currentState = window.sessionStorage.getItem("loggedIn");
    if(currentState == null){
      setLogginState(constants.LoginState.LoggedOut);
      window.sessionStorage.setItem("loggedIn", constants.LoginState.LoggedOut);
    }
  }
  
  const loginCallback = async (response) => {
    //this.setState({ loggedInState : constants.LoginState.LoggingIn });
    setLogginState(constants.LoginState.LoggingIn);
    window.sessionStorage.setItem("loggedIn", constants.LoginState.LoggingIn);
    const googleLogin = await IdentityService.LoginGoogle(response.clientId, response.credential);
    if(googleLogin){
      setLogginState(constants.LoginState.LoggedIn);
      window.sessionStorage.setItem("loggedIn", constants.LoginState.LoggedIn);
      return constants.LoginState.LoggedIn;
    }
    else{
      setLogginState(constants.LoginState.LoggedOut);
      window.sessionStorage.setItem("loggedIn", constants.LoginState.LoggedOut);
      //todo present error
      return constants.LoginState.LoggedOut;
    }
  }

  const logoutCallback = (response) => {
    window.sessionStorage.clear();
    setLogginState(constants.LoginState.LoggedOut);
    window.sessionStorage.setItem("loggedIn", constants.LoginState.LoggedOut);
    setAlerts([]);
    setAlerts([...alerts, <AlertFloat />]);
    return constants.LoginState.LoggedOut;
  }

  return (
    <>
    <Layout Alerts={alerts} loggedInState={loggedIn} loginCallback={loginCallback} logoutCallback={logoutCallback}>
      <Routes>
        {AppRoutes.map((route, index) => {
          const { element, ...rest } = route;
          return <Route key={index} {...rest} element={element} />;
        })}
      </Routes>
    </Layout>
    </>
  );
}

export default App;
