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

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }
	}

	public class ConstantScoreQuery : PlainQuery, IConstantScoreQuery
	{
		public string Name { get; set; }
		bool IQuery.Conditionless => IsConditionless(this);
		public string Lang { get; set; }
		public string Script { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public IQueryContainer Query { get; set; }
		public double? Boost { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.ConstantScore = this;
		internal static bool IsConditionless(IConstantScoreQuery q) => q.Query == null;
	}

	public class ConstantScoreQueryDescriptor<T> : IConstantScoreQuery where T : class
	{
		private IConstantScoreQuery Self { get { return this; }}
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => ConstantScoreQuery.IsConditionless(this);
		IQueryContainer IConstantScoreQuery.Query { get; set; }
		double? IConstantScoreQuery.Boost { get; set; }

		public ConstantScoreQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			Self.Query = null;
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			Self.Query = q;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}
	}
}
