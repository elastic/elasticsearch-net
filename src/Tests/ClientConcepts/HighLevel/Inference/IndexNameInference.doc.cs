using FluentAssertions;
using Nest;
using System;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit;
using static Tests.Framework.RoundTripper;
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
		/**
		* ==== Default Index name on Connection Settings
		* A default index name can be specified on `ConnectionSettings` using `.DefaultIndex()`.
		* This is the default index name to use when no other index name can be resolved for a request
		*/
		[U] public void DefaultIndexIsInferred()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("defaultindex");
			var resolver = new IndexNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("defaultindex");
		}

		/**
		* [[index-name-type-mapping]]
		* ==== Mapping an Index name for a .NET type
		* A index name can be mapped for CLR types using `.MapDefaultTypeIndices()` on `ConnectionSettings`.
		*/
		[U] public void ExplicitMappingIsInferredUsingMapDefaultTypeIndices()
		{
			var settings = new ConnectionSettings()
				.DefaultMappingFor<Project>(m => m
					.IndexName("projects")
				);
			var resolver = new IndexNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("projects");
		}

		/**
		 * `.DefaultMappingFor<T>()` can also be used to specify the index name, as well as be used
		 * to specify the type name and POCO property that should be used as the id for the document
		 */
		[U]
		public void ExplicitMappingIsInferredUsingDefaultMappingFor()
		{
			var settings = new ConnectionSettings()
				.DefaultMappingFor<Project>(m => m
					.IndexName("projects")
				);
			var resolver = new IndexNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("projects");
		}

		/** An index name for a POCO provided using `.MapDefaultTypeIndices()` or `.DefaultMappingFor<T>()` **will take precedence** over
		* the default index name set on `ConnectionSettings`. This way, the client can be configured with a default index to use if no
		* index is specified, and a specific index to use for different POCO types.
		*/
		[U] public void ExplicitMappingTakesPrecedence()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("defaultindex")
				.DefaultMappingFor<Project>(m => m
					.IndexName("projects")
				);
			var resolver = new IndexNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("projects");
		}

		/**
		* ==== Explicitly specifying Index name on the request
		* For API calls that expect an index name, the index name can be explicitly provided
		* on the request
		*/
		[U] public void ExplicitIndexOnRequest()
		{
			Uri requestUri = null;
			var client = TestClient.GetInMemoryClient(s=>s.OnRequestCompleted(r => { requestUri = r.Uri; }));

			var response = client.Search<Project>(s => s.Index("some-other-index")); //<1> Provide the index name on the request

			requestUri.Should().NotBeNull();
			requestUri.LocalPath.Should().StartWith("/some-other-index/");
		}

		/** When an index name is provided on a request, it **will take precedence** over the default
		* index name and any index name specified for the POCO type using `.MapDefaultTypeIndices()` or
		* `.DefaultMappingFor<T>()`
		*/
		[U] public void ExplicitIndexOnRequestTakesPrecedence()
		{
			var client = TestClient.GetInMemoryClient(s=>
				new ConnectionSettings()
					.DefaultIndex("defaultindex")
					.DefaultMappingFor<Project>(m => m
						.IndexName("projects")
					)
			);

			var response = client.Search<Project>(s => s.Index("some-other-index")); //<1> Provide the index name on the request

			response.ApiCall.Uri.Should().NotBeNull();
			response.ApiCall.Uri.LocalPath.Should().StartWith("/some-other-index/");
		}

		/** In summary, the order of precedence for determining the index name for a request is
		 *
		 * . Index name specified  on the request
		 * . Index name specified for the generic type parameter in the request using `.MapDefaultTypeIndices()` or `.DefaultMappingFor<T>()`
		 * . Default index name specified on `ConnectionSettings`
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
			var client = TestClient.GetClient(s => new ConnectionSettings());
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
