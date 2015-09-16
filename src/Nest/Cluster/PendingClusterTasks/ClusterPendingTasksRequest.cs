using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IClusterPendingTasksRequest : IRequest<ClusterPendingTasksRequestParameters> { }

	public partial class ClusterPendingTasksRequest 
		: RequestBase<ClusterPendingTasksRequestParameters>, IClusterPendingTasksRequest { }

	public partial class ClusterPendingTasksDescriptor 
		: RequestDescriptorBase<ClusterPendingTasksDescriptor, ClusterPendingTasksRequestParameters>, IClusterPendingTasksRequest { }
}
