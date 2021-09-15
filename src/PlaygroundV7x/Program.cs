using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace PlaygroundV7x
{
	internal class Program
	{
		private static async Task Main()
		{
			var matchQueryOne = Query<Person>.Match(m => m.Field(f => f.FirstName).Query("Steve"));
			var matchQueryTwo = new QueryContainer(new MatchQuery() { Field = Infer.Field<Person>(f => f.FirstName), Query = "Steve" });
			var matchQueryThree = new QueryContainerDescriptor<Person>().Match(m => m.Field(f => f.FirstName).Query("Steve"));

			var client = new ElasticClient();
			
			var indexName = Guid.NewGuid().ToString();

			// Create an index
			var createResponse = await client.Indices.CreateAsync(new CreateIndexRequest(indexName)
			{
				Mappings = new TypeMapping
				{
					DateDetection = false,
					Properties = new Properties
					{
						{"age", new NumberProperty(NumberType.Integer)},
						{"name", new TextProperty()},
						{"email", new KeywordProperty()}
					},
					Meta = new Dictionary<string, object>()
					{
						{ "foo", "bar" }
					}
				}
			});

			//var intervalsQuery = new IntervalsQuery()
			//{
			//	Match = new IntervalsMatch()
			//	{

			//	},
			//	AllOf = new IntervalsAllOf()
			//	{

			//	}
			//}
		}
	}
}
