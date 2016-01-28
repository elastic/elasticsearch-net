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
			Stack = new Stack<string>();
			base.Visit(expression);
		}

		protected override Expression VisitMember(MemberExpression expression)
		{
			this._members.Add(expression.Member);
			return base.VisitMember(expression);
		}
	}
}