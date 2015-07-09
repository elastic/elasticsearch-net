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
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainerDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }
	}

	public class ConstantScoreQuery : QueryBase, IConstantScoreQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Lang { get; set; }
		public string Script { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public IQueryContainer Query { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.ConstantScore = this;
		internal static bool IsConditionless(IConstantScoreQuery q) => q.Query == null;
	}

	public class ConstantScoreQueryDescriptor<T> 
		: QueryDescriptorBase<ConstantScoreQueryDescriptor<T>, IConstantScoreQuery>
		, IConstantScoreQuery where T : class
	{
		private IConstantScoreQuery Self { get { return this; }}
		bool IQuery.Conditionless => ConstantScoreQuery.IsConditionless(this);
		IQueryContainer IConstantScoreQuery.Query { get; set; }

		public ConstantScoreQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			Self.Query = null;
			var query = new QueryContainerDescriptor<T>();
			var q = querySelector(query);

			Self.Query = q;
			return this;
		}
	}
}
