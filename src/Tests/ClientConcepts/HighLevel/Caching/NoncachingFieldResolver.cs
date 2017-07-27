using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Elasticsearch.Net;
using Nest;

namespace Tests.ClientConcepts.HighLevel.Caching
{
	public class NoncachingFieldResolver
	{
		private readonly IConnectionSettingsValues _settings;

		public NoncachingFieldResolver(IConnectionSettingsValues settings)
		{
			this._settings = settings;
		}

		public string Resolve(Field field)
		{
			var name = ResolveFieldName(field);
			if (field.Boost.HasValue) name += $"^{field.Boost.Value.ToString(CultureInfo.InvariantCulture)}";
			return name;
		}

		internal static bool IsConditionless(Field field)
		{
			return field == null || (string.IsNullOrEmpty(field.Name) && field.Expression == null && field.Property == null);
		}

		internal static bool IsConditionless(PropertyName property)
		{
			return property == null || (string.IsNullOrEmpty(property.Name) && property.Expression == null && property.Property == null);
		}

		private string ResolveFieldName(Field field)
		{
			if (IsConditionless(field)) return null;
			if (!string.IsNullOrEmpty(field.Name)) return field.Name;
			if (field.Expression != null && !field.CacheableExpression)
			{
				return this.Resolve(field.Expression, field.Property);
			}

			var fieldName = this.Resolve(field.Expression, field.Property);
			return fieldName;
		}

		public string Resolve(PropertyName property)
		{
			if (IsConditionless(property)) return null;
			if (!string.IsNullOrEmpty(property.Name)) return property.Name;

			if (property.Expression != null && !property.CacheableExpression)
			{
				return this.Resolve(property.Expression, property.Property);
			}

			var propertyName = this.Resolve(property.Expression, property.Property, true);
			return propertyName;
		}

		private string Resolve(Expression expression, MemberInfo member, bool toLastToken = false)
		{
			var visitor = new FieldExpressionVisitor(_settings);
			var name = expression != null
				? visitor.Resolve(expression, toLastToken)
				: member != null
					? visitor.Resolve(member)
					: null;

			if (name == null)
				throw new ArgumentException("Name resolved to null for the given Expression or MemberInfo.");

			return name;
		}

		internal class FieldExpressionVisitor : ExpressionVisitor
		{
			private readonly Stack<string> _stack = new Stack<string>();

			private readonly IConnectionSettingsValues _settings;

			public FieldExpressionVisitor(IConnectionSettingsValues settings)
			{
				_settings = settings;
			}

			public string Resolve(Expression expression, bool toLastToken = false)
			{
				Visit(expression);
				if (toLastToken) return Enumerable.Last<string>(_stack);
				return Enumerable.Aggregate<string, StringBuilder>(_stack, new StringBuilder(),
						(sb, name) =>
							(sb.Length > 0 ? sb.Append(".") : sb).Append(name))
					.ToString();
			}

			public string Resolve(MemberInfo info)
			{
				if (info == null)
					return null;

				var name = info.Name;

				if (this._settings.PropertyMappings.TryGetValue(info, out IPropertyMapping propertyMapping))
					return propertyMapping.Name;

				var att = ElasticsearchPropertyAttributeBase.From(info);
				if (att != null && !string.IsNullOrEmpty(att.Name))
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
						|| (t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>));

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
}