using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	public static class ExpressionExtensions
	{
		/// <summary>
		/// Appends <paramref name="suffix" /> to the path separating it with a dot.
		/// This is especially useful with multi fields.
		/// </summary>
		/// <param name="expression">the expression to which the suffix should be applied</param>
		/// <param name="suffix">the suffix</param>
		public static Expression<Func<T, object>> AppendSuffix<T>(this Expression<Func<T, object>> expression, string suffix)
		{
			var newBody = new SuffixExpressionVisitor(suffix).Visit(expression.Body);
			return Expression.Lambda<Func<T, object>>(newBody, expression.Parameters[0]);
		}
		public static Expression<Func<T, TValue>> AppendSuffix<T, TValue>(this Expression<Func<T, TValue>> expression, string suffix)
		{
			var newBody = new SuffixExpressionVisitor(suffix).Visit(expression.Body);
			return Expression.Lambda<Func<T, TValue>>(newBody, expression.Parameters[0]);
		}

		internal static object ComparisonValueFromExpression(this Expression expression, out Type type, out bool cachable)
		{
			type = null;
			cachable = false;

			if (expression == null) return null;

			if (!(expression is LambdaExpression lambda)) throw new Exception($"Not a lambda expression: {expression}");

			type = lambda.Parameters.FirstOrDefault()?.Type;
			var visitor = new ToStringExpressionVisitor();
			var toString = visitor.Resolve(expression);
			cachable = visitor.Cachable;
			return toString;
		}

		/// <summary>
		/// Calls <see cref="SuffixExtensions.Suffix" /> on a member expression.
		/// </summary>
		private class SuffixExpressionVisitor : ExpressionVisitor
		{
			private readonly string _suffix;

			public SuffixExpressionVisitor(string suffix) => _suffix = suffix;

			public override Expression Visit(Expression node) => Expression.Call(
				typeof(SuffixExtensions),
				nameof(SuffixExtensions.Suffix),
				null,
				node,
				Expression.Constant(_suffix));

			protected override Expression VisitUnary(UnaryExpression node) => node;
		}
	}
}
