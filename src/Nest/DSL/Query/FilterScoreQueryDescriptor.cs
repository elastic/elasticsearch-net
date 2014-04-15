using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest.DSL.Query
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFilterScoreQuery : IQuery
	{
		[JsonProperty(PropertyName = "filter")]
		BaseFilter Filter { get; set; }

		[JsonProperty(PropertyName = "script")]
		string Script { get; set; }

		[JsonProperty(PropertyName = "boost")]
		float? Boost { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FilterScoreQueryDescriptor<T> : IFilterScoreQuery where T : class
	{
		bool IQuery.IsConditionless { get { return ((IFilterScoreQuery)this).Filter == null; } }

		BaseFilter IFilterScoreQuery.Filter { get; set; }

		string IFilterScoreQuery.Script { get; set; }

		float? IFilterScoreQuery.Boost { get; set; }

		public FilterScoreQueryDescriptor<T> Boost(float boost)
		{
			((IFilterScoreQuery)this).Boost = boost;

			return this;
		}

		public FilterScoreQueryDescriptor<T> Script(string script)
		{
			((IFilterScoreQuery)this).Script = script;

			return this;
		}

		public FilterScoreQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			((IFilterScoreQuery)this).Filter = filterSelector(filter);

			return this;
		}
	}
}
