import HttpClient from "./HttpClient";

const IdentityService =  {
    LoginGoogle: async function(clientId, credential) {
        return HttpClient.Post({ ClientId: clientId, Credential : credential}, 'api/identity/google-login');
    }
}

export default IdentityService;