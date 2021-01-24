using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BrokenRecordLabelContext();
            var bands = context.Bands;
            var albums = context.Albums.Include(albums => albums.Band);
            var songs = context.Songs.Include(songs => songs.Album);
            Console.WriteLine($"There are {bands.Count()} bands!");
            Console.WriteLine($"There are {albums.Count()} albums!");
            Console.WriteLine($"There are {albums.Count()} songs!");

        }
    }
}
