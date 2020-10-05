// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;
using Nest;
using Tests.Framework;
using Tests.Framework.DocumentationTests;

namespace Tests.ClientConcepts.HighLevel.Indexing
{
	/**[[pipelines]]
	 *=== Ingest Pipelines
	 *
	 * An ingest pipeline is a series of processors that are to be executed in the same order as they are declared.
	 *
	 * Let's work with the following POCOs
	*/
	public class IngestPipelines : DocumentationTestBase
	{
		private readonly IElasticClient client = new ElasticClient(new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), new InMemoryConnection()));

		public class Person
		{
			public int Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string IpAddress { get; set; }
			public GeoIp GeoIp { get; set; }
		}

		public class GeoIp
		{
			public string CityName { get; set; }
			public string ContinentName { get; set; }
			public string CountryIsoCode { get; set; }
			public GeoLocation Location { get; set; }
			public string RegionName { get; set; }
		}

		/**
		* ==== Create an ingestion pipeline
		* Assuming we are indexing Person documents, we can create an ingestion pipeline that manipulates the
		* incoming values before they are indexed.
		*
		* Lets assume that our application always expects surnames to be capitalised, and for initials to
		* be indexed into their own field. We also have an IP address that we'd like to convert into
		* a human-readable location.
		*
		* We could achieve this requirement by creating a custom mapping and creating an ingest pipeline.
		* The Person type can then be used as-is, without making any changes.
		*/
		public void IngestionPipeline()
		{
			client.Indices.Create("people", c => c
				.Map<Person>(p => p
					.AutoMap() //<1> automatically create the mapping from the type
					.Properties(props => props
							.Keyword(t => t.Name("initials")) //<2> create an additional field to store the initials
							.Ip(t => t.Name(dv => dv.IpAddress)) //<3> map field as IP Address type
							.Object<GeoIp>(t => t.Name(dv => dv.GeoIp)) //<4> map GeoIp as object
					)
				)
			);

			client.Ingest.PutPipeline("person-pipeline", p => p
				.Processors(ps => ps
					.Uppercase<Person>(s => s
						.Field(t => t.LastName) //<5> uppercase the lastname
					)
					.Script(s => s
						.Lang("painless") //<6> use a painless script to populate the new field
						.Source("ctx.initials = ctx.firstName.substring(0,1) + ctx.lastName.substring(0,1)")
					)
					.GeoIp<Person>(s => s //<7> use ingest-geoip plugin to enrich the GeoIp object from the supplied IP Address
						.Field(i => i.IpAddress)
						.TargetField(i => i.GeoIp)
					)
				)
			);

			var person = new Person
			{
				Id = 1,
				FirstName = "Martijn",
				LastName = "Laarman",
				IpAddress = "139.130.4.5"
			};

			var indexResponse = client.Index(person, p => p.Index("people").Pipeline("person-pipeline")); //<8> index the document using the created pipeline
		}

		/**
		* ==== Increasing timeouts
		* When a pipeline is specified, there will be the added overhead of document enrichment when indexing, the example given above, the execution
		* of the uppercasing and the Painless script.
		*
		* For large bulk requests, it could be prudent to increase the default indexing timeout to avoid exceptions.
		*/
		public void IncreasingTimeouts()
		{
			client.Bulk(b => b
				.Index("people")
				.Pipeline("person-pipeline")
				.Timeout("5m") //<1> increases the server-side bulk timeout
				.Index<Person>(/*snip*/)
				.Index<Person>(/*snip*/)
				.Index<Person>(/*snip*/)
				.RequestConfiguration(rc => rc
				    .RequestTimeout(TimeSpan.FromMinutes(5)) //<2> increases the HTTP request timeout
				)
			);
		}
	}
}
