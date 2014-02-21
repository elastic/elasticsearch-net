using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RangeFacetDescriptor<T, K> : BaseFacetDescriptor<RangeFacetDescriptor<T, K>,T>
		where T : class
		where K : struct
	{
		[JsonProperty(PropertyName = "field")]
		internal PropertyPathMarker _Field { get; set; }

		[JsonProperty(PropertyName = "key_field")]
		internal PropertyPathMarker _KeyField { get; set; }

		[JsonProperty(PropertyName = "value_field")]
		internal PropertyPathMarker _ValueField { get; set; }

		[JsonProperty(PropertyName = "key_script")]
		internal string _KeyScript { get; set; }

		[JsonProperty(PropertyName = "value_script")]
		internal string _ValueScript { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		internal IEnumerable<Range<K>> _Ranges { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> _Params { get; set; }

		public RangeFacetDescriptor<T, K> OnField(string field)
		{
			field.ThrowIfNull("field");
			this._Field = field;
			return this;
		}
		public RangeFacetDescriptor<T, K> KeyField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this._KeyField = objectPath;
			return this;
		}
		public RangeFacetDescriptor<T, K> KeyField(string keyField)
		{
			keyField.ThrowIfNull("keyField");
			this._KeyField = keyField;
			return this;
		}
		public RangeFacetDescriptor<T, K> KeyScript(string keyScript)
		{
			keyScript.ThrowIfNull("keyScript");
			this._KeyScript = keyScript;
			return this;
		}
		public RangeFacetDescriptor<T, K> ValueField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this._ValueField = objectPath;
			return this;
		}
		public RangeFacetDescriptor<T, K> ValueField(string valueField)
		{
			valueField.ThrowIfNull("valueField");
			this._ValueField = valueField;
			return this;
		}
		public RangeFacetDescriptor<T, K> ValueScript(string valueScript)
		{
			valueScript.ThrowIfNull("valueScript");
			this._ValueScript = valueScript;
			return this;
		}
		public RangeFacetDescriptor<T, K> OnField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this._Field = objectPath;
			return this;
		}
		public RangeFacetDescriptor<T, K> Ranges(params Func<Range<K>, Range<K>>[] ranges)
		{
			var newRanges = new List<Range<K>>();
			foreach (var range in ranges)
			{
				var r = new Range<K>();
				newRanges.Add(range(r));
			}
			this._Ranges = newRanges;
			return this;
		}
		public RangeFacetDescriptor<T, K> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

	}
}
