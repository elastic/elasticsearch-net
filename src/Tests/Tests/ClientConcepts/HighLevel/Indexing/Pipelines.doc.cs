using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.Caching
{
	/**[[ingest-pipelines]]
	*=== Ingest Pipelines
	*
	* Elasticsearch will automatically re-route index requests to ingest nodes,
	* however with some careful consideration you can optimise this path.
	*/
	public class IngestPipelines : DocumentationTestBase
	{
		private readonly IElasticClient client = new ElasticClient(new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), new InMemoryConnection()));

		public class Person
		{
			public int Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
		}

		/**
		* ==== Create an ingestion pipeline
		* Assuming we are indexing Person documents, we can create an ingestion pipeline that manipulates the
		* incoming values before they are indexed.
		*
		* Lets assume that our application always expects surnames to be capitalised, and for initials to
		* be indexed into their own field.
		*
		* We could acheive this requirement by creating a custom mapping and creating an ingest pipeline.
		* The Person type can then be used as-is, without making any changes.
		*/
		public async Task IngestionPipeline()
		{
			client.CreateIndex("people");

			client.Map<Person>(p => p
				.Index("people")
				.AutoMap() //<1> automatically create the mapping from the type
				.Properties(props => props
						.Keyword(k => k.Name("initials")) //<2> create an additional field to store the initials
				)
			);

			client.PutPipeline("person-pipeline", p => p
				.Processors(ps => ps
					.Uppercase<Person>(u => u
							.Field(t => t.LastName) //<3> uppercase the lastname
					)
					.Script(s => s
						.Lang("painless") //<4> use a painless script to populate the new field
						.Source("ctx.initials = ctx.firstName.substring(0,1) + ctx.lastName.substring(0,1)"))
				)
			);

			var person = new Person
			{
				Id = 1,
				FirstName = "Martijn",
				LastName = "Laarman"
			};

			var indexResponse = client.Index(person, p => p.Index("people").Pipeline("person-pipeline")); //<5> index the document using the created pipeline
		}

		/**
		* ==== Increasing timeouts
		* When a pipeline is specified, there will be the added overhead of document enrichment when indexing, the example given above, the excution
		* of the uppercasing and the painless script.
		*
		* For large bulk requests, it could be prudent to increase the default indexing timeout to avoid exceptions.
		*/
		public async Task IncreasingTimeouts()
		{
			client.Bulk(b => b
				.Index("people")
				.Pipeline("person-pipeline")
				.Timeout("5m") //<1> increases the bulk timeout to 5 minutes
				.Index<Person>(/*snip*/)
				.Index<Person>(/*snip*/)
				.Index<Person>(/*snip*/)
				.RequestConfiguration(rc => rc
						.RequestTimeout(TimeSpan.FromMinutes(5)) //<2> increases the request timeout to 5 minutes
				)
			);
		}
	}
}
