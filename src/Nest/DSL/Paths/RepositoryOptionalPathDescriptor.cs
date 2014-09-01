using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IRepositoryOptionalPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		string RepositoryName { get; set; }
	}

	internal static class RepositoryOptionalPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IRepositoryOptionalPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			pathInfo.Repository = path.RepositoryName;
		}
	
	}

	public abstract class RepositoryOptionalPathBase<TParameters> : BasePathRequest<TParameters>, IRepositoryOptionalPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public string RepositoryName { get; set; }
		
		public RepositoryOptionalPathBase()
		{
		}

		public RepositoryOptionalPathBase(string repositoryName)
		{
			this.RepositoryName = repositoryName;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			RepositoryOptionalPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path that contains a
	/// <pre>
	///	{repository}
	/// </pre>
	/// routing value
	/// </summary>
	public abstract class RepositoryOptionalPathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IRepositoryOptionalPath<TParameters>
		where TDescriptor : RepositoryOptionalPathDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IRepositoryOptionalPath<TParameters> Self { get { return this; } }
		string IRepositoryOptionalPath<TParameters>.RepositoryName { get; set; }

		/// <summary>
		/// Specify the name of the repository we are targeting
		/// </summary>
		public TDescriptor Repository(string repositoryName)
		{
			Self.RepositoryName = repositoryName;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			RepositoryOptionalPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
