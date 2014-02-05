using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class StatisticalFacetDescriptor<T> : BaseFacetDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "field")]
		internal PropertyPathMarker _Field { get; set; }

		[JsonProperty(PropertyName = "fields")]
		internal IEnumerable<PropertyPathMarker> _Fields { get; set; }

		[JsonProperty(PropertyName = "script")]
		internal string _Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> _Params { get; set; }

		public StatisticalFacetDescriptor<T> OnField(string field)
		{
			field.ThrowIfNullOrEmpty("field");
			this._Fields = null;
			this._Field = field;
			return this;
		}
		public StatisticalFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this._Fields = null;
			this._Field = objectPath;
			return this;
		}
		public StatisticalFacetDescriptor<T> OnFields(params string[] fields)
		{
			fields.ThrowIfEmpty("fields");
			this._Field = null;
			this._Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public StatisticalFacetDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths)
		{
			objectPaths.ThrowIfEmpty("objectPaths");
			this._Field = null;
			this._Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public StatisticalFacetDescriptor<T> Script(string script)
		{
			script.ThrowIfNull("script");
			this._Script = script;
			return this;
		}

		public StatisticalFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public new StatisticalFacetDescriptor<T> Global()
		{
			this._IsGlobal = true;
			return this;
		}
		public new StatisticalFacetDescriptor<T> FacetFilter(
		  Func<FilterDescriptor<T>, BaseFilter> facetFilter
		)
		{
			var filter = facetFilter(new FilterDescriptor<T>());
			if (filter.IsConditionless)
				filter = null;
			this._FacetFilter = filter;
			return this;
		}
		public new StatisticalFacetDescriptor<T> Nested(string nested)
		{
			this._Nested = nested;
			return this;
		}
		public new StatisticalFacetDescriptor<T> Scope(string scope)
		{
			this._Scope = scope;
			return this;
		}
	}
}
