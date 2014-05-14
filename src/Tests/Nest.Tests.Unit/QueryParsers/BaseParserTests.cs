using System;
using System.IO;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers 
{
	[TestFixture]
	public class BaseParserTests : BaseJsonTests
	{
		public ISearchDescriptor GetSearchDescriptorForQuery(Func<SearchDescriptor<ElasticsearchProject>, SearchDescriptor<ElasticsearchProject>> create)
		{
			var descriptor = create(new SearchDescriptor<ElasticsearchProject>());
			var json = this._client.Serializer.Serialize(descriptor);
			Console.WriteLine(json.Utf8String());
			using (var ms = new MemoryStream(json))
			{
				ISearchDescriptor d = this._client.Serializer.Deserialize<SearchDescriptor<ElasticsearchProject>>(ms);
				d.Should().NotBeNull();
				d.Query.Should().NotBeNull();
				return d;
			}
		}
		public ISearchDescriptor GetSearchDescriptorForFilter(Func<SearchDescriptor<ElasticsearchProject>, SearchDescriptor<ElasticsearchProject>> create)
		{
			var descriptor = create(new SearchDescriptor<ElasticsearchProject>());
			var json = this._client.Serializer.Serialize(descriptor);
			Console.WriteLine(json.Utf8String());
			using (var ms = new MemoryStream(json))
			{
				ISearchDescriptor d = this._client.Serializer.Deserialize<SearchDescriptor<ElasticsearchProject>>(ms);
				d.Should().NotBeNull();
				d.Filter.Should().NotBeNull();
				return d;
			}
		}
		
		public T DeserializeInto<T>(MethodBase method, string fileName = null)
		{
			var json = this.ReadMethodJson(method, fileName);
			using (var stream = new MemoryStream(json.Utf8Bytes()))
			{
				return this._client.Serializer.Deserialize<T>(stream);
			}
		}
		
	}
}
