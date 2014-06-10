using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<StatisticalFacetRequest>))]
	public interface IStatisticalFacetRequest : IFacetRequest
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> Fields { get; set; }

		[JsonProperty(PropertyName = "script")]
		string Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }
	}

	public class StatisticalFacetRequest : FacetRequest, IStatisticalFacetRequest
	{
		public PropertyPathMarker Field { get; set; }
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
		public string Script { get; set; }
		public Dictionary<string, object> Params { get; set; }
	}

	public class StatisticalFacetDescriptor<T> : BaseFacetDescriptor<StatisticalFacetDescriptor<T>, T>, IStatisticalFacetRequest where T : class
	{
		protected IStatisticalFacetRequest Self { get { return this; } }

		PropertyPathMarker IStatisticalFacetRequest.Field { get; set; }

		IEnumerable<PropertyPathMarker> IStatisticalFacetRequest.Fields { get; set; }

		string IStatisticalFacetRequest.Script { get; set; }

		Dictionary<string, object> IStatisticalFacetRequest.Params { get; set; }

		public StatisticalFacetDescriptor<T> OnField(string field)
		{
			field.ThrowIfNullOrEmpty("field");
			Self.Fields = null;
			Self.Field = field;
			return this;
		}
		public StatisticalFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.Fields = null;
			Self.Field = objectPath;
			return this;
		}
		public StatisticalFacetDescriptor<T> OnFields(params string[] fields)
		{
			fields.ThrowIfEmpty("fields");
			Self.Field = null;
			Self.Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public StatisticalFacetDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths)
		{
			objectPaths.ThrowIfEmpty("objectPaths");
			Self.Field = null;
			Self.Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public StatisticalFacetDescriptor<T> Script(string script)
		{
			script.ThrowIfNull("script");
			Self.Script = script;
			return this;
		}

		public StatisticalFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

	}
}
