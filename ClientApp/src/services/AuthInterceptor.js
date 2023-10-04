import IdentityService from "./IdentityService";

const AuthInterceptor = {
    GetBearer : async function () {
        //check for expiration
        const currentExpirationDate = window.sessionStorage.getItem("expiration");
        const currentDate = new Date();
        const nowTime = currentDate.getTime() / 1000;

        if(nowTime > currentExpirationDate - process.env.REACT_APP_TOKEN_REFRESH_SECONDS)
        {
            const refresh = await IdentityService.RefreshToken();
            return refresh;
        }
        else
        {
            console.log(`${currentExpirationDate - (nowTime + process.env.REACT_APP_TOKEN_REFRESH_SECONDS)}`)
            return window.sessionStorage.getItem("token");
        }
    }
}

export default AuthInterceptor;