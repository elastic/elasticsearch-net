using System;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.CovariantHits
{
	public class CovariantSearchResults
	{
		/**=== Covariant search results
		 *
		 * NEST directly supports returning covariant result sets.
		 * Meaning a result can be typed to an interface or base class
		 * but the actual instance type of the result can be that of the subclass directly
		 *
		 * Let's look at an example; Imagine we want to search over multiple types that all implement
		 * `ISearchResult`
		 */
		public interface ISearchResult
		{
			string Name { get; set; }
		}

		public abstract class BaseX : ISearchResult
		{
			public string Name { get; set; }
		}

		/**
		* We have three implementations of `ISearchResult` namely `A`, `B` and `C`
		*/
		public class A : BaseX
		{
			public int PropertyOnA { get; set; }
		}

		public class B : BaseX
		{
			public int PropertyOnB { get; set; }
		}

		public class C : BaseX
		{
			public int PropertyOnC { get; set; }
		}


		private readonly IElasticClient _client = TestClient.GetFixedReturnClient(CovariantSearchResultMock.Json);

		[U] public void UsingTypes()
		{
			/**
			* ==== Using types
			* The most straightforward way to search over multiple types is to
			* type the response to the parent interface or base class
			* and pass the actual types we want to search over using `.Type()`
			*/
			var result = this._client.Search<ISearchResult>(s => s
				.Type(Types.Type(typeof(A), typeof(B), typeof(C)))
				.Size(100)
			);
			/**
			* NEST will translate this to a search over `/index/a,b,c/_search`;
			* hits that have `"_type" : "a"` will be serialized to `A` and so forth
			*/

			/**
			* Here we assume our response is valid and that we received the 100 documents
			* we are expecting. Remember `result.Documents` is an `IReadOnlyCollection<ISearchResult>`
			*/
			result.ShouldBeValid();
			result.Documents.Count.Should().Be(100);

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
			* ==== Using ConcreteTypeSelector
			* A more low level approach is to inspect the hit yourself and determine the CLR type to deserialize to
			*/
			var result = this._client.Search<ISearchResult>(s => s
				.ConcreteTypeSelector((d, h) => h.Type == "a" ? typeof(A) : h.Type == "b" ? typeof(B) : typeof(C))
				.Size(100)
			);

            /**
			* here for each hit we'll call the delegate passed to `ConcreteTypeSelector` where
			* - `d` is a representation of the `_source` exposed as a `dynamic` type
			* - a typed `h` which represents the encapsulating hit of the source i.e. `Hit<dynamic>`
			*/

            /**
			* Here we assume our response is valid and that we received the 100 documents
			* we are expecting. Remember `result.Documents` is an `IReadOnlyCollection<ISearchResult>`
			*/
            result.ShouldBeValid();
			result.Documents.Count.Should().Be(100);

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

		/**
		* ==== Using CovariantTypes
		*/
		[U] public void UsingCovariantTypesOnScroll()
		{
			/**
			* The Scroll API is a continuation of the previous Search example so Types() are lost.
			* You can hint at the types using `.CovariantTypes()`
			*/
			var result = this._client.Scroll<ISearchResult>(TimeSpan.FromMinutes(60), "scrollId", s => s
				.CovariantTypes(Types.Type(typeof(A), typeof(B), typeof(C)))
			);
            /**
			* NEST will translate this to a search over `/index/a,b,c/_search`;
			* hits that have `"_type" : "a"` will be serialized to `A` and so forth
			*/

            /**
			* Here we assume our response is valid and that we received the 100 documents
			* we are expecting. Remember `result.Documents` is an `IReadOnlyCollection<ISearchResult>`
			*/
            result.ShouldBeValid();
			result.Documents.Count.Should().Be(100);

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

		[U] public void UsingConcreteTypeSelectorOnScroll()
		{
			/**
			* The more low level concrete type selector can also be specified on scroll
			*/
			var result = this._client.Scroll<ISearchResult>(TimeSpan.FromMinutes(1), "scrollid", s => s
				.ConcreteTypeSelector((d, h) => h.Type == "a" ? typeof(A) : h.Type == "b" ? typeof(B) : typeof(C))
			);

			/**
			* As before, within the delegate passed to `.ConcreteTypeSelector`
			* - `d` is the `_source` typed as `dynamic`
			* - `h` is the encapsulating typed hit
			*/

			/**
			* Here we assume our response is valid and that we received the 100 documents
			* we are expecting. Remember `result.Documents` is an `IReadOnlyCollection<ISearchResult>`
			*/
			result.ShouldBeValid();
			result.Documents.Count.Should().Be(100);

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

		[U] public void UsingSubClasses()
		{
			/**
			* ==== Using types
			* The most straightforward way to search over multiple types is to
			* type the response to the parent interface or base class
			* and pass the actual types we want to search over using `.Type()`
			*/
			var result = this._client.Search<BaseX>(s => s
				.Type(Types.Type(typeof(A), typeof(B), typeof(C)))
				.Size(100)
			);
			/**
			* NEST will translate this to a search over `/index/a,b,c/_search`;
			* hits that have `"_type" : "a"` will be serialized to `A` and so forth
			*/

			/**
			* Here we assume our response is valid and that we received the 100 documents
			* we are expecting. Remember `result.Documents` is an `IReadOnlyCollection<ISearchResult>`
			*/
			result.ShouldBeValid();
			result.Documents.Count.Should().Be(100);

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
