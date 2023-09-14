using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames
{
    public class VideoGames : IComparable<VideoGames>
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public decimal GlobalSales { get; set; }

        public VideoGames()
        {
            this.Name = string.Empty;
            this.Platform = string.Empty;
            this.Year = 0;
            this.Genre = string.Empty;
            this.Publisher = string.Empty;
            this.GlobalSales = 0;

        }

        public VideoGames (string name, string platform, int year, string genre, string publisher, decimal globalSales)
        {
            Name = name;
            Platform = platform;
            Year = year;
            Genre = genre;
            Publisher = publisher;
            GlobalSales = globalSales;

        }

        // Returns all the names in alphebetical order 
        public int CompareTo(VideoGames? other)
        {
            return Name.CompareTo(other.Name);

        }

        // The lot of information for the gaming stats converted to a to string
        public override string ToString()
        {
            string gamingString = "";
            gamingString += $"Name: {Name}\n";
            gamingString += $"Platform: {Platform}\n";
            gamingString += $"Year: {Year}\n";
            gamingString += $"Genre: {Genre}\n";
            gamingString += $"Publisher: {Publisher}\n";
            gamingString += $"Global Sales: {GlobalSales}\n";
            gamingString += "-----------------------------------------------\n";

            return gamingString;


        }
    }
}
