using Elasticsearch.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
    internal class FieldExpressionVisitor : ExpressionVisitor
    {
		private Stack<string> _stack = new Stack<string>();

		private IConnectionSettingsValues _settings;

		public FieldExpressionVisitor(IConnectionSettingsValues settings)
		{
			_settings = settings;
		}

		public string Resolve(Expression expression, bool toLastToken = false)
		{
			Visit(expression);
			if (toLastToken) return _stack.Last();
			return _stack
				.Aggregate(
					new StringBuilder(),
					(sb, name) =>
					(sb.Length > 0 ? sb.Append(".") : sb).Append(name))
				.ToString();
		}

		public string Resolve(MemberInfo info)
		{
			if (info == null)
				return null;

			var name = info.Name;

			IPropertyMapping propertyMapping = null;
			if (this._settings.PropertyMappings.TryGetValue(info, out propertyMapping))
				return propertyMapping.Name;

			var att = ElasticsearchPropertyAttributeBase.From(info);
			if (att != null && !att.Name.IsNullOrEmpty())
				return att.Name;

			return _settings.Serializer?.CreatePropertyMapping(info)?.Name ?? _settings.DefaultFieldNameInferrer(name);
		}

		protected override Expression VisitMember(MemberExpression expression)
		{
			if (_stack == null) return base.VisitMember(expression);
			var name = this.Resolve(expression.Member);
			_stack.Push(name);
			return base.VisitMember(expression);
		}

		protected override Expression VisitMethodCall(MethodCallExpression methodCall)
		{
			if (methodCall.Method.Name == "Suffix" && methodCall.Arguments.Any())
			{
				VisitConstantOrVariable(methodCall, _stack);
				var callingMember = new ReadOnlyCollection<Expression>(
					new List<Expression> { { methodCall.Arguments.First() } }
				);
				base.Visit(callingMember);
				return methodCall;
			}
			else if (methodCall.Method.Name == "get_Item" && methodCall.Arguments.Any())
			{
				var t = methodCall.Object.Type;
				var isDict =
					typeof(IDictionary).IsAssignableFrom(t)
					|| typeof(IDictionary<,>).IsAssignableFrom(t)
					|| (t.IsGeneric() && t.GetGenericTypeDefinition() == typeof(IDictionary<,>));

				if (!isDict)
				{
					return base.VisitMethodCall(methodCall);
				}
				VisitConstantOrVariable(methodCall, _stack);
				Visit(methodCall.Object);
				return methodCall;
			}
			else if (IsLinqOperator(methodCall.Method))
			{
				for (int i = 1; i < methodCall.Arguments.Count; i++)
				{
					Visit(methodCall.Arguments[i]);
				}
				Visit(methodCall.Arguments[0]);
				return methodCall;
			}
			return base.VisitMethodCall(methodCall);
		}

		private static void VisitConstantOrVariable(MethodCallExpression methodCall, Stack<string> stack)
		{
			var lastArg = methodCall.Arguments.Last();
			var constantExpression = lastArg as ConstantExpression;
			var value = constantExpression != null
				? constantExpression.Value.ToString()
				: Expression.Lambda(lastArg).Compile().DynamicInvoke().ToString();
			stack.Push(value);
		}

		private static bool IsLinqOperator(MethodInfo methodInfo)
		{
			if (methodInfo.DeclaringType != typeof(Queryable) && methodInfo.DeclaringType != typeof(Enumerable))
				return false;

			return methodInfo.GetCustomAttribute<ExtensionAttribute>() != null;
		}
    }
}
