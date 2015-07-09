using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IRepositorySnapshotOptionalPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		string Repository { get; set; }
		string Snapshot { get; set; }
	}

	internal static class RepositorySnapshotOptionalPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IRepositorySnapshotOptionalPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			pathInfo.Repository = path.Repository;
			if (!path.Repository.IsNullOrEmpty())
				pathInfo.Snapshot = path.Snapshot;
		}
	
	}

	public abstract class RepositorySnapshotOptionalPathBase<TParameters> : BasePathRequest<TParameters>, IRepositorySnapshotOptionalPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public string Repository { get; set; }

		public string Snapshot { get; set; }
		
		public RepositorySnapshotOptionalPathBase() {}

		public RepositorySnapshotOptionalPathBase(string repository, params string[] snapshots)
		{
			repository.ThrowIfNullOrEmpty("repository");
			this.Repository = repository;
			if (snapshots.HasAny())
				this.Snapshot = string.Join(",", snapshots);
		}


		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			RepositorySnapshotOptionalPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}
	/// <summary>
	/// Provides a base for descriptors that need to describe a path that contains a
	/// <pre>
	///	{repository}
	/// </pre>
	/// routing value
	/// </summary>
	public abstract class RepositorySnapshotOptionalPathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IRepositorySnapshotOptionalPath<TParameters>
		where TDescriptor : RepositorySnapshotOptionalPathDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IRepositorySnapshotOptionalPath<TParameters> Self => this;

		string IRepositorySnapshotOptionalPath<TParameters>.Repository { get; set; }

		string IRepositorySnapshotOptionalPath<TParameters>.Snapshot { get; set; }

		/// <summary>
		/// Specify the name of the repository we are targeting
		/// </summary>
		public TDescriptor Repository(string repositoryName)
		{
			Self.Repository = repositoryName;
			return (TDescriptor)this;
		}

		public TDescriptor Snapshot(string snapshotName)
		{
			Self.Snapshot = snapshotName;
			return (TDescriptor)this;
		}
		
		public TDescriptor Snapshots(params string[] snapshots)
		{
			Self.Snapshot = string.Join(",", snapshots);
			return (TDescriptor)this;
		}
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			RepositorySnapshotOptionalPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
