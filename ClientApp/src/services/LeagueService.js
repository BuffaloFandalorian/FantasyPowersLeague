import HttpClient from "./HttpClient";

const LeagueService = {
    GetLeagueTypes: async function () {
        const response = await HttpClient.GetNoAuth("api/leagues/types");
        if(response.status === 200){
            return response.json();
        }
        else{
            return null;
        }
    },
    CreatePickEmLeague: async function (league) {
        const response = await HttpClient.Post(league, "api/leagues/pick-em");
        if(response.status === 200){
            return response.json();
        }
        else{
            return null;
        }
    }
}

export default LeagueService;