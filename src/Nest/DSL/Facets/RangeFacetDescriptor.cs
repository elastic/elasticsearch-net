using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public interface IRangeFacetRequest<K> : IFacetRequest
		where K : struct
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "key_field")]
		PropertyPathMarker KeyField { get; set; }

		[JsonProperty(PropertyName = "value_field")]
		PropertyPathMarker ValueField { get; set; }

		[JsonProperty(PropertyName = "key_script")]
		string KeyScript { get; set; }

		[JsonProperty(PropertyName = "value_script")]
		string ValueScript { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<Range<K>> Ranges { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }
	}

	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public class RangeFacetRequest<K> : FacetRequest, IRangeFacetRequest<K>
		where K : struct
	{
		public PropertyPathMarker Field { get; set; }
		public PropertyPathMarker KeyField { get; set; }
		public PropertyPathMarker ValueField { get; set; }
		public string KeyScript { get; set; }
		public string ValueScript { get; set; }
		public IEnumerable<Range<K>> Ranges { get; set; }
		public Dictionary<string, object> Params { get; set; }
	}

	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public class RangeFacetDescriptor<T, K> : BaseFacetDescriptor<RangeFacetDescriptor<T, K>, T>, IRangeFacetRequest<K>
		where T : class
		where K : struct
	{
		private IRangeFacetRequest<K> Self { get { return this; } }

		PropertyPathMarker IRangeFacetRequest<K>.Field { get; set; }

		PropertyPathMarker IRangeFacetRequest<K>.KeyField { get; set; }

		PropertyPathMarker IRangeFacetRequest<K>.ValueField { get; set; }

		string IRangeFacetRequest<K>.KeyScript { get; set; }

		string IRangeFacetRequest<K>.ValueScript { get; set; }

		IEnumerable<Range<K>> IRangeFacetRequest<K>.Ranges { get; set; }

		Dictionary<string, object> IRangeFacetRequest<K>.Params { get; set; }

		public RangeFacetDescriptor<T, K> OnField(string field)
		{
			field.ThrowIfNull("field");
			Self.Field = field;
			return this;
		}
		public RangeFacetDescriptor<T, K> KeyField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.KeyField = objectPath;
			return this;
		}
		public RangeFacetDescriptor<T, K> KeyField(string keyField)
		{
			keyField.ThrowIfNull("keyField");
			Self.KeyField = keyField;
			return this;
		}
		public RangeFacetDescriptor<T, K> KeyScript(string keyScript)
		{
			keyScript.ThrowIfNull("keyScript");
			Self.KeyScript = keyScript;
			return this;
		}
		public RangeFacetDescriptor<T, K> ValueField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.ValueField = objectPath;
			return this;
		}
		public RangeFacetDescriptor<T, K> ValueField(string valueField)
		{
			valueField.ThrowIfNull("valueField");
			Self.ValueField = valueField;
			return this;
		}
		public RangeFacetDescriptor<T, K> ValueScript(string valueScript)
		{
			valueScript.ThrowIfNull("valueScript");
			Self.ValueScript = valueScript;
			return this;
		}
		public RangeFacetDescriptor<T, K> OnField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.Field = objectPath;
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
			Self.Ranges = newRanges;
			return this;
		}
		public RangeFacetDescriptor<T, K> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

	}
}
