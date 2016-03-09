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
	}
}
