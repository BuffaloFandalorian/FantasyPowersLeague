import { Container, Row, Col, Accordion, AccordionHeader, AccordionBody, AccordionItem } from "reactstrap";
import { useState, useEffect } from "react";
import LeagueTypeDropdown from "./LeagueTypeDropdown";
import PickEmNewLeague from "./pickem/PickEmNewLeague";

const NewLeagueBuilder = () =>{

    const [leagueType, setLeagueType] = useState();
    const [currentEditor, setCurrentEditor] = useState([]);

    useEffect(()=>{
        
      });

    const leagueTypeChangeCallback = (selectedItem) => {
        setLeagueType(selectedItem);
        onLeagueTypeChange(selectedItem);
    }

    const onLeagueTypeChange = (selectedItem) => {
        switch(selectedItem){
            case '0':
                console.log("Pick Em");
                setCurrentEditor([<PickEmNewLeague />]);
                break;
            default:
                setCurrentEditor(null);
        }
    };

    return(
        <>
        <Container>
            <Row>
                <Col>
                    <LeagueTypeDropdown selectedChangeCallback={leagueTypeChangeCallback} />
                </Col>
            </Row>
            <Row>
                <Col>
                    {currentEditor}
                </Col>
            </Row>
        </Container>
        </>
    )
}

export default NewLeagueBuilder;