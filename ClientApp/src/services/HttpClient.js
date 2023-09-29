import AuthInterceptor from "./AuthInterceptor";

const HttpClient = {
    Post: async function(payload, route) {

            const bearer = AuthInterceptor.GetBearer();

            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json', 'authorization' : bearer },
                body: JSON.stringify(payload)
            };
            return fetch(process.env.REACT_APP_API_URL + route, requestOptions);
    },
    PostNoAuth : async function(payload, route) {

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        };
        return fetch(process.env.REACT_APP_API_URL + route, requestOptions);
    }
}
export default HttpClient;