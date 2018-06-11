using System;
using System.Linq;

namespace Nest
{
	internal static class QueryContainerExtensions
	{
		public static bool IsConditionless(this QueryContainer q) => q == null || q.IsConditionless;
	}

	public partial class QueryContainer : IQueryContainer, IDescriptor
	{
		bool IQueryContainer.IsConditionless => ContainedQuery?.Conditionless ?? true;
		internal bool IsConditionless => Self.IsConditionless;

		bool IQueryContainer.IsStrict { get; set; }
		internal bool IsStrict => Self.IsStrict;

		bool IQueryContainer.IsWritable => Self.IsVerbatim || !Self.IsConditionless;
		internal bool IsWritable => Self.IsWritable;

		bool IQueryContainer.IsVerbatim { get; set; }
		internal bool IsVerbatim => Self.IsVerbatim;

		internal bool HoldsOnlyShouldMusts { get; set; }

		public QueryContainer() { }

		public QueryContainer(QueryBase query) : this()
		{
			if (query == null) return;

			if (query.IsStrict && !query.IsWritable)
				throw new ArgumentException("Query is conditionless but strict is turned on");

			query.WrapInContainer(this);
		}

		public static QueryContainer operator &(QueryContainer leftContainer, QueryContainer rightContainer) =>
			And(leftContainer, rightContainer);

		internal static QueryContainer And(QueryContainer leftContainer, QueryContainer rightContainer) =>
			IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out var queryContainer)
				? queryContainer
				: leftContainer.CombineAsMust(rightContainer);

		public static QueryContainer operator |(QueryContainer leftContainer, QueryContainer rightContainer) =>
			Or(leftContainer, rightContainer);

		internal static QueryContainer Or(QueryContainer leftContainer, QueryContainer rightContainer) =>
			IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out var queryContainer)
				? queryContainer
				: leftContainer.CombineAsShould(rightContainer);

		private static bool IfEitherIsEmptyReturnTheOtherOrEmpty(QueryContainer leftContainer, QueryContainer rightContainer, out QueryContainer queryContainer)
		{
			queryContainer = null;
			if (leftContainer == null && rightContainer == null) return true;
			var leftWritable = leftContainer?.IsWritable ?? false;
			var rightWritable = rightContainer?.IsWritable ?? false;
			if (leftWritable && rightWritable) return false;
			if (!leftWritable && !rightWritable) return true;

			queryContainer = leftWritable ? leftContainer : rightContainer;
			return true;
		}

		public static QueryContainer operator !(QueryContainer queryContainer) => queryContainer == null || (!queryContainer.IsWritable)
			? null
			: new QueryContainer(new BoolQuery { MustNot = new[] { queryContainer } });

		public static QueryContainer operator +(QueryContainer queryContainer) => queryContainer == null || (!queryContainer.IsWritable)
			? null
			: new QueryContainer(new BoolQuery { Filter = new[] { queryContainer } });

		public static bool operator false(QueryContainer a) => false;

		public static bool operator true(QueryContainer a) => false;

		public void Accept(IQueryVisitor visitor)
		{
			if (visitor.Scope == VisitorScope.Unknown) visitor.Scope = VisitorScope.Query;
			new QueryWalker().Walk(this, visitor);
		}
	}
}
