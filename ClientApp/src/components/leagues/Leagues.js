import { Container, Row, Col, Accordion, AccordionHeader, AccordionBody, AccordionItem } from "reactstrap";
import { useState } from "react";
import CreateALeague from "./CreateALeague";
import JoinALeague from "./JoinALeague";
import MyLeagues from "./MyLeagues";

const Leagues = (props) => {

    const [open, setOpen] = useState('1');
    const toggle = (id) => {
        if (open === id) {
            setOpen();
        } else {
            setOpen(id);
        }
    }

    return (
        <>
            <Accordion open={open} toggle={toggle}>
                <AccordionItem>
                    <AccordionHeader targetId="1">
                        New Leagues
                    </AccordionHeader>
                    <AccordionBody accordionId="1">
                        <Container>
                            <Row>
                                <Col>
                                    <CreateALeague />
                                </Col>
                                <Col>
                                    <JoinALeague />
                                </Col>
                            </Row>
                        </Container>
                    </AccordionBody>
                </AccordionItem>
                <AccordionItem>
                    <AccordionHeader targetId="2">
                        My Leagues
                    </AccordionHeader>
                    <AccordionBody accordionId="2">
                        <MyLeagues />
                    </AccordionBody>
                </AccordionItem>
            </Accordion>
        </>
    )
}

export default Leagues;