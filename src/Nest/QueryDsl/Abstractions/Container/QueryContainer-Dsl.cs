// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	internal static class QueryContainerExtensions
	{
		public static bool IsConditionless(this QueryContainer q) => q == null || q.IsConditionless;
	}

	[JsonFormatter(typeof(QueryContainerFormatter))]
	public partial class QueryContainer : IQueryContainer, IDescriptor
	{
		public QueryContainer() { }

		public QueryContainer(QueryBase query) : this()
		{
			if (query == null) return;

			if (query.IsStrict && !query.IsWritable)
				throw new ArgumentException("Query is conditionless but strict is turned on");

			query.WrapInContainer(this);
		}

		[IgnoreDataMember]
		internal bool HoldsOnlyShouldMusts { get; set; }

		[IgnoreDataMember]
		internal bool IsConditionless => Self.IsConditionless;

		[IgnoreDataMember]
		internal bool IsStrict => Self.IsStrict;

		[IgnoreDataMember]
		internal bool IsVerbatim => Self.IsVerbatim;

		[IgnoreDataMember]
		internal bool IsWritable => Self.IsWritable;

		[IgnoreDataMember]
		bool IQueryContainer.IsConditionless => ContainedQuery?.Conditionless ?? true;

		[IgnoreDataMember]
		bool IQueryContainer.IsStrict { get; set; }

		[IgnoreDataMember]
		bool IQueryContainer.IsVerbatim { get; set; }

		[IgnoreDataMember]
		bool IQueryContainer.IsWritable => Self.IsVerbatim || !Self.IsConditionless;

		public void Accept(IQueryVisitor visitor)
		{
			if (visitor.Scope == VisitorScope.Unknown) visitor.Scope = VisitorScope.Query;
			new QueryWalker().Walk(this, visitor);
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

		private static bool IfEitherIsEmptyReturnTheOtherOrEmpty(QueryContainer leftContainer, QueryContainer rightContainer,
			out QueryContainer queryContainer
		)
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

		public static QueryContainer operator !(QueryContainer queryContainer) => queryContainer == null || !queryContainer.IsWritable
			? null
			: new QueryContainer(new BoolQuery { MustNot = new[] { queryContainer } });

		public static QueryContainer operator +(QueryContainer queryContainer) => queryContainer == null || !queryContainer.IsWritable
			? null
			: new QueryContainer(new BoolQuery { Filter = new[] { queryContainer } });

		public static bool operator false(QueryContainer a) => false;

		public static bool operator true(QueryContainer a) => false;

		// ReSharper disable once UnusedMember.Global
		internal bool ShouldSerialize(IJsonFormatterResolver formatterResolver) => IsWritable;
	}
}
