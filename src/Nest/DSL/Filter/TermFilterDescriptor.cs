using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Elasticsearch.Net;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Shared.Extensions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermFilter : IFieldNameFilter
	{
		[JsonProperty("value")]
		object Value { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }
	}
	
	public class TermFilter : PlainFilter, ITermFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Term = this;
		}

		public PropertyPathMarker Field { get; set; }
		public object Value { get; set; }
		public double? Boost { get; set; }
	}

	public class TermFilterDescriptor : FilterBase, ITermFilter
	{
		PropertyPathMarker IFieldNameFilter.Field { get; set; }
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
		
	}
}
