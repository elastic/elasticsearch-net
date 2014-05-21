using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<TermFilter>,CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermFilter : IFilter, ICustomJson
	{
		PropertyPathMarker Field { get; set; }
		object Value { get; set; }
		double? Boost { get; set; }
	}

	public class TermFilter : FilterBase, ITermFilter, ICustomJsonReader<TermFilter>
	{
		PropertyPathMarker ITermFilter.Field { get; set; }
		object ITermFilter.Value { get; set; }
		double? ITermFilter.Boost { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				var tf = ((ITermFilter)this);
				return tf.Value == null || tf.Value.ToString().IsNullOrEmpty() || tf.Field.IsConditionless();
			}
		}

		object ICustomJson.GetCustomJson()
		{
			var tf = ((ITermFilter)this);
			return this.FieldNameAsKeyFormat(tf.Field, tf.Value);
		}

		public TermFilter FromJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var filter = new TermFilter();
			var dict = base.ReadToDict(reader, serializer, filter);
			if (dict.Count == 0) return filter;

			var firstProp = dict.First();
			((ITermFilter)filter).Field = firstProp.Key;
			((ITermFilter)filter).Value = firstProp.Value;
			return filter;
		}
	}
}
