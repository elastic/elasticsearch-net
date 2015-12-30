using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nest
{
	//Shout out to http://tomlev2.wordpress.com/2010/10/03/entity-framework-using-include-with-lambda-expressions/
	//replaces my sloppy 300+ lines (though working!) first attempt, thanks Thomas Levesque.	
	public class FieldResolver : ExpressionVisitor
	{
		private readonly IConnectionSettingsValues _settings;

		public FieldResolver(IConnectionSettingsValues settings)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");
			_settings = settings;
		}

		public string Resolve(MemberInfo info)
		{
			if (info == null)
				return null;

			var name = info.Name;

			var att = ElasticsearchPropertyAttribute.From(info);
			if (att != null && !att.Name.IsNullOrEmpty())
				return att.Name;

			return _settings.Serializer?.CreatePropertyName(info) ?? _settings.DefaultFieldNameInferrer(name);
		}

		public string Resolve(Expression expression)
		{
			var stack = new Stack<string>();
			var properties = new Stack<ElasticsearchPropertyAttribute>();
			Visit(expression, stack, properties);
			return stack
				.Aggregate(
					new StringBuilder(),
					(sb, name) =>
					(sb.Length > 0 ? sb.Append(".") : sb).Append(name))
				.ToString();
		}

		protected override Expression VisitMemberAccess(MemberExpression expression, Stack<string> stack, Stack<ElasticsearchPropertyAttribute> properties)
		{
			if (stack != null)
			{
				var resolvedName = this.Resolve(expression.Member);
				stack.Push(resolvedName);
			}
			return base.VisitMemberAccess(expression, stack, properties);
		}

		protected override Expression VisitMethodCall(MethodCallExpression m, Stack<string> stack, Stack<ElasticsearchPropertyAttribute> properties)
		{
			if (m.Method.Name == "Suffix" && m.Arguments.Any())
			{
				VisitConstantOrVariable(m, stack);
				var callingMember = new ReadOnlyCollection<Expression>(
					new List<Expression> { { m.Arguments.First() } }
				);
				base.VisitExpressionList(callingMember, stack, properties);
				return m;
			}
			else if (m.Method.Name == "get_Item" && m.Arguments.Any())
			{
				var t = m.Object.Type;
				var isDict =
					typeof(IDictionary).IsAssignableFrom(t)
					|| typeof(IDictionary<,>).IsAssignableFrom(t)
					|| (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>));

				if (!isDict)
				{
					return base.VisitMethodCall(m, stack, properties);
				}
				VisitConstantOrVariable(m, stack);
				Visit(m.Object, stack, properties);
				return m;
			}
			else if (IsLinqOperator(m.Method))
			{
				for (int i = 1; i < m.Arguments.Count; i++)
				{
					Visit(m.Arguments[i], stack, properties);
				}
				Visit(m.Arguments[0], stack, properties);
				return m;
			}
			return base.VisitMethodCall(m, stack, properties);
		}

		private static void VisitConstantOrVariable(MethodCallExpression m, Stack<string> stack)
		{
			var lastArg = m.Arguments.Last();
			var constantExpression = lastArg as ConstantExpression;
			var value = constantExpression != null
				? constantExpression.Value.ToString()
				: Expression.Lambda(lastArg).Compile().DynamicInvoke().ToString();
			stack.Push(value);
		}

		private static bool IsLinqOperator(MethodInfo method)
		{
			if (method.DeclaringType != typeof(Queryable) && method.DeclaringType != typeof(Enumerable))
				return false;
			return Attribute.GetCustomAttribute(method, typeof(ExtensionAttribute)) != null;
		}
	}
}
