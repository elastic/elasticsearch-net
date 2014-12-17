using FluentAssertions;
using Elasticsearch.Net.Connection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Nest.Tests.Unit.Search
{
	[TestFixture]
	public class SearchUrlTests
	{
		private void TestUrl(string expected, SearchDescriptor<ElasticsearchProject> descriptor, ConnectionSettings settings = null)
		{
			var client = new ElasticClient(settings, new InMemoryConnection());
			var response = client.Search<ElasticsearchProject>(descriptor);
			var uri = new Uri(response.ConnectionStatus.RequestUrl);
			var actual = WebUtility.UrlDecode(uri.AbsolutePath).Replace(" ", "");
			actual.Should().Be(expected);
		}

		[Test]
		public void AllIndices_AllTypes_Test()
		{
			TestUrl(
				expected: "/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.AllIndices()
					.AllTypes()
			);
		}

		[Test]
		public void AllIndices_ExplicitType_Test()
		{
			TestUrl(
				expected: "/_all/type1/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.AllIndices()
					.Type("type1")
			);
		}

		[Test]
		public void AllIndices_MultipleExplicitTypes_Test()
		{
			TestUrl(
				expected: "/_all/type1,type2/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.AllIndices()
					.Types("type1", "type2")
			);
		}

		[Test]
		public void AllIndices_InferredType_Test()
		{
			TestUrl(
				expected: "/_all/elasticsearchprojects/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.AllIndices()
			);
		}

		[Test]
		public void AllIndices_MappedType_Test()
		{
			TestUrl(
				expected: "/_all/type1/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.AllIndices(),
				settings: new ConnectionSettings()
					.MapDefaultTypeNames(m => m
						.Add(typeof(ElasticsearchProject), "type1")
					)
			);
		}

		[Test]
		public void DefaultIndex_AllIndices_AllTypes_Test()
		{
			TestUrl(
				expected: "/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.AllIndices()
					.AllTypes(),
				settings: new ConnectionSettings()
					.SetDefaultIndex("defaultindex")
			);
		}

		[Test]
		public void DefaultIndex_AllTypes_Test()
		{
			TestUrl(
				expected: "/defaultindex/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.AllTypes(),
				settings: new ConnectionSettings()
					.SetDefaultIndex("defaultindex")
			);
		}

		[Test]
		public void DefaultIndex_ExplicitType_Test()
		{
			TestUrl(
				expected: "/defaultindex/type1/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.Type("type1"),
				settings: new ConnectionSettings()
					.SetDefaultIndex("defaultindex")
			);
		}

		[Test]
		public void DefaultIndex_MultipleExplicitTypes_Test()
		{
			TestUrl(
				expected: "/defaultindex/type1,type2/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.Types("type1", "type2"),
				settings: new ConnectionSettings()
					.SetDefaultIndex("defaultindex")
			);
		}

		[Test]
		public void DefaultIndex_InferredType_Test()
		{
			TestUrl(
				expected: "/defaultindex/elasticsearchprojects/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>(),
				settings: new ConnectionSettings()
					.SetDefaultIndex("defaultindex")
			);
		}

		[Test]
		public void DefaultIndex_MappedType_Test()
		{
			TestUrl(
				expected: "/defaultindex/type1/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>(),
				settings: new ConnectionSettings()
					.SetDefaultIndex("defaultindex")
					.MapDefaultTypeNames(m => m
						.Add(typeof(ElasticsearchProject), "type1")
					)
			);
		}

		[Test]
		public void No_DefaultIndex_AllIndices_AllTypes_Test()
		{
			TestUrl(
				expected: "/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.AllIndices()
					.AllTypes()
			);
		}

		[Test]
		public void No_DefaultIndex_AllTypes_Test()
		{
			TestUrl(
				expected: "/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.AllTypes()
			);
		}

		[Test]
		public void No_DefaultIndex_ExplicitType_Test()
		{
			TestUrl(
				expected: "/_all/type1/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.Type("type1")
			);
		}

		[Test]
		public void No_DefaultIndex_MultipleExplicitTypes_Test()
		{
			TestUrl(
				expected: "/_all/type1,type2/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.Types("type1", "type2")
			);
		}

		[Test]
		public void No_DefaultIndex_InferredType_Test()
		{
			TestUrl(
				expected: "/_all/elasticsearchprojects/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
			);
		}

		[Test]
		public void No_DefaultIndex_MappedType_Test()
		{
			TestUrl(
				expected: "/_all/type1/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>(),
				settings: new ConnectionSettings()
					.MapDefaultTypeNames(m => m
						.Add(typeof(ElasticsearchProject), "type1")
					)
			);
		}

		[Test]
		public void MappedIndex_AllTypes_Test()
		{
			TestUrl(
				expected: "/index1/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.AllTypes(),
				settings: new ConnectionSettings()
					.MapDefaultTypeIndices(m => m
						.Add(typeof(ElasticsearchProject), "index1")
					)
			);
		}

		[Test]
		public void MappedIndex_MappedType_Test()
		{
			TestUrl(
				expected: "/index1/type1/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>(),
				settings: new ConnectionSettings()
					.MapDefaultTypeIndices(m => m
						.Add(typeof(ElasticsearchProject), "index1")
					)
					.MapDefaultTypeNames(m => m
						.Add(typeof(ElasticsearchProject), "type1")
					)
			);
		}

		[Test]
		public void MappedIndex_InferredType_Test()
		{
			TestUrl(
				expected: "/index1/elasticsearchprojects/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>(),
				settings: new ConnectionSettings()
					.MapDefaultTypeIndices(m => m
						.Add(typeof(ElasticsearchProject), "index1")
					)
			);
		}

		[Test]
		public void MappedIndex_MultipleExplicitTypes_Test()
		{
			TestUrl(
				expected: "/index1/type1,type2/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.Types("type1", "type2"),
				settings: new ConnectionSettings()
					.MapDefaultTypeIndices(m => m
						.Add(typeof(ElasticsearchProject), "index1")
					)
			);
		}

		[Test]
		public void ExplicitIndex_AllTypes_Test()
		{
			TestUrl(
				expected: "/index1/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.Index("index1")
					.AllTypes()
			);
		}

		[Test]
		public void ExplicitIndex_InferredType_Test()
		{
			TestUrl(
				expected: "/index1/elasticsearchprojects/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.Index("index1")
			);
		}

		[Test]
		public void ExplicitIndex_ExplicitType_Test()
		{
			TestUrl(
				expected: "/index1/type1/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.Index("index1")
					.Type("type1")
			);
		}

		[Test]
		public void ExplicitIndex_MultipleExplicitTypes_Test()
		{
			TestUrl(
				expected: "/index1/type1,type2/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.Index("index1")
					.Types("type1", "type2")
			);
		}

		[Test]
		public void MultipleExplicitIndices_MultipleExplicitTypes_Test()
		{
			TestUrl(
				expected: "/index1,index2/type1,type2/_search",
				descriptor: new SearchDescriptor<ElasticsearchProject>()
					.Indices("index1", "index2")
					.Types("type1", "type2")
			);
		}
	}
}
