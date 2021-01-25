using System;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static string Menu()
        {
            Console.WriteLine("\n'ADD BAND' - Add a new band");
            Console.WriteLine("'ADD ALBUM' - Add a new album to a band");
            Console.WriteLine("'ADD SONG' - Add a new song to an album");
            Console.WriteLine("'VIEW BANDS' - View all the bands in the database");
            Console.WriteLine("'VIEW SIGNED BANDS' - View all the bands in the database that are signed");
            Console.WriteLine("'VIEW UNSIGNED BANDS' - View all bands in the database that are not signed");
            Console.WriteLine("'VIEW ALBUMS' - View all albums in the database ordered by their release date");
            Console.WriteLine("'VIEW BANDS ALBUMS' - View all albums and songs associated with a particular band name");
            Console.WriteLine("'SIGN BAND' - Sign a particular band to Broken Records Label");
            Console.WriteLine("'RELEASE BAND' - Release a particular band from Broken Records Label");
            Console.WriteLine("EXIT - Exit The Broken Records Label's application\n");
            Console.Write("What would you like to do? ");
            var choice = Console.ReadLine().Trim().ToUpper();
            Console.WriteLine();
            return choice;
        }


        static void Banner(string message)
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(message);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        }


        static string GetDetails(string property, string noun, string verb)
        {
            if (noun == "band" || noun == "album" || noun == "song")
            {
                Console.Write($"\nWhat is the {property} of the {noun} you'd like to {verb}? ");
            }
            else
            {
                Console.Write($"\nWhat is {noun}'s {property}? ");
            }
            return Console.ReadLine();
        }


        static int GetMemberAmount()
        {
            Console.Write($"\nHow many members are there? ");
            var numberToBeParsed = Console.ReadLine();
            var parsedNumber = 0;
            bool wasThisParsable = int.TryParse(numberToBeParsed, out parsedNumber);
            while (!wasThisParsable)
            {
                Console.Write("\nNot a number. Please enter a number only: ");
                numberToBeParsed = Console.ReadLine();
                wasThisParsable = int.TryParse(numberToBeParsed, out parsedNumber);
                while (parsedNumber < 0)
                {
                    Console.Write("\nPlease enter a positive number only: ");
                    numberToBeParsed = Console.ReadLine();
                    wasThisParsable = int.TryParse(numberToBeParsed, out parsedNumber);
                }
            }
            return parsedNumber;
        }


        static ulong GetContactPhoneNumber()
        {
            Console.Write($"\nWhat's the best contact phone number? ");
            var numberToBeParsed = Console.ReadLine();
            ulong parsedNumber = 0;
            bool wasThisParsable = ulong.TryParse(numberToBeParsed, out parsedNumber);
            while (!wasThisParsable)
            {
                Console.Write("\nNot a phone number. Please enter a number only: ");
                numberToBeParsed = Console.ReadLine();
                wasThisParsable = ulong.TryParse(numberToBeParsed, out parsedNumber);
            }
            return parsedNumber;
        }


        static DateTime GetReleaseDate()
        {
            Console.Write($"\nWhat's the album's release date? ");
            string dateToBeParsed;
            bool dateFormatted;
            do
            {
                dateToBeParsed = Console.ReadLine();
                try
                {
                    var dateTime = DateTime.Parse(dateToBeParsed);
                    dateFormatted = true;
                }
                catch (FormatException)
                {
                    Console.Write("\nDate not understood, please use a different format: ");
                    dateFormatted = false;

                }
            } while (!dateFormatted);

            return DateTime.Parse(dateToBeParsed);
        }


        static void AddBand()
        {
            var context = new BrokenRecordLabelContext();
            string moreBandsToAdd;
            do
            {
                var newBand = new Band();
                newBand.Name = GetDetails("name", "band", "add");
                newBand.NumberOfMembers = GetMemberAmount();
                newBand.CountryOfOrigin = GetDetails("country of origin", newBand.Name, "");
                newBand.Website = GetDetails("website", newBand.Name, "");
                newBand.Style = GetDetails("style", newBand.Name, "");
                string isSigned;
                do
                {
                    Console.Write($"Is {newBand.Name} signed? 'YES' or 'NO': ");
                    isSigned = Console.ReadLine().ToUpper().Trim();
                } while (isSigned != "YES" && isSigned != "NO");
                if (isSigned == "YES")
                {
                    newBand.IsSigned = true;
                }
                else
                {
                    newBand.IsSigned = false;
                }
                newBand.ContactName = GetDetails("contact name", newBand.Name, "");
                Console.Write($"What is {newBand.ContactName}'s number? Numbers only please: ");
                newBand.ContactPhoneNumber = GetContactPhoneNumber();
                context.Bands.Add(newBand);
                Console.Write("\nWould you like to add another band? 'YES' or 'NO': ");
                moreBandsToAdd = Console.ReadLine().ToUpper().Trim();

            } while (moreBandsToAdd == "YES");
            context.SaveChanges();
        }


        static void AddAlbum()
        {
            var context = new BrokenRecordLabelContext();
            string moreAlbumsToAdd;
            do
            {
                var newAlbum = new Album();
                newAlbum.Title = GetDetails("title", "album", "add");
                string isExplicit;
                do
                {
                    Console.Write($"Is {newAlbum.Title} explicit? 'YES' or 'NO': ");
                    isExplicit = Console.ReadLine().ToUpper().Trim();
                } while (isExplicit != "YES" && isExplicit != "NO");
                if (isExplicit == "YES")
                {
                    newAlbum.IsExplicit = true;
                }
                else
                {
                    newAlbum.IsExplicit = false;
                }
                newAlbum.ReleaseDate = GetReleaseDate();

                Console.Write($"\nWho's the band that made {newAlbum.Title}? ");
                var albumBand = context.Bands.FirstOrDefault(band => band.Name == Console.ReadLine());
                newAlbum.BandId = albumBand.Id;
                context.Albums.Add(newAlbum);
                Console.Write("\nWould you like to add another album? 'YES' or 'NO': ");
                moreAlbumsToAdd = Console.ReadLine().ToUpper().Trim();

            } while (moreAlbumsToAdd == "YES");
            context.SaveChanges();
        }


        static void AddSong()
        {
            var context = new BrokenRecordLabelContext();
            string moreSongsToAdd;
            do
            {
                var newSong = new Song();
                newSong.Title = GetDetails("title", "song", "add");
                Console.Write("\nWhat is the song's duration? Proper format is 'hr:mn:sc': ");
                newSong.Duration = Console.ReadLine();
                Console.Write($"\nWho's the album that contains {newSong.Title}? ");
                var songAlbum = context.Albums.FirstOrDefault(album => album.Title == Console.ReadLine());
                var allSongsOnAlbum = context.Songs.Where(song => song.AlbumId == songAlbum.Id);
                newSong.AlbumId = songAlbum.Id;
                newSong.TrackNumber = allSongsOnAlbum.Count() + 1;
                context.Songs.Add(newSong);
                Console.Write("\nWould you like to add another album? 'YES' or 'NO': ");
                moreSongsToAdd = Console.ReadLine().ToUpper().Trim();
            } while (moreSongsToAdd == "YES");
            context.SaveChanges();
        }


        static void ViewBands(bool allBands, bool isSigned)
        {
            var context = new BrokenRecordLabelContext();
            if (allBands)
            {
                foreach (var band in context.Bands)
                {
                    Console.WriteLine($"\n{band.Name}, with {band.NumberOfMembers} members, from {band.CountryOfOrigin}, has a {band.Style} style");
                    Console.WriteLine($"and can be reached at {band.Website} or through {band.ContactName} at {band.ContactPhoneNumber}");
                }
            }
            else
            {
                foreach (var band in context.Bands.Where(band => band.IsSigned == isSigned))
                {
                    Console.WriteLine($"\n{band.Name}, with {band.NumberOfMembers} members, from {band.CountryOfOrigin}, has a {band.Style} style");
                    Console.WriteLine($"and can be reached at {band.Website} through {band.ContactName} at {band.ContactPhoneNumber}");
                }
            }
        }


        static void ViewAlbums(bool allAlbums)
        {
            var context = new BrokenRecordLabelContext();
            if (allAlbums)
            {
                foreach (var album in context.Albums.Include(album => album.Band).OrderBy(album => album.ReleaseDate))
                {
                    Console.Write($"\n{album.Title}, created by {album.Band.Name} ");
                    if (album.IsExplicit)
                    {
                        Console.WriteLine("is explicit");
                    }
                    else
                    {
                        Console.WriteLine(" is not explicit");
                    }
                    Console.WriteLine($"and was released on {album.ReleaseDate}");
                }
            }
            else
            {
                var bandsAlbumsToViewName = GetDetails("name", "band", "that you'd like to see all albums for");
                foreach (var song in context.Songs.Include(song => song.Album).ThenInclude(album => album.Band).Where(song => song.Album.Band.Name == bandsAlbumsToViewName))
                {
                    Console.WriteLine($"\nTrack {song.TrackNumber} - {song.Title} - {song.Duration} long, is found on {song.Album.Title}");
                }
            }
        }


        static void SignUnsign(bool sign)
        {
            var context = new BrokenRecordLabelContext();
            var bandToSignOrUnsignsName = GetDetails("name", "band", "");
            var bandToSignOrUnsign = context.Bands.FirstOrDefault(band => band.Name == bandToSignOrUnsignsName);
            if (sign)
            {
                bandToSignOrUnsign.IsSigned = true;
                Console.WriteLine($"{bandToSignOrUnsign.Name} were successfully signed!");
            }
            else
            {
                bandToSignOrUnsign.IsSigned = false;
                Console.WriteLine($"\n{bandToSignOrUnsign.Name} were successfully released!");
            }
            context.SaveChanges();
        }
        static void Main(string[] args)
        {
            Banner("    Welcome to The Broken Records Label!");
            var context = new BrokenRecordLabelContext();
            var bands = context.Bands;
            var albums = context.Albums.Include(album => album.Band);
            var songs = context.Songs.Include(song => song.Album).ThenInclude(album => album.Band);

            var choice = Menu();
            while (choice != "EXIT")
            {
                switch (choice)
                {
                    case "ADD BAND":
                        AddBand();
                        choice = Menu();
                        break;
                    case "ADD ALBUM":
                        AddAlbum();
                        choice = Menu();
                        break;
                    case "ADD SONG":
                        AddSong();
                        choice = Menu();
                        break;
                    case "VIEW BANDS":
                        ViewBands(true, false);
                        choice = Menu();
                        break;
                    case "VIEW SIGNED BANDS":
                        ViewBands(false, true);
                        choice = Menu();
                        break;
                    case "VIEW UNSIGNED BANDS":
                        ViewBands(false, false);
                        choice = Menu();
                        break;
                    case "VIEW ALBUMS":
                        ViewAlbums(true);
                        choice = Menu();
                        break;
                    case "VIEW BANDS ALBUMS":
                        ViewAlbums(false);
                        choice = Menu();
                        break;
                    case "SIGN BAND":
                        SignUnsign(true);
                        choice = Menu();
                        break;
                    case "RELEASE BAND":
                        SignUnsign(false);
                        choice = Menu();
                        break;
                    default:
                        Console.WriteLine("\nThat's not a menu option, please try again!\n");
                        choice = Menu();
                        break;
                }

            }

            Banner("Thank you for using The Broken Records Label!");
        }
    }
}
