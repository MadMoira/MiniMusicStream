export interface Artist {
    id: number;
    name: string;
}

export interface ArtistResponse {
    page: number;
    next: string;
    items: Artist[];
}
