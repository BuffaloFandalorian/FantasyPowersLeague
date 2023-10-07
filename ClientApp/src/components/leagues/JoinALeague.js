import { Card, CardHeader, CardBody, CardText, Button } from "reactstrap";
import { useNavigate } from "react-router-dom";

const JoinALeague = (props) => {
    const navigate = useNavigate();
    const go = () => {
        navigate("/join-league")
    };

    return(
        <>
            <Card color="light">
            <CardHeader>
                Join A League
            </CardHeader>
            <CardBody>
                <CardText>
                    Join an existing league by entering a code provided by your manager.  You may have to ask for your email to be added to league roster.
                </CardText>
                <Button onClick={go}>
                    Go
                </Button>
            </CardBody>
        </Card>
        </>
    )
}

export default JoinALeague;