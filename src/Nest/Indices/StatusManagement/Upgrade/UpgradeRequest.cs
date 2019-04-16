using Elasticsearch.Net;

namespace Nest
{
	[MapsApi("indices.upgrade.json")]
	public partial interface IUpgradeRequest  { }

	public partial class UpgradeRequest { }

	public partial class UpgradeDescriptor { }
}
