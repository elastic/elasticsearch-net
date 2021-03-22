using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;

namespace Tests.Framework.EndpointTests
{
	public abstract class UrlTestsBase
	{
		[U] public abstract Task Urls();
	}
}
