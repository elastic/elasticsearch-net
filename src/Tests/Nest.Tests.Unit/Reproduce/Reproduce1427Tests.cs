using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce1427Tests : BaseJsonTests
	{
		[Test]
		public void RawSurvives()
		{
			var registerPercolator = new RegisterPercolatorDescriptor<ElasticsearchProject>()
				.Name("name")
				.Index("index")
				.Query(q => q.Raw(@"{ ""term"" : ""value"" }"));
			var serialized = _client.Serializer.Serialize(registerPercolator).Utf8String();
			serialized.Should().Contain("term");

		}
	}
}
