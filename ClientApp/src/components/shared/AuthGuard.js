import IdentityService from "../../services/IdentityService";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import constants from "../../models/constants";

const AuthGuard = (props) => {

    const navigate = useNavigate();

    useEffect(()=> {
        const checkAuth = async () =>{
            //simple check
            if(window.sessionStorage.getItem("loggedIn") != constants.LoginState.LoggedIn){
                props.unauthorizedCallback();
            }

            const isAuthenticated = await IdentityService.Auth();

            if(isAuthenticated == null){
                window.sessionStorage.clear();
                props.unauthorizedCallback();
            }

            if(!isAuthenticated){
                props.unauthorizedCallback();
            }
            console.log("isAuthenticated", isAuthenticated);
        };
        checkAuth();
    }, [""]);

    return (
        <>
            {props.component}
        </>
    );
};

export default AuthGuard;