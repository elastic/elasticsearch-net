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
	[JsonConverter(typeof(CustomJsonConverter))]
	public interface ITermFilter : IFilterBase, ICustomJson
	{
		PropertyPathMarker Field { get; set; }
		object Value { get; set; }
		double? Boost { get; set; }
	}

	[JsonConverter(typeof(CustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TermFilter : FilterBase, ITermFilter
	{
		PropertyPathMarker ITermFilter.Field { get; set; }
		object ITermFilter.Value { get; set; }
		double? ITermFilter.Boost { get; set; }

		bool IFilterBase.IsConditionless
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
	}
}
