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
	[JsonConverter(typeof(ReadAsTypeConverter<NotFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface INotFilter : IFilter
	{
		[JsonProperty("filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }
	}
	
	public class NotFilter : PlainFilter, INotFilter
	{
		protected override void WrapInContainer(IFilterContainer container)
		{
			container.Not = this;
		}

		public IFilterContainer Filter { get; set; }
	}

	public class NotFilterDescriptor : FilterBase, INotFilter
	{

		IFilterContainer INotFilter.Filter { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				var nf = ((INotFilter)this);
				return nf.Filter == null || nf.Filter.IsConditionless;
			} 
		}
	}
}
