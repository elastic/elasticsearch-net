
using Elasticsearch.Net;
using Elasticsearch.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface INodesHotThreadsRequest 
        : IRequest<NodesHotThreadsRequestParameters> { }

	public partial class NodesHotThreadsRequest 
		: RequestBase<NodesHotThreadsRequestParameters>, INodesHotThreadsRequest
    {
        public NodesHotThreadsRequest() { }

        public NodesHotThreadsRequest(string nodeId)
            : base(p => p.RequiredNodeId(nodeId))
        { }
    }

	public partial class NodesHotThreadsDescriptor 
		: RequestDescriptorBase<NodesHotThreadsDescriptor, NodesHotThreadsRequestParameters>, INodesHotThreadsRequest
    {
        public NodesHotThreadsDescriptor() { }

        public NodesHotThreadsDescriptor(string nodeId)
            : base(p => p.RequiredNodeId(nodeId))
        { }
    }
}
