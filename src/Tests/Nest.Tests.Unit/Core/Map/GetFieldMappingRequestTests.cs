using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Resolvers;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Map
{
	[TestFixture]
	public class GetFieldMappingRequestTests : BaseJsonTests
	{
		[Test]
		public void Descriptor_InferredIndex_InferredType()
		{
			var result = this._client.GetFieldMapping<ElasticsearchProject>(m=>m.Fields(p=>p.NestedFollowers.First().LastName).ExpandWildcards(ExpandWildcards.Open));
			result.ConnectionStatus.RequestUrl.Should()
				.EndWith("/nest_test_data/_mapping/elasticsearchprojects/field/nestedFollowers.lastName?expand_wildcards=open");
		}

		[Test]
		public void Descriptor_AllIndices_InferredType()
		{
			var result = this._client.GetFieldMapping<ElasticsearchProject>(m=>m
				.AllIndices()
				.Fields(p=>p.NestedFollowers.First().LastName)
			);
			result.ConnectionStatus.RequestUrl.Should().EndWith("/_mapping/elasticsearchprojects/field/nestedFollowers.lastName");
		}

		[Test]
		public void Descriptor_AllIndices_CustomType_MultipleFields()
		{
			var result = this._client.GetFieldMapping<ElasticsearchProject>(m=>m
				.AllIndices()
				.Types("xi", "elasticsearchprojects")
				.Fields("name", Property.Path<ElasticsearchProject>(p=>p.Followers[0].LastName))
			);
			result.ConnectionStatus.RequestUrl.Should().EndWith("/_mapping/xi%2Celasticsearchprojects/field/name%2Cfollowers.lastName");
		}
		
		[Test]
		public void Initializer_Untyped_FieldOnly()
		{
			var result = this._client.GetFieldMapping(new GetFieldMappingRequest("name"));
			result.ConnectionStatus.RequestUrl.Should().EndWith("/_mapping/field/name");
		}

		[Test]
		public void Initializer_Untyped_IndicesAndTypes()
		{
			var result = this._client.GetFieldMapping(new GetFieldMappingRequest("name")
			{
				Indices = new List<IndexNameMarker> { { "index1" }, { "index2"} },
				Types = new List<TypeNameMarker> { { "type1" }, { "type2" }}
			});
			result.ConnectionStatus.RequestUrl.Should().EndWith("/index1%2Cindex2/_mapping/type1%2Ctype2/field/name");
		}

		[Test]
		public void Initializer_Typed_FieldOnly()
		{
			var result = this._client.GetFieldMapping(new GetFieldMappingRequest<ElasticsearchProject>(p=>p.Name));
			result.ConnectionStatus.RequestUrl.Should().EndWith("/nest_test_data/_mapping/elasticsearchprojects/field/name");
		}

		[Test]
		public void Initializer_Typed_IndicesAndTypes()
		{
			var result = this._client.GetFieldMapping(new GetFieldMappingRequest<ElasticsearchProject>(p=>p.Name)
			{
				Indices = new List<IndexNameMarker> { { "index1" }, { "index2"} },
				Types = new List<TypeNameMarker> { { "type1" }, { "type2" }}
			});
			result.ConnectionStatus.RequestUrl.Should().EndWith("/index1%2Cindex2/_mapping/type1%2Ctype2/field/name");
		}
	}
}
