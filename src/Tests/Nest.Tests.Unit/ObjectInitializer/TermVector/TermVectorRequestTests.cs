using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.ObjectInitializer.TermVector
{
	public class TermVectorRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public TermVectorRequestTests()
		{
			var request = new TermvectorRequest("my_index", "my_type", "1")
				{
					Offsets = true,
					Payloads = true,
					Positions = true,
					TermStatistics = true,
					FieldStatistics = true
				};

			var response = this._client.TermVector(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith(
				"/my_index/my_type/1/_termvector?offsets=true&payloads=true&positions=true&term_statistics=true&field_statistics=true");
			this._status.RequestMethod.Should().Be("GET");
		}
	}
}
