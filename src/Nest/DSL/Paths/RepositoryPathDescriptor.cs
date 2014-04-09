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
	public class RepositoryPathDescriptor<P, K> : BasePathDescriptor<P>
		where P : RepositoryPathDescriptor<P, K> 
		where K : FluentRequestParameters<K>, new()
	{
		internal string _RepositoryName { get; set; }

		/// <summary>
		/// Specify the name of the repository we are targeting
		/// </summary>
		public P Repository(string repositoryName)
		{
			this._RepositoryName = repositoryName;
			return (P)this;
		}

		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentRequestParameters<K>, new()
		{
			if (this._RepositoryName.IsNullOrEmpty())
				throw new DslException("missing Repository()");

			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Repository = this._RepositoryName
			};
			pathInfo.RequestParameters = queryString ?? new K();
			pathInfo.RequestParameters.RequestConfiguration(r=>this._RequestConfiguration);
			return pathInfo;
		}

	}
}
