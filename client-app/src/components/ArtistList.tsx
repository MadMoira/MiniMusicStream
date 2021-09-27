import React, { useEffect, useState } from "react";
import { Artist } from "../app/models/Artist";
import Card from "../common/Card";
import './ArtistList.css';
import { RootState, store } from "../app/stores/store";
import { fetchAllArtists, cleanUp } from "../features/artistSlice";
import { useSelector } from "react-redux";

function ArtistList() {

  let artists: Artist[] = useSelector((state: RootState) => state.artists.artists);

  useEffect(() => {
    store.dispatch(fetchAllArtists());
    return () => {
      store.dispatch(cleanUp());
    }
  }, []);

  return (
    <>
      <h2>Artists</h2>
      <div className="flex flex-wrap">
        {artists.map(artist => (
          <Card key={artist.id} name={artist.name} />
        ))}
      </div>
    </>
  )
}

export default ArtistList;
