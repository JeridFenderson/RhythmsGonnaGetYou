using System;
using System.Linq;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BrokenRecordLabelContext();
            var bands = context.Bands;
            var albums = context.Albums;
            var songs = context.Songs;
            Console.WriteLine($"There are {bands.Count()} bamds!");
            Console.WriteLine($"There are {albums.Count()} albums!");
            Console.WriteLine($"There are {albums.Count()} songs!");

        }
    }
}
