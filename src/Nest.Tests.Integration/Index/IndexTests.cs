using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Index
{
	[TestFixture]
	public class IndexTests : BaseElasticSearchTests
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
      var response = this.ConnectedClient.Index(newProject);

      StringAssert.Contains(@"""id"": 2000", response.Request);
      StringAssert.Contains(@"""loc"": 0", response.Request);
      StringAssert.Contains(@"""longValue"": 0", response.Request);
      StringAssert.Contains(@"""floatValue"": 0.0", response.Request);
      StringAssert.Contains(@"""doubleValue"": 0.0", response.Request);
      StringAssert.Contains(@"""boolValue"": false", response.Request);
      
		}
		
	}
}
