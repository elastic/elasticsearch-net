using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{nodeid}
	/// </pre>
	/// node id is optional
	/// </summary>
	public abstract class NodeIdOptionalDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor, TParameters>
		where TDescriptor : NodeIdOptionalDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal string _NodeId { get; set; }

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public TDescriptor NodeId(string nodeId)
		{
			this._NodeId = nodeId;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			pathInfo.NodeId = this._NodeId;
		}

	}
}
