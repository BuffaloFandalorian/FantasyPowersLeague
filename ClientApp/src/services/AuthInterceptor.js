import HttpClient from "./HttpClient";

const AuthInterceptor = {
    GetBearer : function () {
        //check for expiration
        const currentExpirationDate = window.sessionStorage.getItem("expiration");
        const nowTime = Date.now() / 1000;
        if(nowTime > currentExpirationDate)
        {
            //TODO: user session is expired, log them out...
        }
        else if(nowTime + process.env.TOKEN_REFRESH_SECONDS > currentExpirationDate)
        {
            //TODO: we will expire in 2 mintues, refresh the token
        }
        else
        {
            return window.sessionStorage.getItem("token");
        }
    }
}

export default AuthInterceptor;