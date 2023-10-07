import React, { useEffect, useState } from 'react';
import { Route, Routes, useNavigate } from 'react-router-dom';
import Home from './components/Home'
import { Layout } from './components/Layout';
import IdentityService from './services/IdentityService';
import constants from './models/constants';
import AlertFloat from './components/alerts/AlertFloat';
import './custom.css';
import NewLeagueBuilder from './components/leagues/newleague/NewLeagueBuilder';
import AuthGuard from './components/shared/AuthGuard';

const App = (props) => {

  const navigate = useNavigate();
  const displayName = App.name;
  const [loggedIn, setLogginState] = useState(constants.LoginState.LoggedOut);
  const [alerts, setAlerts] = useState([]);

  useEffect(()=>{
    //check session on page reload
    if(sessionStorage.getItem("loggedIn") == 1){
      setLogginState(constants.LoginState.LoggedIn);
    }
  },[]);

  
  const loginCallback = async (response) => {
    setLogginState(constants.LoginState.LoggingIn);
    window.sessionStorage.setItem("loggedIn", constants.LoginState.LoggingIn);
    const googleLogin = await IdentityService.LoginGoogle(response.clientId, response.credential);
    if(googleLogin){
      setLogginState(constants.LoginState.LoggedIn);
      window.sessionStorage.setItem("loggedIn", constants.LoginState.LoggedIn);
      navigate("/");
      return constants.LoginState.LoggedIn;
    }
    else{
      setLogginState(constants.LoginState.LoggedOut);
      window.sessionStorage.setItem("loggedIn", constants.LoginState.LoggedOut);
      navigate("/");
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
    navigate("/");
    return constants.LoginState.LoggedOut;
  }

  return (
    <>
    <Layout Alerts={alerts} loggedInState={loggedIn} loginCallback={loginCallback} logoutCallback={logoutCallback}>
      <Routes>
        <Route path="/" element={<Home loggedIn={loggedIn} />} />
        <Route path="/new-league" element={<AuthGuard unauthorizedCallback={logoutCallback} component={<NewLeagueBuilder />} loggedIn={loggedIn} />} />
      </Routes>
    </Layout>
    </>
  );
}

export default App;
