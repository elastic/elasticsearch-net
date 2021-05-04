// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using FluentAssertions;
using Nest;
using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Tests.Core.Client;
using Tests.Core.Client.Settings;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.DocumentationTests;
using Xunit;
using static Tests.Core.Serialization.SerializationTestHelper;
using static Nest.Infer;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	/**[[index-name-inference]]
	*=== Index name inference
	*
	* Many endpoints within the Elasticsearch API expect to receive one or more index names
	* as part of the request, in order to know what index/indices a request should operate on.
	*
	* NEST has a number of ways in which the index name(s) can be specified
	*/
	public class IndexNameInference : DocumentationTestBase
	{
		//hide
		private class ConnectionSettings : Nest.ConnectionSettings
		{
			public ConnectionSettings() : base(new InMemoryConnection())
			{
			}
		}

		/**
		* ==== Default Index name on Connection Settings
		* A default index name can be specified on `ConnectionSettings` using `.DefaultIndex()`.
		* This is the default index name to use, when no other index name can be resolved for a request
		*/
		[U] public void DefaultIndexIsInferred()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("defaultindex"); // <1> set the default index

			var client = new ElasticClient(settings);
			var searchResponse = client.Search<Project>();

			/**
			 * will send a search request to the API endpoint
			 */
			//json
			var expected = "http://localhost:9200/defaultindex/_search";

			//hide
			{
				searchResponse.ApiCall.Uri.GetLeftPart(UriPartial.Path).Should().Be(expected);
				var resolver = new IndexNameResolver(settings);
				var index = resolver.Resolve<Project>();
				index.Should().Be("defaultindex");
			}
		}

		/**
		 * [[index-name-type-mapping]]
		 * ==== Index name for a .NET type
		 * An index name can be mapped for a _Plain Old CLR Object_ (POCO) using `.DefaultMappingFor<T>()` on `ConnectionSettings`
		 */
		[U]
		public void ExplicitMappingIsInferredUsingDefaultMappingFor()
		{
			var settings = new ConnectionSettings()
				.DefaultMappingFor<Project>(m => m
					.IndexName("projects")
				);

			var client = new ElasticClient(settings);
			var searchResponse = client.Search<Project>();

			/**
			 * will send a search request to the API endpoint
			 */
			//json
			var expected = "http://localhost:9200/projects/_search";

			//hide
			{
				searchResponse.ApiCall.Uri.GetLeftPart(UriPartial.Path).Should().Be(expected);
				var resolver = new IndexNameResolver(settings);
				var index = resolver.Resolve<Project>();
				index.Should().Be("projects");
			}
		}

		/**
		 * `.DefaultMappingFor<T>()` can also be used to specify other defaults for a POCO, including
		 * property names, property to use for the document id, amongst others.
		 *
		 * An index name for a POCO provided using `.DefaultMappingFor<T>()` **will take precedence** over
		* the default index name set on `ConnectionSettings`. This way, the client can be configured with a default index to use if no
		* index is specified, and a specific index to use for different POCO types.
		*/
		[U] public void ExplicitMappingTakesPrecedence()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("defaultindex") // <1> a default index to use, when no other index can be inferred
				.DefaultMappingFor<Project>(m => m
					.IndexName("projects") // <2> a index to use when `Project` is the target POCO type
				);

			var client = new ElasticClient(settings);

			var projectSearchResponse = client.Search<Project>();

			/**
			 * will send a search request to the API endpoint
			 */
			//json
			var expected = "http://localhost:9200/projects/_search";

			//hide
			{
				projectSearchResponse.ApiCall.Uri.GetLeftPart(UriPartial.Path).Should().Be(expected);
				var resolver = new IndexNameResolver(settings);
				var index = resolver.Resolve<Project>();
				index.Should().Be("projects");
			}

			/**
			 * but
			 */
			var objectSearchResponse = client.Search<object>();

			/**
			 * will send a search request to the API endpoint
			 */
			//json
			expected = "http://localhost:9200/defaultindex/_search";

			//hide
			objectSearchResponse.ApiCall.Uri.GetLeftPart(UriPartial.Path).Should().Be(expected);
		}

		/**
		* ==== Explicitly specifying Index name on the request
		* For API calls that expect an index name, an index name can be explicitly provided
		* on the request
		*/
		[U] public void ExplicitIndexOnRequest()
		{
			var settings = new ConnectionSettings();
			var client = new ElasticClient(settings);

			var response = client.Search<Project>(s => s
				.Index("some-other-index") //<1> Provide the index name on the request
			);

			/**
			 * will send a search request to the API endpoint
			 */
			//json
			var expected = "http://localhost:9200/some-other-index/_search";

			//hide
			response.ApiCall.Uri.GetLeftPart(UriPartial.Path).Should().Be(expected);
		}

		/** When an index name is provided on a request, it **will take precedence** over the default
		 * index name specified on `ConnectionSettings`, _and_ any index name specified for the POCO
		 * using `.DefaultMappingFor<T>()`. The following example will send a search request
		 * to the same API endpoint as the previous example
		 */
		[U] public void ExplicitIndexOnRequestTakesPrecedence()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("defaultindex")
				.DefaultMappingFor<Project>(m => m
					.IndexName("projects")
				);

			var client = new ElasticClient(settings);

			var response = client.Search<Project>(s => s
				.Index("some-other-index")
			);

			//hide
			{
				var expected = "http://localhost:9200/some-other-index/_search";
				response.ApiCall.Uri.GetLeftPart(UriPartial.Path).Should().Be(expected);
			}
		}

		/** In summary, the order of precedence for determining the index name for a request is
		 *
		 * . Index name specified  on the request
		 * . Index name specified for the generic type parameter in the request using `.DefaultMappingFor<T>()`
		 * . Default index name specified on `ConnectionSettings`
		 *
		 * [IMPORTANT]
		 * --
		 * If no index can be determined for a request that requires an index, the client will throw
		 * an exception to indicate that this is the case.
		 * --
		 */

		//hide
		[U] public void NoIndexThrowsArgumentException()
		{
			var settings = new ConnectionSettings();
			var resolver = new IndexNameResolver(settings);
			var e = Assert.Throws<ArgumentException>(() => resolver.Resolve<Project>());
			e.Message.Should().Contain("Index name is null");
		}

		//hide
		[U] public void ArgumentExceptionBubblesOut()
		{
			var client = new ElasticClient(new ConnectionSettings());
			var e = Assert.Throws<ArgumentException>(() => client.Search<Project>());
		}

		//hide
		[U] public void RoundTripSerializationPreservesCluster()
		{
			Expect("cluster_one:project").WhenSerializing(Index<Project>("cluster_one"));
			Expect("cluster_one:project").WhenSerializing((IndexName)"cluster_one:project");

			Expect("cluster_one:project,x").WhenSerializing(Index<Project>("cluster_one").And("x"));
			Expect("cluster_one:project,x:devs").WhenSerializing(Index<Project>("cluster_one").And<Developer>("x"));
		}

		//hide
		[U] public void ImplicitConversionReadsCluster()
		{
			var i = (IndexName)"cluster_one  :  project  ";
			i.Cluster.Should().Be("cluster_one  ");
			i.Name.Should().Be("  project  ");

			i = (IndexName)"cluster_one:project";
			i.Cluster.Should().Be("cluster_one");
			i.Name.Should().Be("project");

			i = (IndexName)"    ";
			i.Should().BeNull();
		}

		//hide
		[U] public void EqualsValidation()
		{
			var clusterIndex = (IndexName)"cluster_one:p";
			var index = (IndexName)"p";
			Index<Project>("cluster_one").Should().NotBe(Index<Project>("cluster_two"));

			clusterIndex.Should().NotBe(index);
			clusterIndex.Should().Be("cluster_one:p");
			clusterIndex.Should().Be((IndexName)"cluster_one:p");

			Index<Project>().Should().Be(Index<Project>());
			Index<Project>().Should().NotBe(Index<Project>("cluster_two"));
			Index<Project>("cluster_one").Should().NotBe("cluster_one:project");
			Index<Project>().Should().NotBe(Index<Developer>());
			Index<Project>("cluster_one").Should().NotBe(Index<Developer>("cluster_one"));

			Nest.Indices indices1 = "foo,bar";
			Nest.Indices indices2 = "bar,foo";
			indices1.Should().Be(indices2);
			(indices1 == indices2).Should().BeTrue();
		}

		//hide
		[U] public void GetHashCodeValidation()
		{
			var clusterIndex = (IndexName)"cluster_one:p";
			var index = (IndexName)"p";

			clusterIndex.GetHashCode().Should().NotBe(index.GetHashCode()).And.NotBe(0);
			clusterIndex.GetHashCode().Should().Be(((IndexName)"cluster_one:p").GetHashCode()).And.NotBe(0);
			clusterIndex.GetHashCode().Should().Be(((IndexName)"cluster_one:p").GetHashCode()).And.NotBe(0);

			Index<Project>().GetHashCode().Should().Be(Index<Project>().GetHashCode()).And.NotBe(0);
			Index<Project>().GetHashCode().Should().NotBe(Index<Project>("cluster_two").GetHashCode()).And.NotBe(0);
			Index<Project>("cluster_one").GetHashCode().Should().NotBe(Index<Project>("cluster_two").GetHashCode()).And.NotBe(0);
			Index<Project>("cluster_one").Should().NotBe("cluster_one:project").And.NotBe(0);
			Index<Project>().GetHashCode().Should().NotBe(Index<Developer>().GetHashCode()).And.NotBe(0);
			Index<Project>("cluster_one").GetHashCode().Should().NotBe(Index<Developer>("cluster_one").GetHashCode()).And.NotBe(0);

		}
	}
}
