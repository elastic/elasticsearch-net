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
	[JsonConverter(typeof(ReadAsTypeConverter<NotFilter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface INotFilter : IFilterBase
	{
		[JsonProperty("filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<BaseFilterDescriptor>, CustomJsonConverter>))]
		IFilterDescriptor Filter { get; set; }
	}

	public class NotFilter : FilterBase, INotFilter
	{

		IFilterDescriptor INotFilter.Filter { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				var nf = ((INotFilter)this);
				return nf.Filter == null || nf.Filter.IsConditionless;
			} 
		}
	}
}
