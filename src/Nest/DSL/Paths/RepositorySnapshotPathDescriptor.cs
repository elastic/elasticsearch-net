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
using Shared.Extensions;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path that contains a
	/// <pre>
	///	{repository}
	/// </pre>
	/// routing value
	/// </summary>
	public class RepositorySnapshotPathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor>
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
		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			if (this._Repository.IsNullOrEmpty())
				throw new DslException("missing Repository()");
			if (this._Snapshot.IsNullOrEmpty())
				throw new DslException("missing Snapshot()");

			var pathInfo = base.ToPathInfo(queryString);
			pathInfo.Repository = this._Repository;
			pathInfo.Snapshot = this._Snapshot;

			return pathInfo;
		}

	}
}
