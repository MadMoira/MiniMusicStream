interface Artist {
    id: number;
    name: string;
}

interface ArtistResponse {
    page: number;
    next: string;
    items: Artist[];
}

export default ArtistResponse;
