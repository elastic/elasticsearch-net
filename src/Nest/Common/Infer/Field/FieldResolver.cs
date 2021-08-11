using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Nest.Types.Core;

namespace Nest
{
	public class FieldResolver
	{
		private readonly IElasticsearchClientSettings _settings;
		protected readonly ConcurrentDictionary<Field, string> Fields = new();
		protected readonly ConcurrentDictionary<PropertyName, string> Properties = new();

		public FieldResolver(IElasticsearchClientSettings settings)
		{
			settings.ThrowIfNull(nameof(settings));
			_settings = settings;
		}

		public string Resolve(Field field)
		{
			var name = ResolveFieldName(field);
			if (field.Boost.HasValue)
				name += $"^{field.Boost.Value.ToString(CultureInfo.InvariantCulture)}";
			return name;
		}

		private string ResolveFieldName(Field field)
		{
			if (field.IsConditionless())
				return null;
			if (!field.Name.IsNullOrEmpty())
				return field.Name;
			if (field.Expression != null && !field.CachableExpression)
				return Resolve(field.Expression, field.Property);

			if (Fields.TryGetValue(field, out var fieldName))
				return fieldName;

			fieldName = Resolve(field.Expression, field.Property);
			Fields.TryAdd(field, fieldName);
			return fieldName;
		}

		public string Resolve(PropertyName property)
		{
			if (property.IsConditionless())
				return null;
			if (!property.Name.IsNullOrEmpty())
				return property.Name;

			if (property.Expression != null && !property.CacheableExpression)
				return Resolve(property.Expression, property.Property);

			if (Properties.TryGetValue(property, out var propertyName))
				return propertyName;

			propertyName = Resolve(property.Expression, property.Property, true);
			Properties.TryAdd(property, propertyName);
			return propertyName;
		}

		private string Resolve(Expression? expression, MemberInfo? member, bool toLastToken = false)
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
	}

	internal class FieldExpressionVisitor : ExpressionVisitor
	{
		private readonly IElasticsearchClientSettings _settings;
		private readonly Stack<string> _stack = new();

		public FieldExpressionVisitor(IElasticsearchClientSettings settings) => _settings = settings;

		public string Resolve(Expression expression, bool toLastToken = false)
		{
			Visit(expression);
			if (toLastToken)
				return _stack.Last();

			var builder = new StringBuilder(_stack.Sum(s => s.Length) + (_stack.Count - 1));

			return _stack
				.Aggregate(
					builder,
					(sb, name) =>
						(sb.Length > 0 ? sb.Append(".") : sb).Append(name))
				.ToString();
		}

		public string? Resolve(MemberInfo? info)
		{
			if (info == null)
				return null;

			var name = info.Name;

			// TODO - Un-skip this and re-implement properly
			//if (_settings.PropertyMappings.TryGetValue(info, out var propertyMapping))
			//	return propertyMapping.Name;

			//var att = ElasticsearchPropertyAttributeBase.From(info);
			//if (att != null && !att.Name.IsNullOrEmpty())
			//	return att.Name;

			//return _settings.PropertyMappingProvider?.CreatePropertyMapping(info)?.Name ?? _settings.DefaultFieldNameInferrer(name);

			return _settings.DefaultFieldNameInferrer(name);
		}

		protected override Expression VisitMember(MemberExpression expression)
		{
			var name = Resolve(expression.Member);
			_stack.Push(name);
			return base.VisitMember(expression);
		}

		protected override Expression VisitMethodCall(MethodCallExpression methodCall)
		{
			if (methodCall.Method.Name == nameof(SuffixExtensions.Suffix) && methodCall.Arguments.Any())
			{
				VisitConstantOrVariable(methodCall, _stack);
				var callingMember = new ReadOnlyCollection<Expression>(
					new List<Expression> {{methodCall.Arguments.First()}}
				);
				Visit(callingMember);
				return methodCall;
			}
			else if (methodCall.Method.Name == "get_Item" && methodCall.Arguments.Any())
			{
				var t = methodCall.Object.Type;
				var isDict =
					typeof(IDictionary).IsAssignableFrom(t)
					|| typeof(IDictionary<,>).IsAssignableFrom(t)
					|| t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>);

				if (!isDict)
					return base.VisitMethodCall(methodCall);

				VisitConstantOrVariable(methodCall, _stack);
				Visit(methodCall.Object);
				return methodCall;
			}
			else if (IsLinqOperator(methodCall.Method))
			{
				for (var i = 1; i < methodCall.Arguments.Count; i++)
					Visit(methodCall.Arguments[i]);
				Visit(methodCall.Arguments[0]);
				return methodCall;
			}

			return base.VisitMethodCall(methodCall);
		}

		private static void VisitConstantOrVariable(MethodCallExpression methodCall, Stack<string> stack)
		{
			var lastArg = methodCall.Arguments.Last();
			var value = lastArg is ConstantExpression constantExpression
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
