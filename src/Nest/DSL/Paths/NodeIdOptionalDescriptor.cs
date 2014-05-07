using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{nodeid}
	/// </pre>
	/// node id is optional
	/// </summary>
	public class NodeIdOptionalDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor>
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

		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			var pathInfo = base.ToPathInfo(queryString);
			pathInfo.NodeId = this._NodeId;
			return pathInfo;
		}

	}
}
