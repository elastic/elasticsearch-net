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
	/// Provides a base for descriptors that need to describe a path that contains a
	/// <pre>
	///	{repository}
	/// </pre>
	/// routing value
	/// </summary>
	public class RepositorySnapshotPathDescriptor<P, K> : BasePathDescriptor<P>
		where P : RepositorySnapshotPathDescriptor<P, K> 
		where K : FluentRequestParameters<K>, new()
	{
		internal string _Repository { get; set; }
		internal string _Snapshot { get; set; }

		/// <summary>
		/// Specify the name of the repository we are targeting
		/// </summary>
		public P Repository(string repositoryName)
		{
			this._Repository = repositoryName;
			return (P)this;
		}

		public P Snapshot(string snapshotName)
		{
			this._Snapshot = snapshotName;
			return (P)this;
		}
		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentRequestParameters<K>, new()
		{
			if (this._Repository.IsNullOrEmpty())
				throw new DslException("missing Repository()");
			if (this._Snapshot.IsNullOrEmpty())
				throw new DslException("missing Snapshot()");

			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Repository = this._Repository,
				Snapshot = this._Snapshot

			};
			pathInfo.RequestParameters = queryString ?? new K();
			pathInfo.RequestParameters.RequestConfiguration(r=>this._RequestConfiguration);
			return pathInfo;
		}

	}
}
