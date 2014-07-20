using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class ParseFilterTestsBase : BaseParserTests
	{
		protected FilterContainer Filter1 = Filter<object>.Term("w", "x");
		protected FilterContainer Filter2 = Filter<object>.Term("y", "z");
		protected FilterContainer Filter3 = Filter<object>.Term("a", "b");
		
		protected T ParseSearchDescriptorFromFile<T>(Func<IFilterContainer, T> filterBaseSelector, MethodBase method, string fileName = null)
			where T : IFilter
		{
			var descriptor = this.DeserializeInto<SearchDescriptor<ElasticsearchProject>>(method, fileName);
			var filter = filterBaseSelector(((ISearchRequest)descriptor).Filter);
			filter.Should().NotBeNull();
			return filter;
		}
		
		protected T SerializeThenDeserialize<T>(string cacheName, string cacheKey, bool cache, 
			Func<IFilterContainer, T> filterBaseSelector,
			Func<FilterDescriptor<ElasticsearchProject>, FilterContainer> create
			)
			where T : IFilter
		{
			var descriptor = this.GetSearchDescriptorForFilter(s=>s
				.Filter(f=>create(f
					.Name(cacheName)
					.CacheKey(cacheKey)
					.Cache(cache)
				))
			);
			var filter = filterBaseSelector(descriptor.Filter);
			filter.Should().NotBeNull();
			filter.FilterName.Should().Be(cacheName);
			filter.Cache.Should().Be(cache);
			filter.CacheKey.Should().Be(cacheKey);
			return filter;
		}
	}
}
