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
	///	/{index}/{type}/{id}
	/// </pre>
	/// if one of the parameters is not explicitly specified this will fall back to the defaults for type <para>T</para>
	/// </summary>
	public class DocumentPathDescriptorBase<P, T, K>
		where P : DocumentPathDescriptorBase<P, T, K>, new()
		where T : class
		where K : FluentRequestParameters<K>, new()
	{

		internal IndexNameMarker _Index { get; set; }
		internal TypeNameMarker _Type { get; set; }
		internal string _Id { get; set; }
		internal T _Object { get; set; }

		public P Index(string index)
		{
			this._Index = index;
			return (P)this;
		}

		public P Index(Type index)
		{
			this._Index = index;
			return (P)this;
		}

		public P Index<TAlternative>() where TAlternative : class
		{
			this._Index = typeof(T);
			return (P)this;
		}

		public P Type(string type)
		{
			this._Type = type;
			return (P)this;
		}
		public P Type(Type type)
		{
			this._Type = type;
			return (P)this;
		}
		public P Type<TAlternative>() where TAlternative : class
		{
			this._Type = typeof(TAlternative);
			return (P)this;
		}
		public P Id(int id)
		{
			return this.Id(id.ToString());
		}
		public P Id(string id)
		{
			this._Id = id;
			return (P)this;
		}
		public P Object(T @object)
		{
			this._Object = @object;
			return (P)this;
		}
		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentRequestParameters<K>, new()
		{
			var inferrer = new ElasticInferrer(settings);
			var index = this._Index != null ? inferrer.IndexName(this._Index) : inferrer.IndexName<T>();
			var type = this._Type != null ? inferrer.TypeName(this._Type) : inferrer.TypeName<T>();
			var id = this._Id ?? inferrer.Id(this._Object);
			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Index = index,
				Type = type,
				Id = id
			};
			pathInfo.QueryString = queryString ?? new K();
			return pathInfo;
		}

	}
}
