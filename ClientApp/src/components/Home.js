import React, { Component } from 'react';
import { Container, Card, Row, Col, CardTitle, CardSubtitle, CardBody, CardText, CardHeader } from 'reactstrap';
import logo from '../images/logo.png';
import constants from '../models/constants';
import CreateALeague from './leagues/CreateALeague';
import JoinALeague from './leagues/JoinALeague';
import Leagues from './leagues/Leagues';

const Home = (props) => {
  const displayName = Home.name;

  return (
    <Container>
      <Row>
        <Col>
          <Card color="primary" inverse>
            <CardHeader>
              <CardTitle tag="h5">Welcome to Fantasy Powers League</CardTitle>
              <CardSubtitle>Fantasy Football for Geeks</CardSubtitle>
            </CardHeader>
            <CardBody>
              <CardText>
                {(props.loggedIn === constants.LoginState.LoggedOut) && <span>Please log in to continue</span>}
                {(props.loggedIn === constants.LoginState.LoggedIn) &&
                  <Leagues />
                }
              </CardText>
            </CardBody>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}

export default Home;
