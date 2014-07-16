using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Index
{
	[TestFixture]
	public class IndexDefaultValueTests : IntegrationTests
	{
		[Test]
		public void IndexDefaultValue()
		{
			var newProject = new ElasticsearchProject
			{
				Id = 2000,
				Name = "TempProject",
				LOC = 0,
				LongValue = 0,
				DoubleValue = 0,
				FloatValue = 0,
				FloatValues = new[] { 0f },
				BoolValue = false
			};
			var response = this.Client.Index(newProject);
			var connectionStatus = response.ConnectionStatus;
			var request = connectionStatus.Request.Utf8String();
			StringAssert.Contains(@"""id"": 2000", request);
			StringAssert.Contains(@"""loc"": 0", request);
			StringAssert.Contains(@"""longValue"": 0", request);
			StringAssert.Contains(@"""floatValue"": 0.0", request);
			StringAssert.Contains(@"""doubleValue"": 0.0", request);
			StringAssert.Contains(@"""boolValue"": false", request);
		}

	}
}
