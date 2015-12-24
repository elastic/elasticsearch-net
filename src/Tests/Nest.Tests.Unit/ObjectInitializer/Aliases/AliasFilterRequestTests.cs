using System.Reflection;
using Elasticsearch.Net;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Aliases
{
	[TestFixture]
	public class AliasFilterRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public AliasFilterRequestTests()
		{
			var response = _client.Alias(x => x.Add(
					addAction => addAction
						.Index("myindex-2014-2-2")
						.Alias("myindex")
						.Filter<object>(filter => filter.Raw(" { \"term\" : { \"test\" : \"this_test\" } }"))
				));
			_status = response.ConnectionStatus;
		}

		[Test]
		public void AliasFilterBody()
		{
			JsonEquals(_status.Request, MethodBase.GetCurrentMethod());
		}
	}
}
