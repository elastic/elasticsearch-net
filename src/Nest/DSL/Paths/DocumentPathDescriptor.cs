using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
	public class DocumentPathDescriptorBase<TDescriptor, T, TParameters> : BasePathDescriptor<TDescriptor>
		where TDescriptor : DocumentPathDescriptorBase<TDescriptor, T, TParameters>, new()
		where T : class
		where TParameters : FluentRequestParameters<TParameters>, new()
	{

		internal IndexNameMarker _Index { get; set; }
		internal TypeNameMarker _Type { get; set; }
		internal string _Id { get; set; }
		internal T _Object { get; set; }

		public TDescriptor Index(string index)
		{
			this._Index = index;
			return (TDescriptor)this;
		}

		public TDescriptor Index(Type index)
		{
			this._Index = index;
			return (TDescriptor)this;
		}

		public TDescriptor Index<TAlternative>() where TAlternative : class
		{
			this._Index = typeof(TAlternative);
			return (TDescriptor)this;
		}

		public TDescriptor Type(string type)
		{
			this._Type = type;
			return (TDescriptor)this;
		}
		public TDescriptor Type(Type type)
		{
			this._Type = type;
			return (TDescriptor)this;
		}
		public TDescriptor Type<TAlternative>() where TAlternative : class
		{
			this._Type = typeof(TAlternative);
			return (TDescriptor)this;
		}
		public TDescriptor Id(long id)
		{
			return this.Id(id.ToString());
		}
		public TDescriptor Id(string id)
		{
			this._Id = id;
			return (TDescriptor)this;
		}
		public TDescriptor Object(T @object)
		{
			this._Object = @object;
			return (TDescriptor)this;
		}
		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			var inferrer = new ElasticInferrer(settings);
			var index = this._Index != null ? inferrer.IndexName(this._Index) : inferrer.IndexName<T>();
			var type = this._Type != null ? inferrer.TypeName(this._Type) : inferrer.TypeName<T>();
			var id = this._Id ?? inferrer.Id(this._Object);

			var pathInfo = base.ToPathInfo(queryString);
			pathInfo.Index = index;
			pathInfo.Type = type;
			pathInfo.Id = id;
			return pathInfo;
		}

	}
}
