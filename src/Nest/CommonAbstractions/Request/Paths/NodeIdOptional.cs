using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface INodeIdOptionalPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		string NodeId { get; set; }
	}

	internal static class NodeIdOptionalPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			INodeIdOptionalPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			pathInfo.NodeId = path.NodeId;
		}
	
	}

	public abstract class NodeIdOptionalPathBase<TParameters> : PathRequestBase<TParameters>, INodeIdOptionalPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			NodeIdOptionalPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

		public string NodeId { get; set; }
	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{nodeid}
	/// </pre>
	/// node id is optional
	/// </summary>
	public abstract class NodeIdOptionalDescriptor<TDescriptor, TParameters>
		: PathDescriptorBase<TDescriptor, TParameters>, INodeIdOptionalPath<TParameters>
		where TDescriptor : NodeIdOptionalDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private INodeIdOptionalPath<TParameters> Self => this;
		string INodeIdOptionalPath<TParameters>.NodeId { get; set; }

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public TDescriptor NodeId(string nodeId)
		{
			Self.NodeId = nodeId;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			NodeIdOptionalPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
