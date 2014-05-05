using System.Linq;
using Elasticsearch.Net.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.RawCalls
{
	[TestFixture]
	public class HasUsefultServerExceptionTests : IntegrationTests
	{
		[Test]
		public void MaxRetryException_DoesNotHide_ElasticsearchServerException()
		{
			Assert.Throws<MaxRetryException>(() =>
			{
				var result = this._client.Raw.Search("{ size: 10, searc}");
			});
			
		}
		
	}
}
