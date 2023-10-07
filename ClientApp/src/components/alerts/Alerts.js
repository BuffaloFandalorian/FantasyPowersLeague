import { Container,Row } from "reactstrap";
const Alerts = (props) => {
    return (
        <>
        <Row class="row row-cols-4">
            {props.Alerts}
        </Row>
        </>
    )
}

export default Alerts;