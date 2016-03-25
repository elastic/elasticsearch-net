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
		* A default index name can be specified on `ConnectionSettings` usinf `.DefaultIndex()`.
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

		/**=== Naming Conventions
		* Index names within Elasticsearch cannot contain upper case letters.
		* NEST will check the index name at the point at which the index
		* name needs to be resolved to make a request; if the index name contains
		* upper case letters, a `ResolveException` will be thrown indicating
		* the problem and the index name that caused the problem.
		*/
		[U]
		public void UppercaseCharacterThrowsResolveException()
		{
			/**
			* In the following example, we create a connection settings withboth a default index
			* name and an index name to use for the `Project` type.
			*/
			var settings = new ConnectionSettings()
				.DefaultIndex("Default")
				.MapDefaultTypeIndices(m => m
					.Add(typeof(Project), "myProjects")
				);

			var resolver = new IndexNameResolver(settings);

			/** When resolving the index name for the `Project` type, a `ResolveException`
			* is thrown, indicating that the index name "__myProjects__" contains upper case letters
			*/
			var e = Assert.Throws<ResolveException>(() => resolver.Resolve<Project>());
			e.Message.Should().Be($"Index names cannot contain uppercase characters: myProjects.");

			/**
			* Similarly, when resolving the index name for the `Tag` type, which will use the default index
			* name, a `ResolveException` is thrown indicating that the default index name contains upper case
			* letters
			*/
			e = Assert.Throws<ResolveException>(() => resolver.Resolve<Tag>());
			e.Message.Should().Be($"Index names cannot contain uppercase characters: Default.");

			/**
			* Finally, when resolving an index name from a string, a `ResolveException` will be thrown
			* if the string contains upper case letters
			*/
			e = Assert.Throws<ResolveException>(() => resolver.Resolve("Foo"));
			e.Message.Should().Be($"Index names cannot contain uppercase characters: Foo.");
		}

		/** If no index name can be resolved for a request i.e. if
		*
		* - no default index name is set on connection settings
		* - no index name is mapped for a POCO
		* - no index name is explicitly specified on the request
		*
		* then a `ResolveException` will be thrown to indicate that the index name is `null`
		*/
		[U]
		public void NoIndexThrowsResolveException()
		{
			var settings = new ConnectionSettings();
			var resolver = new IndexNameResolver(settings);
			var e = Assert.Throws<ResolveException>(() => resolver.Resolve<Project>());
			e.Message.Should().Contain("Index name is null");
		}

		/**
		* ``ResolveException``s bubble out of the client and should be dealt with as <<thrown-exceptions, development time exceptions>>
		* similar to `ArgumentException`, `ArgumentOutOfRangeException` and other exceptions that _usually_ indicate
		* misuse of the client API
		*/
		[U]
		public void ResolveExceptionBubblesOut()
		{
			var client = TestClient.GetInMemoryClient(s => new ConnectionSettings());
			var e = Assert.Throws<ResolveException>(() => client.Search<Project>());
		}
	}
}
