using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Aggregations.Bucket.Children;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations
{
	/**
	*=== Reserved aggregation names
	* NEST uses a heuristics based parser to parse the aggregations back in to typed responses.
	* Because of this, some of the key properties we use to make decisions about parsing are not allowed as key names
	* for aggregations in a request.
	*
	* Elasticsearch 5.x will at some point get a flag that returns the aggregations in a parsable
	* fashion. when this happens, this limitation will be lifted but until that time, avoid the following names for
	* aggregation keys:
	*/
	public class ReservedAggregationNames : DocumentationTestBase
	{
		public string[] Reserved => new []
		{
		    "score",
            "value_as_string",
            "keys",
            "max_score"
		};

        //hide
		private TermsAggregation Terms(string name) => new TermsAggregation(name) {Field = "x"};

		//hide
		[U] public void ReservedKeyWordsThrow()
		{
			foreach (var key in Reserved)
			{
				ThrowsOn(key, this.SearchFluent, nameof(SearchFluent));
				ThrowsOn(key, this.SearchInitializer, nameof(SearchInitializer));
				ThrowsOn(key, this.DictionaryAddInitializer, nameof(DictionaryAddInitializer));
				ThrowsOn(key, this.DictionaryConstructor, nameof(DictionaryConstructor));
				ThrowsOn(key, this.DictionaryImplict, nameof(DictionaryImplict));

				//Container themselves do not throw just their assignment to AggregationDictionary
				DoesNotThrowOn(key, this.ContainerImplicitConvert, nameof(ContainerImplicitConvert));
			}
		}

		//hide
		[U] public void NonReservedKeywordsDoNotThrow()
		{
			foreach (var key in Reserved.Select(r => r + "1"))
			{
				DoesNotThrowOn(key, this.SearchFluent, nameof(SearchFluent));
				DoesNotThrowOn(key, this.SearchInitializer, nameof(SearchInitializer));
				DoesNotThrowOn(key, this.DictionaryAddInitializer, nameof(DictionaryAddInitializer));
				DoesNotThrowOn(key, this.DictionaryConstructor, nameof(DictionaryConstructor));
				DoesNotThrowOn(key, this.DictionaryImplict, nameof(DictionaryImplict));

				//Container themselves do not throw just their assignment to AggregationDictionary
				DoesNotThrowOn(key, this.ContainerImplicitConvert, nameof(ContainerImplicitConvert));
			}
		}

		//hide
		private void SearchFluent(string name) =>
			this.Client.Search<Project>(s => s.Aggregations(aggs => aggs.Terms(name, t => t.Field("x"))));

		//hide
		private void SearchInitializer(string name) =>
			this.Client.Search<Project>(new SearchRequest<Project>
			{
				Aggregations = Terms(name)
			});

		//hide
		private void DictionaryAddInitializer(string name) => new AggregationDictionary()
		{
			{name, Terms(name)}
		};

		//hide
		private void DictionaryConstructor(string name)
		{
			var vanilla = new Dictionary<string, AggregationContainer>()
			{
				{name, Terms(name)}
			};
			var dictionary = new AggregationDictionary(vanilla);
		}

		//hide
		private void DictionaryImplict(string name)
		{
			AggregationDictionary vanilla = new Dictionary<string, AggregationContainer>()
			{
				{name, Terms(name)}
			};
		}

		//hide
		private void ContainerImplicitConvert(string name)
		{
			AggregationContainer x = Terms(name);
		}

		//hide
		private void DoesNotThrowOn(string name, Action<string> act, string origin) =>
			act.Invoking(s => s(name)).ShouldNotThrow<ArgumentException>(origin);

		//hide
		private void ThrowsOn(string name, Action<string> act, string origin)
		{
			var e = act.Invoking(s => s(name)).ShouldThrow<ArgumentException>(origin).Subject.First();
			AssertArgumentException(name, e);
		}

		//hide
		private void AssertArgumentException(string name, ArgumentException e)
		{
			e.Should().NotBeNull();
			e.Message.Should().StartWith($"'{name}' is one of the reserved");
		}
	}
}
