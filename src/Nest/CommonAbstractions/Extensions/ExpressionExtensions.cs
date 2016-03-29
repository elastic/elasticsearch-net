using System;
using System.Linq.Expressions;

namespace Nest
{
	public static class ExpressionExtensions
	{
		/// <summary>
		/// Appends <paramref name="suffix"/> to the path separating it with a dot.
		/// This is especially useful with multi fields.
		/// </summary>
		/// <param name="expression">the expression to which the suffix should be applied</param>
		/// <param name="suffix">the suffix</param>
		public static Expression<Func<T, object>> AppendSuffix<T>(this Expression<Func<T, object>> expression, string suffix)
		{
			var newBody = new SuffixExpressionVisitor(suffix).Visit(expression.Body);
			return Expression.Lambda<Func<T, object>>(newBody, expression.Parameters[0]);
		}

		/// <summary>
		/// Calls <see cref="SuffixExtensions.Suffix"/> on a member expression.
		/// </summary>
		private class SuffixExpressionVisitor : ExpressionVisitor
		{
			private readonly string suffix;

			public SuffixExpressionVisitor(string suffix)
			{
				this.suffix = suffix;
			}

			protected override Expression VisitMember(MemberExpression node)
			{
				return Expression.Call(
					typeof(SuffixExtensions),
					nameof(SuffixExtensions.Suffix),
					null,
					node,
					Expression.Constant(suffix));
			}

			protected override Expression VisitUnary(UnaryExpression node)
			{
				return node;
			}
		}
	}
}