const HttpClient =  {
    Post: async function(payload, route) {

            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            };

            console.log(process.env);

            return fetch(process.env.REACT_APP_API_URL + route, requestOptions);
    }
}

export default HttpClient;