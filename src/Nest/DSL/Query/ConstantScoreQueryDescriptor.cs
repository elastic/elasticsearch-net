using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

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

	public class ConstantScoreQuery : PlainQuery, IConstantScoreQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.ConstantScore = this;
		}

		public string Name { get; set; }
		//TODO 2.0 change to explicit IQuery implementation
		public bool IsConditionless { get { return false; } }
		public string Lang { get; set; }
		public string Script { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public IQueryContainer Query { get; set; }
		public IFilterContainer Filter { get; set; }
		public double? Boost { get; set; }
	}

	public class ConstantScoreQueryDescriptor<T> : IConstantScoreQuery where T : class
	{
		private IConstantScoreQuery Self { get { return this; }}

		IQueryContainer IConstantScoreQuery.Query { get; set; }

		IFilterContainer IConstantScoreQuery.Filter { get; set; }

		double? IConstantScoreQuery.Boost { get; set; }
		
		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (Self.Query == null && Self.Filter == null)
					return true;
				if (Self.Filter == null && Self.Query != null)
					return Self.Query.IsConditionless;
				if (Self.Filter != null && Self.Query == null)
					return Self.Filter.IsConditionless;
				return false;
			}
		}

		public ConstantScoreQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			Self.Filter = null;
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			Self.Query = q;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			Self.Query = null;
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			Self.Filter = f;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}
	}
}
