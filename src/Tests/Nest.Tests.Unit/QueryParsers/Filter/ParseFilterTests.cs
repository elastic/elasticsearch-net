using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class ParseFilterTestsBase : BaseParserTests
	{
		protected BaseFilterDescriptor Filter1 = Filter<object>.Term("w", "x");
		protected BaseFilterDescriptor Filter2 = Filter<object>.Term("y", "z");
		protected BaseFilterDescriptor Filter3 = Filter<object>.Term("a", "b");
		
		protected T ParseSearchDescriptorFromFile<T>(Func<IFilterDescriptor, T> filterBaseSelector, MethodBase method, string fileName = null)
			where T : IFilterBase
		{
			var descriptor = this.DeserializeInto<SearchDescriptor<ElasticsearchProject>>(method, fileName);
			var filter = filterBaseSelector(((ISearchDescriptor)descriptor).Filter);
			filter.Should().NotBeNull();
			return filter;
		}
		
		protected T SerializeThenDeserialize<T>(string cacheName, string cacheKey, bool cache, 
			Func<IFilterDescriptor, T> filterBaseSelector,
			Func<FilterDescriptorDescriptor<ElasticsearchProject>, BaseFilterDescriptor> create
			)
			where T : IFilterBase
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
			filter.CacheName.Should().Be(cacheName);
			filter.Cache.Should().Be(cache);
			filter.CacheKey.Should().Be(cacheKey);
			return filter;
		}
	}
}
