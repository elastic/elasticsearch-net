using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public interface IRepositorySnapshotPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		string Repository { get; set; }
		string Snapshot { get; set; }
	}

	internal static class RepositorySnapshotPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IRepositorySnapshotPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			if (path.Repository.IsNullOrEmpty())
				throw new DslException("missing Repository()");
			if (path.Snapshot.IsNullOrEmpty())
				throw new DslException("missing Snapshot()");

			pathInfo.Repository = path.Repository;
			pathInfo.Snapshot = path.Snapshot;
		}
	
	}

	public abstract class RepositorySnapshotPathBase<TParameters> : BasePathRequest<TParameters>, IRepositorySnapshotPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public string Repository { get; set; }

		public string Snapshot { get; set; }

		public RepositorySnapshotPathBase(string repository, string snapshot)
		{
			repository.ThrowIfNullOrEmpty("repository");
			snapshot.ThrowIfNullOrEmpty("snapshot");
			this.Repository = repository;
			this.Snapshot = snapshot;
		}


		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			RepositorySnapshotPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}
	/// <summary>
	/// Provides a base for descriptors that need to describe a path that contains a
	/// <pre>
	///	{repository}
	/// </pre>
	/// routing value
	/// </summary>
	public abstract class RepositorySnapshotPathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IRepositorySnapshotPath<TParameters>
		where TDescriptor : RepositorySnapshotPathDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IRepositorySnapshotPath<TParameters> Self { get { return this; } }

		string IRepositorySnapshotPath<TParameters>.Repository { get; set; }

		string IRepositorySnapshotPath<TParameters>.Snapshot { get; set; }

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

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			RepositorySnapshotPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
