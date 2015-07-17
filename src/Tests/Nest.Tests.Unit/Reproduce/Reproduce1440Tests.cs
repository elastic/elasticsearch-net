using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce1440Tests : BaseJsonTests
	{
		[Test]
		public void CountShouldNotThrowWhenNoDefaultIndexSpecified()
		{
			var client = new ElasticClient();
			var request = client.Count<ElasticsearchProject>();
			var path = new Uri(request.ConnectionStatus.RequestUrl).AbsolutePath;
			path.Should().Be("/_all/elasticsearchprojects/_count");
			request = client.Count<ElasticsearchProject>(c=>c.Index("x"));
			path = new Uri(request.ConnectionStatus.RequestUrl).AbsolutePath;
			path.Should().Be("/x/elasticsearchprojects/_count");
		}

	}
}
