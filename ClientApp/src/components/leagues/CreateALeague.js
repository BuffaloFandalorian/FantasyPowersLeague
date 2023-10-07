import { Card, CardHeader, CardBody, CardText, Button } from "reactstrap";
import { useNavigate } from "react-router-dom";

const CreateALeague = (props) => {
    const navigate = useNavigate();
    const go = () => {
        navigate("/new-league")
    };
    return (<>
        <Card color="light">
            <CardHeader>
                Create A League
            </CardHeader>
            <CardBody>
                <CardText>
                    Create a new league as the manager.
                </CardText>
                <Button onClick={go}>
                    Go
                </Button>
            </CardBody>
        </Card>
    </>);
}

export default CreateALeague;