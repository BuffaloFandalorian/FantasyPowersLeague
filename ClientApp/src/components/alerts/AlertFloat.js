import { Toast, ToastBody, ToastHeader, Col } from "reactstrap";
import { useState } from "react";

const AlertFloat = (props) => {

    const [show, setShow] = useState(true);



    return(
        <>
            {show && <Col>
                <Toast isOpen={show}>
                    <ToastHeader className="bg-warning" toggle={() => setShow(false)}>
                        You have been logged out
                    </ToastHeader>
                    <ToastBody>
                        Please log back in to continue
                    </ToastBody>
                </Toast>
                <br />
            </Col>}
        </>
    );
}

export default AlertFloat;