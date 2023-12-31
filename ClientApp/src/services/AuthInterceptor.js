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

            if(refresh == null){
                window.sessionStorage.clear();
                return null;
            }

            if(refresh.status !== null){
                return refresh;
            }
            else{
                window.sessionStorage.clear();
                return null;
            }
        }
        else
        {
            return window.sessionStorage.getItem("token");
        }
    }
}

export default AuthInterceptor;