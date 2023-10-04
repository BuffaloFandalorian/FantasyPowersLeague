import AuthInterceptor from "./AuthInterceptor";

const HttpClient = {
    Post: async function(payload, route) {

            const bearer = await AuthInterceptor.GetBearer();

            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json', 'authorization' : `Bearer ${bearer}` },
                body: JSON.stringify(payload)
            };
            return fetch(process.env.REACT_APP_API_URL + route, requestOptions);
    },
    PostRawAuth: async function(payload, route) {
        const token = window.sessionStorage.getItem('token');
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', 'authorization' : `Bearer ${token}` },
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
    },
    Get: async function(route) {
        const bearer = await AuthInterceptor.GetBearer();

        const requestOptions = {
            method: 'GET',
            headers: { 'Content-Type': 'application/json', 'authorization' : `Bearer ${bearer}` }
        };
        return fetch(process.env.REACT_APP_API_URL + route, requestOptions);
    },
    GetNoAuth: async function(route) {

        const requestOptions = {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        };
        return fetch(process.env.REACT_APP_API_URL + route, requestOptions);
    }
}
export default HttpClient;