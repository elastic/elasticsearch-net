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
	public abstract class BaseParserTests : BaseJsonTests
	{
		public ISearchRequest GetSearchDescriptorForQuery(Func<SearchDescriptor<ElasticsearchProject>, SearchDescriptor<ElasticsearchProject>> create)
		{
			return GetSearchDescriptor(create, request => request.Query.Should().NotBeNull());
		}

		public ISearchRequest GetSearchDescriptorForFilter(Func<SearchDescriptor<ElasticsearchProject>, SearchDescriptor<ElasticsearchProject>> create)
		{
			return GetSearchDescriptor(create, request => request.PostFilter.Should().NotBeNull());
		}

		private ISearchRequest GetSearchDescriptor(Func<SearchDescriptor<ElasticsearchProject>, SearchDescriptor<ElasticsearchProject>> create, Action<ISearchRequest> assertFunc )
		{
			var descriptor = create(new SearchDescriptor<ElasticsearchProject>());
			var json = this._client.Serializer.Serialize(descriptor);
			Console.WriteLine(json.Utf8String());
			using (var ms = new MemoryStream(json))
			{
				ISearchRequest d = this._client.Serializer.Deserialize<SearchDescriptor<ElasticsearchProject>>(ms);
				d.Should().NotBeNull();
				assertFunc(d);
				return d;
			}
		}
		
		protected static void AssertIsTermFilter(IQueryContainer f1, IQueryContainer f2)
		{
			f1.Should().NotBeNull();
			f2.Should().NotBeNull();
			f1.Term.Should().NotBeNull();
			f2.Term.Should().NotBeNull();

			f1.Term.Field.Should().Be(f2.Term.Field);
			f1.Term.Value.Should().Be(f2.Term.Value);
		}
		
		protected static void AssertIsTermFilter(QueryContainer filter1, QueryContainer filter2)
		{
			filter1.Should().NotBeNull();
			filter2.Should().NotBeNull();

			IQueryContainer f1 = filter1;
			IQueryContainer f2 = filter2;

			f1.Should().NotBeNull();
			f2.Should().NotBeNull();
			f1.Term.Should().NotBeNull();
			f2.Term.Should().NotBeNull();

			f1.Term.Field.Should().Be(f2.Term.Field);
			f1.Term.Value.Should().Be(f2.Term.Value);
		}
		protected static void AssertIsTermFilter(QueryContainer compareTo, ITermQuery firstTermFilter)
		{
			var c = (IQueryContainer)compareTo;
			firstTermFilter.Should().NotBeNull();
			firstTermFilter.Field.Should().Be(c.Term.Field);
			firstTermFilter.Value.Should().Be(c.Term.Value);
		}
		protected static void AssertIsTermQuery(IQueryContainer query1, IQueryContainer query2)
		{
			query1.Term.Should().NotBeNull();
			query2.Term.Should().NotBeNull();

			query2.Should().NotBeNull();
			query2.Term.Field.Should().Be(query1.Term.Field);
			query2.Term.Value.Should().Be(query1.Term.Value);
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
