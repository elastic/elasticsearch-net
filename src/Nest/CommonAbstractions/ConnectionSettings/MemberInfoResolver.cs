using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Nest
{
	/// <summary>
	/// Resolves member infos in an expression, instance may NOT be shared.
	/// </summary>
	public class MemberInfoResolver : FieldResolver
	{
		private readonly IList<MemberInfo> _members = new List<MemberInfo>();
		public IList<MemberInfo> Members { get { return _members; } }

		public MemberInfoResolver(IConnectionSettingsValues settings, Expression expression) : base(settings)
		{
			var stack = new Stack<string>();
			var properties = new Stack<ElasticsearchPropertyAttribute>();
			base.Visit(expression, stack, properties);
		}

		protected override Expression VisitMemberAccess(MemberExpression expression, Stack<string> stack, Stack<ElasticsearchPropertyAttribute> properties)
		{
			this._members.Add(expression.Member);
			return base.VisitMemberAccess(expression, stack, properties);
		}
	}
}