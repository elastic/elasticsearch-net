using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SpanOrQueryDescriptor<object>>))]
	public interface ISpanOrQuery : ISpanSubQuery
	{
		[JsonProperty(PropertyName = "clauses")]
		IEnumerable<ISpanQuery> Clauses { get; set; }
	}

	public class SpanOrQuery : QueryBase, ISpanOrQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IEnumerable<ISpanQuery> Clauses { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.SpanOr = this;
		internal static bool IsConditionless(ISpanOrQuery q) => !q.Clauses.HasAny() || q.Clauses.Cast<IQuery>().All(qq => qq.Conditionless);
	}

	public class SpanOrQueryDescriptor<T> 
		: QueryDescriptorBase<SpanOrQueryDescriptor<T>, ISpanOrQuery>
		, ISpanOrQuery where T : class
	{
		private ISpanOrQuery Self => this;
		bool IQuery.Conditionless => SpanOrQuery.IsConditionless(this);
		IEnumerable<ISpanQuery> ISpanOrQuery.Clauses { get; set; }

		public SpanOrQueryDescriptor<T> Clauses(params Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>>[] selectors) => Assign(a =>
		{
			var clauses = (
				from selector in selectors
				select selector(new SpanQueryDescriptor<T>()) into q
				where !(q as IQuery).Conditionless
				select q
			).ToList();
			a.Clauses = clauses.HasAny() ? clauses : null;
		});
	}
}
