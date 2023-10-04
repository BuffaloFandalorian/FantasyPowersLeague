import { Container } from "reactstrap";
const Alerts = (props) => {
    return (
        <>
        <div class="row row-cols-4">
            {props.Alerts}
        </div>
        </>
    )
}

export default Alerts;