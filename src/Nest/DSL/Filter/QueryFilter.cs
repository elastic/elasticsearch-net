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
	[JsonConverter(typeof(ReadAsTypeConverter<QueryFilter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IQueryFilter : IFilterBase
	{
		[JsonProperty("query")]
		IQueryDescriptor Query { get; set; }
	}

	public class QueryFilter : FilterBase, IQueryFilter
	{

		IQueryDescriptor IQueryFilter.Query { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				var af = ((IQueryFilter)this);
				return af.Query == null || af.Query.IsConditionless;
			} 
		}
	}
}
