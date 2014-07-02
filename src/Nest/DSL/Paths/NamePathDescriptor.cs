using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public interface INamePath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		string Name { get; set; }
	}

	internal static class NamePathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			INamePath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			if (path.Name.IsNullOrEmpty())
				throw new DslException("missing Repository()");

			pathInfo.Name = path.Name;
		}
	
	}

	public abstract class NamePathBase<TParameters> : BasePathRequest<TParameters>, INamePath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public string Name { get; set; }
		
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			NamePathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{name}
	/// </pre>
	/// name is mandatory.
	/// </summary>
	public abstract class NamePathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, INamePath<TParameters>
		where TDescriptor : NamePathDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private INamePath<TParameters> Self { get { return this; } } 

		string INamePath<TParameters>.Name { get; set; }

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public TDescriptor Name(string name)
		{
			Self.Name = name;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			NamePathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
