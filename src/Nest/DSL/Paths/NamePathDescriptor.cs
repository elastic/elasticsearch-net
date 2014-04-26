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
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{name}
	/// </pre>
	/// name is mandatory.
	/// </summary>
	public class NamePathDescriptor<P, K> : BasePathDescriptor<P>
		where P : NamePathDescriptor<P, K> 
		where K : FluentRequestParameters<K>, new()
	{
		internal string _Name { get; set; }

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public P Name(string name)
		{
			this._Name = name;
			return (P)this;
		}

		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentRequestParameters<K>, new()
		{
			if (this._Name.IsNullOrEmpty())
				throw new DslException("missing Repository()");

			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Name = this._Name
			};
			pathInfo.RequestParameters = queryString ?? new K();
			pathInfo.RequestParameters.RequestConfiguration(r=>this._RequestConfiguration);
			return pathInfo;
		}

	}
}
