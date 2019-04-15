using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest {
	internal class HasVariableExpressionVisitor : ExpressionVisitor
	{
		private bool _found;

		public HasVariableExpressionVisitor(Expression e) => Visit(e);

		public bool Found
		{
			get => _found;
			// This is only set to true once to prevent clobbering from subsequent node visits
			private set
			{
				if (!_found) _found = value;
			}
		}

		public override Expression Visit(Expression node)
		{
			if (!Found)
				return base.Visit(node);

			return node;
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (node.Method.Name == nameof(SuffixExtensions.Suffix) && node.Arguments.Any())
			{
				var lastArg = node.Arguments.Last();
				Found = !(lastArg is ConstantExpression);
			}
			else if (node.Method.Name == "get_Item" && node.Arguments.Any())
			{
				var t = node.Object.Type;
				var isDict =
					typeof(IDictionary).IsAssignableFrom(t)
					|| typeof(IDictionary<,>).IsAssignableFrom(t)
					|| t.IsGeneric() && t.GetGenericTypeDefinition() == typeof(IDictionary<,>);

				if (!isDict)
					return base.VisitMethodCall(node);

				var lastArg = node.Arguments.Last();
				Found = !(lastArg is ConstantExpression);
			}
			return base.VisitMethodCall(node);
		}
	}
}