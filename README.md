# Album Reviews

An API with CRUD actions for reviewing music albums and artist data.

## Endpoints

- `GetArtists`
- `GetArtistByID`
- `AddArtist`
- `UpdateArtist`
- `DeleteArtist`

## Basic Undertstanding

The controller layer opens up API endpoints, e.g. `/api/getartists/2`

The service layer is utlized by each controller to handle business logic and access the database safely.
e.g. if `inludeAlbums` is true, handle the response accordingly.

The database is seeded with hardcoded data.

## ToDo

Add tests for EVERYTHING!
