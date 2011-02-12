using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;
using ElasticSearch.Client.DSL;
using System.Threading;


namespace ElasticSearch.ConsolePlayground
{
	class Program
	{
		static void Main(string[] args)
		{

			var connectionSettings = new ConnectionSettings("127.0.0.1.", 9200)
										.SetDefaultIndex("mpdreamz");
			var client = new ElasticClient(connectionSettings);
			ConnectionStatus status;
			if (client.TryConnect(out status))
			{
				var version = client.VersionInfo;
				Console.WriteLine("Connected to ElasticSearch {0} released {1}", version.Number, version.Date);
				/*
				var blogAmmount = 100;
				var blogPosts = StaticData.GetBlogPosts(blogAmmount);

				



				blogPosts.ForEachWithIndex((b,i)=>
				{
					Console.Write("\rIndexing blog post {0} out of {1}", i + 1, blogAmmount);	
					client.Index(b);
				});
				Console.WriteLine("\nDone indexing {0} items, \nSleeping 3 seconds to catch up with ES Near real time indexing.", blogAmmount);
												
				Thread.Sleep(3000); //If ES is NRT 3 seconds ought to be sufficient right ?
				
				Console.WriteLine("Getting blog post 66 as stored in ES..");
				var originalPost = blogPosts[66];
				var post = client.Get<Blog>(66);
				var isEqual = originalPost.Title == post.Title;
				Console.WriteLine("Blog 66 in memory is {0} to blog post 66 as indexed in ES.", (isEqual) ? "equal" : "not equal");
								
				
				*/
				var post = client.Get<Blog>(66);
				string firstName = post.Author.FirstName;
				string lastName = post.Author.LastName;

				var q = Query<Blog>
							.Fuzzy(b => b.Author.FirstName, firstName)
							.AndFuzzy(b => b.Author.LastName, lastName);

				var results = client.Search(q);

				QueryResponse<Blog> queryResults = client.Search<Blog>(new Search()
				{
					Query = new Query(new Fuzzy("author.firstName", firstName, 1.0))

				}.Skip(0).Take(10)
				);
				var thisPageCount = queryResults.Documents.Count();

				Console.WriteLine("Found {0} document matching author.FirstName:{1}", queryResults.Total, firstName);
				queryResults.Documents.ForEachWithIndex((d, i) =>
				{
					Console.WriteLine("First page, doc no. {0} entitled {1}", i, d.Title);
				});
			}
			else 
			{
				Console.WriteLine("Error connecting to {0} because:\r\n {1}", connectionSettings.Host, status.Error.ExceptionMessage);

			}

			Console.ReadLine();
		}

		
		
	}
	
	
}
