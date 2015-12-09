using System;
using System.Collections.Generic;
using System.Linq;
using Nest.DSL.Visitor;
using Newtonsoft.Json;
using Nest.QueryDsl.Visitor;

namespace Nest
{
	internal static class QueryContainerExtensions
	{
		public static bool IsConditionless(this QueryContainer q) => q == null || q.IsConditionless;
	}

	public partial class QueryContainer : IQueryContainer, IDescriptor
	{
		bool IQueryContainer.IsConditionless => (ContainedQuery?.Conditionless).GetValueOrDefault(true);
		internal bool IsConditionless => Self.IsConditionless;

		bool IQueryContainer.IsStrict { get; set; }
		internal bool IsStrict => Self.IsStrict;

		bool IQueryContainer.IsVerbatim { get; set; }
		internal bool IsVerbatim => Self.IsVerbatim;

		public QueryContainer()
		{
		}
	
		public QueryContainer(QueryBase query) : this()
		{
			query?.WrapInContainer(this);
		}
	
		public static QueryContainer operator &(QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer queryContainer;
			if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out queryContainer))
				return queryContainer;

			return leftContainer.MergeMustQueries(rightContainer);
		}
		
		public static QueryContainer operator |(QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer queryContainer;
			if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out queryContainer)) return queryContainer;

			return leftContainer.MergeShouldQueries(rightContainer);
		}

		private static bool IfEitherIsEmptyReturnTheOtherOrEmpty(QueryContainer leftContainer, QueryContainer rightContainer, out QueryContainer queryContainer)
		{
			var combined = new[] {leftContainer, rightContainer};
			var any = combined.Any(bf => bf == null || bf.IsConditionless);
			queryContainer = any ? combined.FirstOrDefault(bf => bf != null && !bf.IsConditionless) : null;
			return any;
		}

		public static QueryContainer operator !(QueryContainer queryContainer) => queryContainer == null || queryContainer.IsConditionless
			? null
			: new QueryContainer(new BoolQuery {MustNot = new[] {queryContainer}});

		public static QueryContainer operator +(QueryContainer queryContainer) => queryContainer == null || queryContainer.IsConditionless
			? null
			: new QueryContainer(new BoolQuery {Filter = new[] {queryContainer}});

		public static bool operator false(QueryContainer a)
		{
			return false;
		}

		public static bool operator true(QueryContainer a)
		{
			return false;
		}

		public void Accept(IQueryVisitor visitor)
		{
			var walker = new QueryWalker();
			if (visitor.Scope == VisitorScope.Unknown) visitor.Scope = VisitorScope.Query;
			walker.Walk(this, visitor);
		}

		//TODO remove rely on a custom serializer
		public object GetCustomJson()
		{	
			var f = ((IQueryContainer)this);
			if (f.RawQuery.IsNullOrEmpty()) return f; 
			return new RawJson(f.RawQuery);
		}
	}

}
