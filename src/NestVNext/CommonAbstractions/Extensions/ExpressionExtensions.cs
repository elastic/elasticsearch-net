// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
			return Expression.Lambda<Func<T, object>>(newBody!, expression.Parameters[0]);
		}
		public static Expression<Func<T, TValue>> AppendSuffix<T, TValue>(this Expression<Func<T, TValue>> expression, string suffix)
		{
			var newBody = new SuffixExpressionVisitor(suffix).Visit(expression.Body);
			return Expression.Lambda<Func<T, TValue>>(newBody!, expression.Parameters[0]);
		}

		internal static object ComparisonValueFromExpression(this Expression expression, out Type type, out bool cachable)
		{
			type = null;
			cachable = false;

			if (expression == null) return null;

			switch (expression)
			{
				case LambdaExpression lambdaExpression:
					type = lambdaExpression.Parameters.FirstOrDefault()?.Type;
					break;
				case MemberExpression memberExpression:
					type = memberExpression.Member.DeclaringType;
					break;
				case MethodCallExpression methodCallExpression when methodCallExpression.Method?.DeclaringType is {}:
					// special case F# method call expressions on FuncConvert
					// that are used to convert F# quotations representing lambda expressions, to expressions.
					// https://github.com/dotnet/fsharp/blob/7adaacf150dd79f072efe42d43168c9cd6edbced/src/fsharp/FSharp.Core/Linq.fs#L796
					//
					// For example:
					//
					// type Doc = { Message: string; State: string }
					// let field (f:Expr<'a -> 'b>) =
					//     Microsoft.FSharp.Linq.RuntimeHelpers.LeafExpressionConverter.QuotationToExpression f
					//     |> Nest.Field.op_Implicit
					//
					// let fieldExpression = field <@ fun (d: Doc) -> d.Message @>
					//
					if (methodCallExpression.Method.DeclaringType.FullName == "Microsoft.FSharp.Core.FuncConvert" &&
						methodCallExpression.Arguments.FirstOrDefault() is LambdaExpression lambda)
						type = lambda.Parameters.FirstOrDefault()?.Type;
					else
						throw new Exception($"Unsupported {nameof(MethodCallExpression)}: {expression}");
					break;
				case MethodCallExpression _:
						throw new Exception($"Unsupported {nameof(MethodCallExpression)}: {expression}");
				default:
					throw new Exception(
						$"Expected {nameof(LambdaExpression)}, {nameof(MemberExpression)} or "
						+ $"{nameof(MethodCallExpression)}, received: {expression.GetType().Name}");

			}

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
