using Elasticsearch.Net_5_2_0;

namespace Nest_5_2_0
{
	public partial interface IUpgradeRequest : IRequest<UpgradeRequestParameters> { }

	public partial class UpgradeRequest { }

	[DescriptorFor("IndicesUpgrade")]
	public partial class UpgradeDescriptor { }
}
