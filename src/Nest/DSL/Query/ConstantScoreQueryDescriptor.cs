using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IConstantScoreQuery
	{
		[JsonProperty(PropertyName = "query")]
		BaseQuery _Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		BaseFilter _Filter { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ConstantScoreQueryDescriptor<T> : IQuery, IConstantScoreQuery where T : class
	{
		BaseQuery IConstantScoreQuery._Query { get; set; }

		BaseFilter IConstantScoreQuery._Filter { get; set; }

		double? IConstantScoreQuery._Boost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (((IConstantScoreQuery)this)._Query == null && ((IConstantScoreQuery)this)._Filter == null)
					return true;
				if (((IConstantScoreQuery)this)._Filter == null && ((IConstantScoreQuery)this)._Query != null)
					return ((IConstantScoreQuery)this)._Query.IsConditionless;
				if (((IConstantScoreQuery)this)._Filter != null && ((IConstantScoreQuery)this)._Query == null)
					return ((IConstantScoreQuery)this)._Filter.IsConditionless;
				return false;
			}
		}

		public ConstantScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			((IConstantScoreQuery)this)._Filter = null;
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((IConstantScoreQuery)this)._Query = q;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			((IConstantScoreQuery)this)._Query = null;
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			((IConstantScoreQuery)this)._Filter = f;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Boost(double boost)
		{
			((IConstantScoreQuery)this)._Boost = boost;
			return this;
		}
	}
}
