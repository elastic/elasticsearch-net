using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	/**[[index-name-inference]]
	*== Index Name Inference
	*
	* Many endpoints within the Elasticsearch API expect to receive one or more index names
	* as part of the request in order to know what index/indices a request should operate on.
	*
	* NEST has a number of ways in which an index name can be specified
	*/
	public class IndexNameInference
	{
		/**=== Default Index name on ConnectionSettings
		* A default index name can be specified on `ConnectionSettings` using `.DefaultIndex()`.
		* This is the default index name to use when no other index name can be resolved for a request
		*/
		[U]
		public void DefaultIndexIsInferred()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("defaultindex");
			var resolver = new IndexNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("defaultindex");
		}

		/**=== Mapping an Index name for POCOs
		* A index name can be mapped for CLR types using `.MapDefaultTypeIndices()` on `ConnectionSettings`.
		*/
		[U]
		public void ExplicitMappingIsInferred()
		{
			var settings = new ConnectionSettings()
				.MapDefaultTypeIndices(m => m
					.Add(typeof(Project), "projects")
				);
			var resolver = new IndexNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("projects");
		}

		/**=== Mapping an Index name for POCOs
		* An index name for a POCO provided using `.MapDefaultTypeIndices()` **will take precedence** over
		* the default index name
		*/
		[U]
		public void ExplicitMappingTakesPrecedence()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("defaultindex")
				.MapDefaultTypeIndices(m => m
					.Add(typeof(Project), "projects")
				);
			var resolver = new IndexNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("projects");
		}

		/**=== Explicitly specifying Index name on the request
		* For API calls that expect an index name, the index name can be explicitly provided
		* on the request
		*/
		[U]
		public void ExplicitIndexOnRequest()
		{
			Uri requestUri = null;
			var client = TestClient.GetInMemoryClient(s => s
				.OnRequestCompleted(r => { requestUri = r.Uri; }));

			var response = client.Search<Project>(s => s.Index("some-other-index")); //<1> Provide the index name on the request

			requestUri.Should().NotBeNull();
			requestUri.LocalPath.Should().StartWith("/some-other-index/");
		}

		/** When an index name is provided on a request, it **will take precedence** over the default
		* index name and any index name specified for the POCO type using `.MapDefaultTypeIndices()`
		*/
		[U]
		public void ExplicitIndexOnRequestTakesPrecedence()
		{
			var client = TestClient.GetInMemoryClient(s =>
				new ConnectionSettings()
					.DefaultIndex("defaultindex")
					.MapDefaultTypeIndices(m => m
						.Add(typeof(Project), "projects")
					)
			);

			var response = client.Search<Project>(s => s.Index("some-other-index")); //<1> Provide the index name on the request

			response.ApiCall.Uri.Should().NotBeNull();
			response.ApiCall.Uri.LocalPath.Should().StartWith("/some-other-index/");
		}
	}
}
