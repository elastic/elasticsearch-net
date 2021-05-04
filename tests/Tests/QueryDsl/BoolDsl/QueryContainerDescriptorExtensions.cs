// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Domain;

namespace Tests.QueryDsl.BoolDsl
{
	public static class QueryContainerDescriptorExtensions
	{
		public static QueryContainer Query(this QueryContainerDescriptor<Project> q) => q.Term("x", "y");

		public static QueryContainer ConditionlessQuery(this QueryContainerDescriptor<Project> q) => q.Term("x", "");

		public static QueryContainer NullQuery(this QueryContainerDescriptor<Project> q) => q.Term("x", "");
	}
}
