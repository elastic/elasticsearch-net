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
			client.Map<Person>(p => p
				.AutoMap() //<1> automatically create the mapping from the type
				.Properties(props => props
					.Keyword(k => k.Name("Initials")) //<2> create an additional keyword field to store the initials
				)
			);

			client.PutPipeline("person-pipeline", p => p
				.Processors(ps => ps
					.Uppercase<Person>(u => u
						.Field(t => t.LastName) //<3> uppercase the lastname
					)
					.Script(s => s
						.Lang("painless") //<4> use a painless script to populate the Initials field
						.Source("ctx.Initials = doc['Firstname.keyword'].value.substring(1) + doc['Lastname.keyword'].value.substring(1)"))
				)
			);

			var person = new Person
			{
				Id = 1,
				FirstName = "Martijn",
				LastName = "Laarman"
			};

			var indexResponse = client.Index(person, p => p.Pipeline("person-pipeline")); //<5> index the document using the created pipeline
		}
	}
}
