using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<LimitQuery>))]
	public interface ILimitQuery : IQuery
	{
		[JsonProperty(PropertyName = "limit")]
		int? Limit { get; set; }
	}

	public class LimitQuery : QueryBase, ILimitQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public int? Limit { get; set; }

		internal override void WrapInContainer(IQueryContainer c) => c.Limit = this;
		internal static bool IsConditionless(ILimitQuery q) => !q.Limit.HasValue;
	}

	public class LimitQueryDescriptor<T> 
		: QueryDescriptorBase<LimitQueryDescriptor<T>, ILimitQuery>
		, ILimitQuery where T : class
	{
		protected override bool Conditionless => LimitQuery.IsConditionless(this);
		int? ILimitQuery.Limit { get; set; }
		
		public LimitQueryDescriptor<T> Limit(int? limit) => Assign(a => a.Limit = limit);
	}
}
