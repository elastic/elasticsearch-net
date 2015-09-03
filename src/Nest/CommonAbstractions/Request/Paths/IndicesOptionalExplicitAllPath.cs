using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	public interface IIndicesOptionalExplicitAllPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		Indices Indices { get; set; }
	}

	internal static class IndicesOptionalExplicitAllPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IIndicesOptionalExplicitAllPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			pathInfo.Index = ((IUrlParameter)path.Indices).GetString(settings);
		}
	
	}

	public abstract class IndicesOptionalExplicitAllPathBase<TParameters> : BasePathRequest<TParameters>, IIndicesOptionalExplicitAllPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		Indices IIndicesOptionalExplicitAllPath<TParameters>.Indices { get; set; }

		public IndicesOptionalExplicitAllPathBase(Indices indices)
		{
			indices.ThrowIfNull(nameof(indices));
			((IIndicesOptionalExplicitAllPath<TParameters>)this).Indices = indices;
		}
		
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo) =>
			IndicesOptionalExplicitAllPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}
	/// </pre>
	/// {indices} is optional but AllIndices() needs to be explicitly called.
	/// </summary>
	public abstract class IndicesOptionalExplicitAllPathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IIndicesOptionalExplicitAllPath<TParameters>
		where TDescriptor : IndicesOptionalExplicitAllPathDescriptor<TDescriptor, TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IIndicesOptionalExplicitAllPath<TParameters> Self => this;

		Indices IIndicesOptionalExplicitAllPath<TParameters>.Indices { get; set; }

		public IndicesOptionalExplicitAllPathDescriptor(Indices indices)
		{
			indices.ThrowIfNull(nameof(indices));
			((IIndicesOptionalExplicitAllPath<TParameters>)this).Indices = indices;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			IndicesOptionalExplicitAllPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
