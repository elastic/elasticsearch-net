using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Nest;

namespace Nest.Tests.Unit.Core.Map
{
	[TestFixture]
	public class MappingBeghaviourTests : BaseJsonTests
	{
		[ElasticType(Name="some-weird-name-that-i-want")]
		public class TestMappingObject
		{
			public string Id { get; set; }
			[ElasticProperty(Name="namez", Index = FieldIndexOption.analyzed)]
			public string Name { get; set; }
		}


		[Test]
		public void MapFromPropertiesCanBeOverwritten()
		{
			var result = this._client.Map<TestMappingObject>(m => m
				.MapFromAttributes()
				.Properties(pp => pp
					.String(s => s.Name(p => p.Name).Index(FieldIndexOption.not_analyzed))
				)
			);
			result.ConnectionStatus.Request.Should().Contain(@"""index"": ""not_analyzed""");
			result.ConnectionStatus.Request.Should().Contain(@"""namez"": {");
			result.ConnectionStatus.Request.Should().NotContain(@"""name"": {");
		}
	}
}