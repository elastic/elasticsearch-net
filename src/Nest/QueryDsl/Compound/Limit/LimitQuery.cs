using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<LimitQuery>))]
	[Obsolete("Use TerminateAfter (terminate_after parameter) on the request instead")]
	public interface ILimitQuery : IQuery
	{
		[JsonProperty(PropertyName = "limit")]
		int? Limit { get; set; }
	}

	[Obsolete("Use TerminateAfter (terminate_after parameter) on the request instead")]
	public class LimitQuery : QueryBase, ILimitQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public int? Limit { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Limit = this;
		internal static bool IsConditionless(ILimitQuery q) => !q.Limit.HasValue;
	}

	[Obsolete("Use TerminateAfter (terminate_after parameter) on the request instead")]
	public class LimitQueryDescriptor<T>
		: QueryDescriptorBase<LimitQueryDescriptor<T>, ILimitQuery>
		, ILimitQuery where T : class
	{
		protected override bool Conditionless => LimitQuery.IsConditionless(this);
		int? ILimitQuery.Limit { get; set; }

		public LimitQueryDescriptor<T> Limit(int? limit) => Assign(a => a.Limit = limit);
	}
}
