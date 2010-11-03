using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.DSL;
using ElasticSearch.Client;
using System.Threading;


namespace ElasticSearch.ConsolePlayground
{
	class Program
	{
		static void Main(string[] args)
		{
			QueryString qs = new QueryString("Martijn laarman AND Arnhem")
								.SetAllowLeadingWildcard(false)
								.SetFields(new List<Field>
								{
									new Field("firstName", 2.0),
									new Field("city", 0.5)
								})
								.SetFuzzyMinimumSimilarity(0.1);

			var connectionSettings = new ConnectionSettings("127.0.0.1.", 9200)
										.SetDefaultIndex("mpdreamz");
			var client = new ElasticClient(connectionSettings);
			if (client.IsValid)
			{
				var version = client.VersionInfo;
				Console.WriteLine("Connected to ElasticSearch {0} released {1}", version.Number, version.Date);
				
				var blogAmmount = 100;
				var blogPosts = StaticData.GetBlogPosts(blogAmmount);
				
				blogPosts.ForEachWithIndex((b,i)=>
				{
					Console.Write("\rIndexing blog post {0} out of {1}", i + 1, blogAmmount);	
					client.IndexSync(b);
				});
				Console.WriteLine("\nDone indexing {0} items, \nSleeping 3 seconds to catch up with ES Near real time indexing.", blogAmmount);
												
				Thread.Sleep(3000); //If ES is NRT 3 seconds ought to be sufficient right ?
				
				Console.WriteLine("Getting blog post 66 as stored in ES..");
				var originalPost = blogPosts[66];
				var post = client.Get<Blog>(66);
				var isEqual = originalPost.Title == post.Title;
				Console.WriteLine("Blog 66 in memory is {0} to blog post 66 as indexed in ES.", (isEqual) ? "equal" : "not equal");
								
				string lookFor = originalPost.Author.FirstName;
				string query = "{\"query\" : { \"field\" : { \"author.firstName\" : \"" + lookFor + "\" } } }"; //TODO Fluent Query Interface!

				var queryResults = client.Query<Blog>(query);

				var thisPageCount = queryResults.Documents.Count();

				Console.WriteLine("Found {0} document matching author.FirstName:{1}", queryResults.Total, lookFor);
				queryResults.Documents.ForEachWithIndex((d, i) =>
				{
					Console.WriteLine("First page, doc no. {0} entitled {1}", i, d.Title);
				});


				Console.ReadLine();
			}
		}

		
		
	}
	
	
}
