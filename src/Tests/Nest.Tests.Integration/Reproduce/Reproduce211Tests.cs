using System.Collections.Generic;
using NUnit.Framework;
using System.Diagnostics;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce211Tests : IntegrationTests
	{
		public class Post
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}


		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/211
		/// </summary>
		[Test]
		public void NoSearchResults()
		{
			//test teardown will delete defaultindex_* indices
 			//process id makes it so we can run these tests concurrently using NCrunch
			var index = ElasticsearchConfiguration.DefaultIndex + "_posts_" + Process.GetCurrentProcess().Id.ToString();

			var list = new List<Post>();
			for (int i = 0; i < 10; i++)
			{
				var post = new Post() { Id = 12 + i, Name = "tdd" };
				list.Add(post);
			}
			this._client.IndexMany(list, index, "post");

			//indexing is NRT so issueing a search 
			//right after indexing might not return the documents just yet.
			var refreshResult = this._client.Refresh(i=>i.Index(index));
			Assert.True(refreshResult.IsValid);
			    
			var results = this._client.Search<Post>(s => s
				.Index(index)
				//by default nest will infer the typename for Post
				//by lowercasing it and pluralizing it
				//since we said the type is "post" we now have to explicitly 
				//pass it to elasticsearch.
				.Type("post") 
				.MatchAll()
			);
			results.IsValid.Should().Be(true);
			results.Total.Should().BeGreaterThan(0);
		}

	}
}
