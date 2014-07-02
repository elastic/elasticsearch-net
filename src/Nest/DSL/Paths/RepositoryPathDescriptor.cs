using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public interface IRepositoryPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		string RepositoryName { get; set; }
	}

	internal static class RepositoryPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IRepositoryPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			if (path.RepositoryName.IsNullOrEmpty())
				throw new DslException("missing Repository()");

			pathInfo.Repository = path.RepositoryName;
		}
	
	}

	public abstract class RepositoryPathBase<TParameters> : BasePathRequest<TParameters>, IRepositoryPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public string RepositoryName { get; set; }

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			RepositoryPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path that contains a
	/// <pre>
	///	{repository}
	/// </pre>
	/// routing value
	/// </summary>
	public abstract class RepositoryPathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IRepositoryPath<TParameters>
		where TDescriptor : RepositoryPathDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IRepositoryPath<TParameters> Self { get { return this; } }
		string IRepositoryPath<TParameters>.RepositoryName { get; set; }

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
			RepositoryPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
