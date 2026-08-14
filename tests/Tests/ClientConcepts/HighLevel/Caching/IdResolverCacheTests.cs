// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Reflection;

namespace Tests.ClientConcepts.HighLevel.Caching
{
	public class IdResolverCacheTests
	{
		private class MyDocument
		{
			public string Id { get; set; }
		}

		private class MyKeyedDocument
		{
			public string Key { get; set; }
		}

		private static IDictionary GlobalCache =>
			(IDictionary)typeof(IdResolver)
				.GetField("GlobalDelegateCache", BindingFlags.NonPublic | BindingFlags.Static)
				.GetValue(null);

		private static IDictionary LocalCache(IdResolver resolver) =>
			(IDictionary)typeof(IdResolver)
				.GetField("_localDelegateCache", BindingFlags.NonPublic | BindingFlags.Instance)
				.GetValue(resolver);

		[U] public void CachesGetterDelegate()
		{
			var resolver = new IdResolver(new ElasticsearchClientSettings());

			resolver.Resolve(new MyDocument { Id = "1" }).Should().Be("1");
			GlobalCache.Contains(typeof(MyDocument)).Should().BeTrue();
			resolver.Resolve(new MyDocument { Id = "2" }).Should().Be("2");
		}

		[U] public void CachesGetterDelegateForCustomIdProperty()
		{
			var settings = new ElasticsearchClientSettings()
				.DefaultMappingFor<MyKeyedDocument>(m => m.IdProperty(p => p.Key));
			var resolver = new IdResolver(settings);

			resolver.Resolve(new MyKeyedDocument { Key = "1" }).Should().Be("1");
			LocalCache(resolver).Contains(typeof(MyKeyedDocument)).Should().BeTrue();
			GlobalCache.Contains(typeof(MyKeyedDocument)).Should().BeFalse();
		}
	}
}
