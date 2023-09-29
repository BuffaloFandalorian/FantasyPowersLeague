import HttpClient from "./HttpClient";

const IdentityService =  {
    LoginGoogle: async function(clientId, credential) {
        const response = await HttpClient.PostNoAuth({ ClientId: clientId, Credential : credential}, 'api/identity/google-login');
        if(response.status == 200){
            const loginResult = await response.json();
            window.sessionStorage.setItem("token", loginResult.token);
            window.sessionStorage.setItem("expiration", loginResult.expiration);
            window.sessionStorage.setItem("refreshToken", loginResult.refreshToken);
            return true;
        }
        else{
            return false;
        }
    }
}

export default IdentityService;