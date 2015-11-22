using System;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit.Sdk;
using static Tests.Framework.RoundTripper;
using static Nest.Static;
using Elasticsearch.Net.Connection;
using System.IO;
using System.Text;

namespace Tests.ClientConcepts.HighLevel.CovariantHits
{
	/** # Covariant Search Results
	 *
	 * NEST directly supports returning covariant result sets.
	 * Meaning a result can be typed to an interface or baseclass
	 * but the actual instance type of the result can be that of the subclass directly
	 *
	 * Let look at an example, imagine we want to search over multiple types that all implement
	 * `ISearchResult`
	 *
	 */
	public interface ISearchResult
	{
		string Name { get; set; }
	} 
	/**
	* We have three implementations of `ISearchResult` namely `A`, `B` and `C`
	*/

	public class A : ISearchResult
	{
		public string Name { get; set; }
		public int PropertyOnA { get; set; }
	} 

	public class B : ISearchResult
	{
		public string Name { get; set; }
		public int PropertyOnB { get; set; }
	} 

	public class C : ISearchResult
	{
		public string Name { get; set; }
		public int PropertyOnC { get; set; }
	} 

	public class CovariantSearchResults
	{
		private IElasticClient _client = TestClient.GetFixedReturnClient(CovariantSearchResultMock.Json);
		[U] public void UsingTypes()
		{
			/**
			* The most straightforward way to search over multiple types is to
			* type the response to the parent interface or base class
			* and pass the actual types we want to search over using `.Types()`
			*/
			var result = this._client.Search<ISearchResult>(s => s
				.Type(Types.Type(typeof(A), typeof(B), typeof(C)))
				.Size(100)
			);
			/**
			* Nest will translate this to a search over /index/a,b,c/_search. 
			* hits that have `"_type" : "a"` will be serialized to `A` and so forth
			*/
			
			/**
			* Here we assume our response is valid and that we received the 100 documents
			* we are expecting. Remember `result.Documents` is an `IEnumerable&lt;ISearchResult&gt;`
			*/
			result.IsValid.Should().BeTrue();
			result.Documents.Count().Should().Be(100);
			
			/**
			* To prove the returned result set is covariant we filter the documents based on their 
			* actual type and assert the returned subsets are the expected sizes
			*/
			var aDocuments = result.Documents.OfType<A>();
			var bDocuments = result.Documents.OfType<B>();
			var cDocuments = result.Documents.OfType<C>();

			aDocuments.Count().Should().Be(25);
			bDocuments.Count().Should().Be(25);
			cDocuments.Count().Should().Be(50);

			/**
			* and assume that properties that only exist on the subclass itself are properly filled
			*/
			aDocuments.Should().OnlyContain(a => a.PropertyOnA > 0);
			bDocuments.Should().OnlyContain(a => a.PropertyOnB > 0);
			cDocuments.Should().OnlyContain(a => a.PropertyOnC > 0);
		}


		[U] public void UsingConcreteTypeSelector()
		{
			/**
			* A more low level approach is to inspect the hit yourself and determine the CLR type to deserialize to
			*/
			var result = this._client.Search<ISearchResult>(s => s
				.ConcreteTypeSelector((d, h) => h.Type == "a" ? typeof(A) : h.Type == "b" ? typeof(B) : typeof(C))
				.Size(100)
			);

			/**
			* here for each hit we'll call the delegate with `d` which a dynamic representation of the `_source`
			* and a typed `h` which represents the encapsulating hit.
			*/
			
			/**
			* Here we assume our response is valid and that we received the 100 documents
			* we are expecting. Remember `result.Documents` is an `IEnumerable&lt;ISearchResult&gt;`
			*/
			result.IsValid.Should().BeTrue();
			result.Documents.Count().Should().Be(100);
			
			/**
			* To prove the returned result set is covariant we filter the documents based on their 
			* actual type and assert the returned subsets are the expected sizes
			*/
			var aDocuments = result.Documents.OfType<A>();
			var bDocuments = result.Documents.OfType<B>();
			var cDocuments = result.Documents.OfType<C>();

			aDocuments.Count().Should().Be(25);
			bDocuments.Count().Should().Be(25);
			cDocuments.Count().Should().Be(50);

			/**
			* and assume that properties that only exist on the subclass itself are properly filled
			*/
			aDocuments.Should().OnlyContain(a => a.PropertyOnA > 0);
			bDocuments.Should().OnlyContain(a => a.PropertyOnB > 0);
			cDocuments.Should().OnlyContain(a => a.PropertyOnC > 0);
		}
	}

	internal static class CovariantSearchResultMock
	{
		public static object Json = new
		{
			took = 1,
			timed_out = false,
			_shards = new {
				total = 2,
				successful = 2,
				failed = 0
			},
			hits = new {
				total = 100,
				max_score = 1.0,
				hits = Enumerable.Range(1, 25).Select(i => (object)new 
				{
					_index = "project",
					_type = "a",
					_id = i,
					_score = 1.0,
					_source= new { name= "A object", propertyOnA = i }
				}).Concat(Enumerable.Range(26, 25).Select(i => (object)new 
				{
					_index = "project",
					_type = "b",
					_id = i,
					_score = 1.0,
					_source= new { name= "B object", propertyOnB = i }
				})).Concat(Enumerable.Range(51, 50).Select(i => new 
				{
					_index = "project",
					_type = "c",
					_id = i,
					_score = 1.0,
					_source= new { name= "C object", propertyOnC = i }
				})).ToArray()
			}
		};
	}
}
