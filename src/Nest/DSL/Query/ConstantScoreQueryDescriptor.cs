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
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }
	}

	public class ConstantScoreQuery : PlainQuery, ICustomScoreQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.CustomScore = this;
		}

		public bool IsConditionless { get { return false; } }
		public string Lang { get; set; }
		public string Script { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public IQueryContainer Query { get; set; }
	}

	public class ConstantScoreQueryDescriptor<T> : IConstantScoreQuery where T : class
	{
		IQueryContainer IConstantScoreQuery.Query { get; set; }

		IFilterContainer IConstantScoreQuery.Filter { get; set; }

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

		public ConstantScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			((IConstantScoreQuery)this).Filter = null;
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((IConstantScoreQuery)this).Query = q;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			((IConstantScoreQuery)this).Query = null;
			var filter = new FilterDescriptor<T>();
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
