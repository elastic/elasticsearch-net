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
	public interface ITermFilter : IFilterBase
	{
		PropertyPathMarker Field { get; set; }
		object Value { get; set; }
		double? Boost { get; set; }
	}

	[JsonConverter(typeof(CustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TermFilter : FilterBase, ICustomJson, ITermFilter
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
			return new Dictionary<object, object>
			{
				{
					tf.Field, new Dictionary<string, object>
					{
						{ "value", tf.Value },
						{ "boost", tf.Boost },
					}
				}
			};
		}
	}
}
