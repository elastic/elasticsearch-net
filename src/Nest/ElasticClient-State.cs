using System.Collections.Generic;

namespace Nest
{
    public partial class ElasticClient
    {
        /// <summary>
        /// Gets the health status of the cluster.
        /// </summary>
        public IClusterStateResponse ClusterState(ClusterStateInfo stateInfo, IEnumerable<string> indices = null)
        {
            var path = this.PathResolver.CreateClusterPath("state");

            var options = new List<string>();
            if (indices != null && indices.HasAny() && (!stateInfo.HasFlag(ClusterStateInfo.ExcludeMetadata)))
            {
                options.Add("filter_indices=" + string.Join(",", indices));
            }


            if (stateInfo.HasFlag(ClusterStateInfo.ExcludeNodes))
                options.Add("filter_nodes=true");
            if (stateInfo.HasFlag(ClusterStateInfo.ExcludeRoutingTable))
                options.Add("filter_routing_table=true");
            if (stateInfo.HasFlag(ClusterStateInfo.ExcludeMetadata))
                options.Add("filter_metadata=true");
            if (stateInfo.HasFlag(ClusterStateInfo.ExcludeBlocks))
                options.Add("filter_blocks=true");

            path += "?" + string.Join("&", options);

            var status = this.Connection.GetSync(path);
            var r = this.ToParsedResponse<ClusterStateResponse>(status);
            return r;
        }
    }
}
