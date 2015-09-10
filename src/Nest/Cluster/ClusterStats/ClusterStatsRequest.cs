using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IClusterStatsRequest : IRequest<ClusterStatsRequestParameters> { }

	public partial class ClusterStatsRequest : BasePathRequest<ClusterStatsRequestParameters>, IClusterStatsRequest { }

	public partial class ClusterStatsDescriptor 
		: BasePathDescriptor<ClusterStatsDescriptor, ClusterStatsRequestParameters>, IClusterStatsRequest { }
}
