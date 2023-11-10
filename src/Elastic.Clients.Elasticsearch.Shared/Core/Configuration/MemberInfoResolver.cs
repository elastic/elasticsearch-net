// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

/// <summary>
/// Resolves member infos in an expression, instance may NOT be shared.
/// </summary>
public sealed class MemberInfoResolver : ExpressionVisitor
{
	// ReSharper disable once VirtualMemberCallInConstructor
	public MemberInfoResolver(Expression expression) => Visit(expression);

	public IList<MemberInfo> Members { get; } = new List<MemberInfo>();

	protected override Expression VisitMember(MemberExpression expression)
	{
		Members.Add(expression.Member);
		return base.VisitMember(expression);
	}
}
