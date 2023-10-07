import { Card, CardHeader, CardBody, CardText, Form, Input, Label, FormText, FormGroup, Button, FormFeedback } from "reactstrap";
import { useState, useRef, useEffect } from "react";
import LeagueService from "../../../../services/LeagueService";

const PickEmNewLeague = (props) => {

    const [name, setName] = useState("");
    const [nameInvalid, setNameInvalid] = useState(false);
    const [nameValidationMessage, setNameValidationMessage] = useState("");

    const [description, setDescription] = useState("");
    const [descriptionInvalid, setDescriptionInvalid] = useState(false);
    const [descriptionValidationMessage, setDescriptionValidationMessage] = useState("");
    

    const validateAndSubmit = async () => {
        
        var valid = true;
        
        if(name === ""){
            setNameValidationMessage("Need a name for the league there bud.");
            setNameInvalid(true);
            valid = false;
        }

        if(name.length > 50){
            setNameValidationMessage("Name too long.  Relax.");
            setNameInvalid(true);
            valid = false;
        }

        if(description === ""){
            setDescriptionValidationMessage("Need a name for the description there pal.");
            setDescriptionInvalid(true);
            valid = false;
        }
        else{
            setDescriptionInvalid(false);
        }

        if(valid){
            await LeagueService.CreatePickEmLeague({ leagueType : 0, name : name, description : description});
        }
    };

    const descriptionValidation = event => {
        setDescription(event.target.value);
    };


    const nameValidation = event => {
        setName(event.target.value);
        if(name.length > 50){
            setNameValidationMessage("Name too long.  Relax.");
            setNameInvalid(true);
        }else{
            setNameInvalid(false);
        }
    };

    return (
        <>
            <Card color="primary" inverse>
                <CardHeader>
                    New Pick Em League
                </CardHeader>
                <CardBody>
                    <Card color="light">
                        <CardHeader>
                            Settings
                        </CardHeader>
                        <CardBody>
                            <CardText>
                                <Form>
                                    <FormGroup>
                                        <Label for="leagueName">
                                            Name
                                        </Label>
                                        <Input
                                            id="leagueName" value={name} onChange={nameValidation} invalid={nameInvalid}></Input>
                                        <FormText>
                                            Name of the league.
                                        </FormText>
                                        <FormFeedback>
                                            {nameValidationMessage}
                                        </FormFeedback>
                                    </FormGroup>
                                    <FormGroup>
                                        <Label for="description">
                                            Description
                                        </Label>
                                        <Input
                                            id="leagueName" value={description} onChange={descriptionValidation} invalid={descriptionInvalid}></Input>
                                        <FormText>
                                            Description of the league.
                                        </FormText>
                                        <FormFeedback>
                                            {descriptionValidationMessage}
                                        </FormFeedback>
                                    </FormGroup>
                                    <FormGroup switch>
                                        <Label for="retroactive" check>
                                            Retroactive
                                        </Label>
                                        <Input type="switch"
                                            role="switch"
                                            id="retroactive"></Input>
                                        <FormText>
                                            &nbsp;Allow users to go back and enter thier picks if we're mid season.  Otherwise pick up now.  As an admin you can go in a fill results.
                                        </FormText>
                                    </FormGroup>
                                    <FormGroup switch>
                                        <Label for="requireApproval" check>
                                            Require Approval
                                        </Label>
                                        <Input type="switch"
                                            role="switch"
                                            id="requireApproval"></Input>
                                        <FormText>
                                            &nbsp;If you don't require approval anybody can join the league instantly who has the Access Code.  Otherwise you'll need to approve the user once they ask to join.
                                        </FormText>
                                    </FormGroup>
                                    <Button onClick={async () => { await validateAndSubmit() }}>
                                        Create
                                    </Button>
                                </Form>
                            </CardText>
                        </CardBody>
                    </Card>
                </CardBody>
            </Card >
        </>
    );
}

export default PickEmNewLeague;