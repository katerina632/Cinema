using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8__InterfaceBase
{
    enum Genre { comedy, horror, action, detective, drama, thriller }

    class Director : ICloneable
    {
        public string Name { get; set; }
        public string SurName { get; set; }

        public int Age { get; set; }

        public Director()
        {
            Name = "No name";
            SurName = "No surname";
            Age = 0;
        }

        public Director(string n, string sn, int y)
        {
            Name = n;
            SurName = sn;
            Age = y;

        }

        public override string ToString()
        {
            return $"{Name} {SurName} - {Age} year\n";                  
        }

        public object Clone()
        {     

            return (Director)this.MemberwiseClone();
        }
    };
    

    class Cinema : IEnumerable
    {
        public string Address { get; set; }
        Movie[] movies;

        public Cinema()
        {
            Address = "No address";
            movies = new Movie[]
            {

                new Movie("Get Smart","USA", new Director("Peter", "Segal",58 ), Genre.comedy, 2008, 6.6),
                new Movie("The Prestige","USA", new Director("Christopher", "Nolan",50 ), Genre.thriller, 2006, 8.5),
                new Movie("The Exorcism of Emily Rose","USA", new Director("Scott", "Derrickson",54 ), Genre.horror, 2005, 6.5),


            };
        }
        public int NumberOfMovies
        {
            get 
            {
                return movies.Length;
            }
        }

        public Cinema(string address) : this()
        {
            Address = address;
        }

        public void Sort()
        {
            Array.Sort(movies);
        }

        public void Sort(IComparer<Movie> comparer)
        {
            Array.Sort(movies, comparer);
        }

        public IEnumerator GetEnumerator()
        {
            return movies.GetEnumerator();
        }

        public override string ToString()
        {
            return $"Address of the cinema: {Address}\n" +
                $"The number of movies currently being broadcast: {NumberOfMovies}";
        }

    }

    class Movie : IComparable<Movie>, ICloneable
    {
        public string Title { get; set; }
        public string Country { get; set; }

        Director director;
        public Genre Genre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }

        public Movie()
        {
            Title = "No title";
            Country = "Nocountry";
            director = new Director();
            Genre = 0;
            Year = 0;
            Rating = 0;
        }

        public Movie(string t, string c, Director d, Genre g, int y, double r)
        {
            Title = t;
            Country = c;
            director = d;
            Genre = g;
            Year = y;
            Rating = r;
        }

        public override string ToString()
        {
            return $"Title: {Title}\n" +
                $"Country: {Country}\n" +
                $"Director: {director}" +
                $"Genre: {Genre}\n" +
                $"Year: {Year}\n" +
                $"Rating: {Rating}\n";
        }

       public int CompareTo(Movie other)
        {
            return this.Title.CompareTo(other.Title);
        }


        public object Clone()
        {
            Movie copy = (Movie)this.MemberwiseClone();
            copy.director = (Director)this.MemberwiseClone();

            return copy; ;
        }
    }

    class ComapareByRating : IComparer<Movie>
    {
        int IComparer<Movie>.Compare(Movie x, Movie y)
        {
            return x.Rating.CompareTo(y.Rating);
        }
    }

    class ComapareByYear : IComparer<Movie>
    {
        int IComparer<Movie>.Compare(Movie x, Movie y)
        {
            return x.Year.CompareTo(y.Year);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Cinema cinema = new Cinema("Soborna St. 10");
            Console.WriteLine (cinema.ToString());
            Console.WriteLine();

            Console.WriteLine(new string('-', 20));
            foreach (var movie in cinema)
            {
                Console.WriteLine(movie);
            }
            Console.WriteLine(new string('-', 20));


            cinema.Sort();

            Console.WriteLine("====Sorted by title=====");
            foreach (var movie in cinema)
            {
                Console.WriteLine(movie);
            }
            Console.WriteLine(new string('-', 20));

            cinema.Sort(new ComapareByRating());
            Console.WriteLine("====Sorted by Rating=====");
            foreach (var movie in cinema)
            {
                Console.WriteLine(movie);
            }
            Console.WriteLine(new string('-', 20));


            cinema.Sort(new ComapareByYear());
            Console.WriteLine("====Sorted by Year=====");
            foreach (var movie in cinema)
            {
                Console.WriteLine(movie);
            }
            Console.WriteLine(new string('-', 20));
        }

    }
}

