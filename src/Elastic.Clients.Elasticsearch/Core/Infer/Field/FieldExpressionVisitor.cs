// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections;

namespace Elastic.Clients.Elasticsearch;

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

	public string Resolve(MemberInfo info)
	{
		if (info == null)
			return null;

		var name = info.Name;

		if (_settings.PropertyMappings.TryGetValue(info, out var propertyMapping))
			return propertyMapping.Name;

		// If an IPropertyMappingProvider is available, first attempt to create a mapping and access the name.
		// If no IPropertyMappingProvider is available or a null PropertyMapping is returned, fallback to the configured DefaultFieldNameInferrer function.
		return _settings.PropertyMappingProvider?.CreatePropertyMapping(info).Name ?? _settings.DefaultFieldNameInferrer(name);
	}

	protected override Expression VisitMember(MemberExpression expression)
	{
		if (_stack == null)
			return base.VisitMember(expression);

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
				new List<Expression> { { methodCall.Arguments.First() } }
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
