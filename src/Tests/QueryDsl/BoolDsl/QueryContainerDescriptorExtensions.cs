using System;
using Nest;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.BoolDsl
{
	public static class QueryContainerDescriptorExtensions
	{
		public static QueryContainer Query(this QueryContainerDescriptor<Project> q) => q.Term("x", "y");
		public static QueryContainer ConditionlessQuery(this QueryContainerDescriptor<Project> q) => q.Term("x", "");
		public static QueryContainer NullQuery(this QueryContainerDescriptor<Project> q) => q.Term("x", "");
		public static QueryContainer InvokeQuery<T>(
			this Func<QueryContainerDescriptor<T>, QueryContainer> f,
			QueryContainerDescriptor<T> container)
			where T : class
		{
			var c = f.Invoke(container);
			IQueryContainer ic = c;
			//if query is not conditionless or is verbatim: return a container that holds the query
			if (ic != null && (!ic.IsConditionless || ic.IsVerbatim))
				return c;

			//query is conditionless but the container is marked as strict, throw exception
			if (ic != null && ic.IsStrict)
				throw new ArgumentException("Query is conditionless but strict is turned on");

			//query is conditionless return an empty container that can later be rewritten
			return null;
		}
	}
}