using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path that contains a
	/// <pre>
	///	{repository}
	/// </pre>
	/// routing value
	/// </summary>
	public abstract class RepositorySnapshotPathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor, TParameters>
		where TDescriptor : RepositorySnapshotPathDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal string _Repository { get; set; }
		internal string _Snapshot { get; set; }

		/// <summary>
		/// Specify the name of the repository we are targeting
		/// </summary>
		public TDescriptor Repository(string repositoryName)
		{
			this._Repository = repositoryName;
			return (TDescriptor)this;
		}

		public TDescriptor Snapshot(string snapshotName)
		{
			this._Snapshot = snapshotName;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			if (this._Repository.IsNullOrEmpty())
				throw new DslException("missing Repository()");
			if (this._Snapshot.IsNullOrEmpty())
				throw new DslException("missing Snapshot()");

			pathInfo.Repository = this._Repository;
			pathInfo.Snapshot = this._Snapshot;
		}

	}
}
