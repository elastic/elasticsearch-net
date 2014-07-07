using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Map
{
	[TestFixture]
	public class MappingBeghaviourTests : BaseJsonTests
	{
		[ElasticType(Name="some-weird-name-that-i-want")]
		public class TestMappingObject
		{
			public string Id { get; set; }
			[ElasticProperty(Name="namez", Index = FieldIndexOption.Analyzed)]
			public string Name { get; set; }
		}


		[Test]
		public void MapFromPropertiesCanBeOverwritten()
		{
			var result = this._client.Map<TestMappingObject>(m => m
				.MapFromAttributes()
				.Properties(pp => pp
					.String(s => s.Name(p => p.Name).Index(FieldIndexOption.NotAnalyzed))
				)
			);
			var request = result.ConnectionStatus.Request.Utf8String();
			request.Should().Contain(@"""index"": ""not_analyzed""");
			request.Should().Contain(@"""namez"": {");
			request.Should().NotContain(@"""name"": {");
			
		}
	}
}