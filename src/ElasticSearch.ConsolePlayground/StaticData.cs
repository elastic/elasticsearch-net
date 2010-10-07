using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// Some parts copied from the interwebs. Proper recognizition in form of link backs would have been more dignified.
/// To whom it concerns: I am sorry !.

namespace ElasticSearch.ConsolePlayground
{
	internal static class StaticData
	{
		private static readonly Random _rng = new Random();
		private static string _chars = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ";
		public static int RandomNumber(int min, int max)
		{
			return _rng.Next(min, max);
		}
		public static string RandomString(int size)
		{
			char[] buffer = new char[size];

			for (int i = 0; i < size; i++)
			{
				buffer[i] = _chars[_rng.Next(_chars.Length)];
			}
			return new string(buffer);
		}
		public static Func<DateTime> RandomDay()
		{
			DateTime start = new DateTime(1995, 1, 1);
			Random gen = new Random();
			int range = ((TimeSpan)(DateTime.Today - start)).Days;
			return () => start.AddDays(gen.Next(range));
		}
		private static List<string> Names = new List<string>()
		{
			"Erik Rohn",
			"Fernando Hipps",
			"Karina Dierks",
			"Javier Kreisel",
			"Roxie Lagace",
			"Kurt Blann",
			"Roslyn Ishii",
			"Julio Bachicha",
			"Emilia Mccaskey",
			"Harriett Runions",
			"Julio Trapani",
			"Erik Cowher",
			"Clayton Guadarrama",
			"Lakisha Batalla",
			"Ted Shufelt",
			"Ashlee Delamora",
			"Erik Mizzell",
			"Lonnie Gallogly",
			"Noreen Silvestri",
			"Elnora Ostrem",
			"Jamie Mitcham",
			"Hohl Straman",
			"Christian Naron",
			" Margery Holiman",
			"Tyrone Longmire",
			"Maricela Cassel",
			"Serena Parillo",
			"Mathew Robeson",
			"Fernando Flatley",
			"Melisa Stecklein"
		};
		public static string GetRandomName()
		{
			return StaticData.Names[StaticData.RandomNumber(0, StaticData.Names.Count() - 1)];
		}
		
		public static IList<Person> GetPersons(int ammount)
		{
			var persons = new List<Person>();
			var split = new[] { ' ' };
			for (var i = 0; i < ammount; i++)
			{
				var randName = StaticData.GetRandomName();
				var firstName = randName.Split(split).First();
				var lastName = randName.Split(split).Last();
				var p = new Person()
				{
					FirstName = firstName,
					LastName = lastName,
					DateOfBirth = StaticData.RandomDay()()
				};
				persons.Add(p);
			}
			return persons;
		}
		public static IList<Blog> GetBlogPosts(int ammount)
		{
			var persons = StaticData.GetPersons(30);

			var posts = new List<Blog>();
			for (var i = 0; i < ammount; i++)
			{
				var blogPost = new Blog()
				{
					Id = i,
					Body = StaticData.RandomString(StaticData.RandomNumber(10, 1000)),
					Title = StaticData.RandomString(StaticData.RandomNumber(4, 20)),
					Author = persons[StaticData.RandomNumber(0, 29)],
					CreatedOn = StaticData.RandomDay()()
				};
				posts.Add(blogPost);
			}
			return posts;
			
		}
	}
}
