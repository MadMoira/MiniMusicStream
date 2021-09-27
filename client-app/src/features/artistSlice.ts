import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import agent from "../app/api/agent";
import { Artist } from "../app/models/Artist";

export interface ArtistsState {
    artists: Artist[];
}

const initialState: ArtistsState = {
    artists: [],
}

export const fetchAllArtists = createAsyncThunk(
    'library/fetchArtists',
    async (thunkAPI) => {
        const response = await agent.Library.getArtists()
        return response.items
    }
)

export const artistSlice = createSlice({
    name: 'artists',
    initialState,
    reducers: {
        cleanUp: (state) => state = initialState
    },
    extraReducers: (builder) => {
        builder.addCase(fetchAllArtists.fulfilled, (state, action) => {
            state.artists.push(...action.payload)
        })
    }
})

export const { cleanUp } = artistSlice.actions
export default artistSlice.reducer;
