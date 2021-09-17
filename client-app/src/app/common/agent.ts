import axios, {AxiosResponse} from "axios";
import ArtistResponse from "../models/Artist";

axios.defaults.baseURL = "http://localhost:5000";

const responseBody = <T>(response: AxiosResponse) => response.data;

const Library = {
    getArtists: () => axios.get<ArtistResponse>('api/v1/Library/artists').then(responseBody)
};

const agent = {
    Library
};

export default agent;
