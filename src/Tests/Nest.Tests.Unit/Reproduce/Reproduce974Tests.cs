using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce974Tests : BaseJsonTests
	{
		[Test]
		public void OrAssignRepeatedlyShouldNotThrowStackOverflow()
		{
			Assert.DoesNotThrow(() =>
			{
				QueryContainer query = null;
				for (int i = 0; i < 10000; i++)
				{
					var q = Query<ElasticsearchProject>.Term(f => f.Id, i);
					query |= q;
				}
			});
		}
	}
}
