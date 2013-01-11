using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Tests.MockData;
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
      var newProject = new ElasticSearchProject
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
      var response = this._client.Index(newProject);
      var connectionStatus = response.ConnectionStatus;
      StringAssert.Contains(@"""id"": 2000", connectionStatus.Request);
      StringAssert.Contains(@"""loc"": 0", connectionStatus.Request);
      StringAssert.Contains(@"""longValue"": 0", connectionStatus.Request);
      StringAssert.Contains(@"""floatValue"": 0.0", connectionStatus.Request);
      StringAssert.Contains(@"""doubleValue"": 0.0", connectionStatus.Request);
      StringAssert.Contains(@"""boolValue"": false", connectionStatus.Request);
		}
		
	}
}
