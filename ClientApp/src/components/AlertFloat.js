import { Toast, ToastBody, ToastHeader } from "reactstrap";
import { useState } from "react";

const AlertFloat = (props) => {

    const [show, setShow] = useState(true);



    return(
        <>
            {show && <div class="col">
                <Toast isOpen={show}>
                    <ToastHeader className="bg-warning" toggle={() => setShow(false)}>
                        You have been logged out
                    </ToastHeader>
                    <ToastBody>
                        Please log back in to continue
                    </ToastBody>
                </Toast>
            </div>}
        </>
    );
}

export default AlertFloat;