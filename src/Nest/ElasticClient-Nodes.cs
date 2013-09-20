using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public partial class ElasticClient
    {
        /// <summary>
        /// Gets the health status of the cluster.
        /// </summary>
        public INodeInfoResponse NodeInfo(NodesInfo nodesInfo)
        {
            var path = this.PathResolver.CreateNodePath();
            return _NodeInfo(path, nodesInfo);
        }

        /// <summary>
        /// Gets the health status of the cluster, for the specified nodes.
        /// </summary>
        public INodeInfoResponse NodeInfo(IEnumerable<string> nodes, NodesInfo nodesInfo)
        {
            var path = this.PathResolver.CreateNodePath(nodes);
            return _NodeInfo(path, nodesInfo);
        }

        private NodeInfoResponse _NodeInfo(string path, NodesInfo nodesInfo)
        {
            if (nodesInfo.HasFlag(NodesInfo.All))
            {
                path += "?all=true";
            }
            else
            {
                var options = new List<string>();
                if (nodesInfo.HasFlag(NodesInfo.Settings))
                    options.Add("settings=true");
                if (nodesInfo.HasFlag(NodesInfo.OS))
                    options.Add("os=true");
                if (nodesInfo.HasFlag(NodesInfo.Process))
                    options.Add("process=true");
                if (nodesInfo.HasFlag(NodesInfo.JVM))
                    options.Add("jvm=true");
                if (nodesInfo.HasFlag(NodesInfo.ThreadPool))
                    options.Add("thread_pool=true");
                if (nodesInfo.HasFlag(NodesInfo.Network))
                    options.Add("network=true");
                if (nodesInfo.HasFlag(NodesInfo.Transport))
                    options.Add("transport=true");
                if (nodesInfo.HasFlag(NodesInfo.HTTP))
                    options.Add("http=true");
                path += "?" + string.Join("&", options);
            }
            var status = this.Connection.GetSync(path);
            var r = this.Deserialize<NodeInfoResponse>(status);
            return r;
        }

        /// <summary>
        /// Gets the health status of each node in the cluster, for the specified indexes.
        /// </summary>
        public INodeStatsResponse NodeStats(NodeInfoStats nodeInfoStats)
        {
            var path = this.PathResolver.CreateNodePath("stats");
            return this._NodeStats(path, nodeInfoStats);
        }

        /// <summary>
        /// Gets the health status of each node in the cluster, for the specified indexes.
        /// </summary>
        public INodeStatsResponse NodeStats(IEnumerable<string> nodes, NodeInfoStats nodeInfoStats)
        {
            var path = this.PathResolver.CreateNodePath(nodes, "stats");
            return this._NodeStats(path, nodeInfoStats);
        }

		private NodeStatsResponse _NodeStats(string path, NodeInfoStats nodeInfoStats)
        {
            if (nodeInfoStats.HasFlag(NodeInfoStats.All))
            {
                path += "?all=true";
            }
            else
            {
                var options = new List<string>();
                if (nodeInfoStats.HasFlag(NodeInfoStats.FileSystem))
                    options.Add("fs=true");
                if (nodeInfoStats.HasFlag(NodeInfoStats.Indices))
                    options.Add("indices=true");
                if (nodeInfoStats.HasFlag(NodeInfoStats.OS))
                    options.Add("os=true");
                if (nodeInfoStats.HasFlag(NodeInfoStats.Process))
                    options.Add("process=true");
                if (nodeInfoStats.HasFlag(NodeInfoStats.JVM))
                    options.Add("jvm=true");
                if (nodeInfoStats.HasFlag(NodeInfoStats.ThreadPool))
                    options.Add("thread_pool=true");
                if (nodeInfoStats.HasFlag(NodeInfoStats.Network))
                    options.Add("network=true");
                if (nodeInfoStats.HasFlag(NodeInfoStats.Transport))
                    options.Add("transport=true");
                if (nodeInfoStats.HasFlag(NodeInfoStats.HTTP))
                    options.Add("http=true");
                path += "?" + string.Join("&", options);
            }
            var status = this.Connection.GetSync(path);
            var r = this.Deserialize<NodeStatsResponse>(status);
            return r;
        }
	}
}
