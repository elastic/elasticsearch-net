
using Elasticsearch.Net;
using Elasticsearch.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface INodesHotThreadsRequest 
		: INodeIdOptionalPath<NodesHotThreadsRequestParameters> { }

	public partial class NodesHotThreadsRequest 
		: NodeIdOptionalPathBase<NodesHotThreadsRequestParameters>, INodesHotThreadsRequest { }

	public partial class NodesHotThreadsDescriptor 
		: NodeIdOptionalDescriptor<NodesHotThreadsDescriptor, NodesHotThreadsRequestParameters>, INodesHotThreadsRequest { }
}
