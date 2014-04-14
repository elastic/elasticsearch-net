using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest.DSL.Query
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFilterScoreQuery
	{
		[JsonProperty(PropertyName = "filter")]
		BaseFilter _Filter { get; set; }

		[JsonProperty(PropertyName = "script")]
		string _Script { get; set; }

		[JsonProperty(PropertyName = "boost")]
		float? _Boost { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FilterScoreQueryDescriptor<T> : IQuery, IFilterScoreQuery where T : class
	{
		bool IQuery.IsConditionless { get { return ((IFilterScoreQuery)this)._Filter == null; } }

		BaseFilter IFilterScoreQuery._Filter { get; set; }

		string IFilterScoreQuery._Script { get; set; }

		float? IFilterScoreQuery._Boost { get; set; }

		public FilterScoreQueryDescriptor<T> Boost(float boost)
		{
			((IFilterScoreQuery)this)._Boost = boost;

			return this;
		}

		public FilterScoreQueryDescriptor<T> Script(string script)
		{
			((IFilterScoreQuery)this)._Script = script;

			return this;
		}

		public FilterScoreQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			((IFilterScoreQuery)this)._Filter = filterSelector(filter);

			return this;
		}
	}
}
