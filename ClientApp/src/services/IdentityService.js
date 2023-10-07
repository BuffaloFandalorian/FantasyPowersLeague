import HttpClient from "./HttpClient";

const IdentityService =  {
    LoginGoogle: async function(clientId, credential) {
        const response = await HttpClient.PostNoAuth({ ClientId: clientId, Credential : credential}, 'api/identity/google-login');
        if(response.status === 200){
            const loginResult = await response.json();
            window.sessionStorage.setItem("token", loginResult.token);
            window.sessionStorage.setItem("expiration", loginResult.expiration);
            window.sessionStorage.setItem("refreshToken", loginResult.refreshToken);
            return true;
        }
        else{
            return false;
        }
    },
    RefreshToken: async function(){
        const token = window.sessionStorage.getItem("refreshToken");
        if(token === null){
            return null;
        }
        else{
            const response = await HttpClient.PostRawAuth({ token : token }, 'api/identity/refresh-token');
            if(response.status === 200){
                const loginResult = await response.json();
                window.sessionStorage.setItem("token", loginResult.token);
                window.sessionStorage.setItem("expiration", loginResult.expiration);
                window.sessionStorage.setItem("refreshToken", loginResult.refreshToken);
                return loginResult.token;
            }
            window.sessionStorage.clear();
        }
    },
    KeepAlive: function(){
        HttpClient.Get('api/identity/keep-alive').then( (response) => {
            if(response.status === 200){
            }
            else{
                window.sessionStorage.clear();
            }
        });
    },
    Auth: async function(){
        const response = await HttpClient.Get('api/identity/keep-alive');
        if(response === null){
            return false;
        }
        if(response.status === 200){
            return true;
        }
        else{
            return false;
        }
    }
}

export default IdentityService;