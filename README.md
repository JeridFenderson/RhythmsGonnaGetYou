# RhythmsGonnaGetYou PEDA

## Problem

Create a database that stores Albums, Bands, and Songs. A Band has many Albums and Album belongs to one Band. An Album has many Songs and a Song belongs to one Album. Create a menu system that shows certain options to the user until they choose to quit your program. The options are as follows:

- Add a new band
- View all the bands
- Add an album for a band
- Add a song to an album
- Let a band go (update isSigned to false)
- Resign a band (update isSigned to true)
- Prompt for a band name and view all their albums
- View all albums ordered by ReleaseDate
- View all bands that are signed
- View all bands that are not signed
- Quit the program

### Adventure Mode

Track the individual members of a band. Since musicians play in several different groups, create a new table called Musicians and make it a many to many relationships with a Band.

- Add the following menu options:
- View albums in a genre
- View all members of a band

### Epic Mode

Add another entity that you feel would benefit the system. Update your ERD, tables, and user interface to support it.

## Examples

## Data

Bands

- Id SERIAL PRIMARY KEY
- Name VARCHAR(45) NOT NULL
- CountryOfOrigin VARCHAR(56)
- NumberOfMembers INT
- Website VARCHAR(65)
- Style VARCHAR (32)
- IsSigned BOOLEAN
- ContactName VARCHAR (21)
- ContactPhoneNumber BIGINT

Albums

- Id SERIAL PRIMARY KEY
- Title VARCHAR (65) NOT NULL
- IsExplicit BOOLEAN
- ReleaseDate DATE
- BandId INT NULL REFERENCES "Bands" ("Id")

Songs

- Id SERIAL PRIMARY KEY
- Title TEXT NOT NULL
- TrackNumber INT
- Duration VARCHAR(8)
- AlbumId INT NULL REFERENCES "Albums" ("Id")

## Algorithm

Once the database is created and filled with data, set up access to the database through the use of ORM.

Set up classes that relate to the tables stored in the database exactly as they are, with appropriate data types assigned to each property.

While user hasn't chosen to quit, allow them to:

- Add a new band. While user wants to add a new band, capture all required band info through Q&A, with validation of data before submitting to database. Continue to add bands to the list of bands while the user wants to do so. After the user has entered all the bands they want, upload all new bands to the database

- Add an album to a band. While user wants to add a new album, capture all required album info through Q&A, with validation of data before submitting to database. Continue to add albums to the list of albums, more specifically to the same band if true, while the user wants to do so. After the user has entered all the albums they want, upload all new albums to the database

- Add a song to an album. While the user wants to add a new album, capture all the required album info through Q&A, with validation of data before submitting to database. Continue to add songs to to the list of songs, more specifically the same album if true, while the user wants to do so. After the user has entered all the songs they want, upload all new songs to the database. Create a method for incrementing the TrackNumber by 1 for each song added to the same track

- View all the bands. Console.WriteLine all the bands in the Bands database, displaying each band's properties

- View all bands that are signed. Console.WriteLine all the bands in the Bands database, displaying each band's properties WHERE IsSigned is true

- View all bands that are not signed. Console.WriteLine all the bands in the Bands database, displaying each band's properties WHERE IsSigned is false

- View all albums ordered by ReleaseDate. Console.WriteLine all the albums in the Albums database, displaying each album's properties JOINED with the Band name, ordered by ReleaseDate

- Prompt for a band name and view all their albums. Ask for a band name, then if the band exists, View all the band's albums ordered by ReleaseDate

- Let a band go. Ask for a band name, then if the band exists, confirm that the user wants to sign band and update IsSigned to true. Upload to server

- Resign a band. Ask for a band name, then if the band exists, confirm that the user wants to release band and update IsSigned to false. Upload to server

- Quit the program
