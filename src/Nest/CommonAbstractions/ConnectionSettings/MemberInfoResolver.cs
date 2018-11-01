using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Nest
{
	/// <summary>
	///     Resolves member infos in an expression, instance may NOT be shared.
	/// </summary>
	public class MemberInfoResolver : ExpressionVisitor
	{
		public MemberInfoResolver(Expression expression) => Visit(expression);

		public IList<MemberInfo> Members { get; } = new List<MemberInfo>();

		protected override Expression VisitMember(MemberExpression expression)
		{
			Members.Add(expression.Member);
			return base.VisitMember(expression);
		}
	}
}
