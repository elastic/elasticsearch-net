using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<ConstantScoreQueryDescriptor<object>>))]
	public interface IConstantScoreQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		IQueryDescriptor Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		IFilterDescriptor Filter { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ConstantScoreQueryDescriptor<T> : IConstantScoreQuery where T : class
	{
		IQueryDescriptor IConstantScoreQuery.Query { get; set; }

		IFilterDescriptor IConstantScoreQuery.Filter { get; set; }

		double? IConstantScoreQuery.Boost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (((IConstantScoreQuery)this).Query == null && ((IConstantScoreQuery)this).Filter == null)
					return true;
				if (((IConstantScoreQuery)this).Filter == null && ((IConstantScoreQuery)this).Query != null)
					return ((IConstantScoreQuery)this).Query.IsConditionless;
				if (((IConstantScoreQuery)this).Filter != null && ((IConstantScoreQuery)this).Query == null)
					return ((IConstantScoreQuery)this).Filter.IsConditionless;
				return false;
			}
		}

		public ConstantScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			((IConstantScoreQuery)this).Filter = null;
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((IConstantScoreQuery)this).Query = q;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Filter(Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			((IConstantScoreQuery)this).Query = null;
			var filter = new FilterDescriptorDescriptor<T>();
			var f = filterSelector(filter);

			((IConstantScoreQuery)this).Filter = f;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Boost(double boost)
		{
			((IConstantScoreQuery)this).Boost = boost;
			return this;
		}
	}
}
