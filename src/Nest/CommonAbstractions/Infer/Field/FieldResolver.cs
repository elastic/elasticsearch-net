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

namespace Nest
{
	public class FieldResolver : ExpressionVisitor
	{
		private readonly IConnectionSettingsValues _settings;

		private readonly ConcurrentDictionary<Field, string> Fields = new ConcurrentDictionary<Field, string>();
		private readonly ConcurrentDictionary<PropertyName, string> Properties = new ConcurrentDictionary<PropertyName, string>();

		private object _lock = new object();
		private Stack<string> _stack;
		protected Stack<string> Stack
		{
			get { lock(_lock) { return _stack; } }
			set { lock(_lock) { _stack = value; } }
		}

		public FieldResolver(IConnectionSettingsValues settings)
		{
			settings.ThrowIfNull(nameof(settings));
			this._settings = settings;
		}

		public string Resolve(Field field)
		{
			if (field.IsConditionless()) return null;
			if (!field.Name.IsNullOrEmpty()) return field.Name;
			string f;
			if (this.Fields.TryGetValue(field, out f))
				return f;
			f = this.Resolve(field.Expression, field.Property);
			this.Fields.TryAdd(field, f);
			return f;
		}

		public string Resolve(PropertyName property)
		{
			if (property.IsConditionless()) return null;
			if (!property.Name.IsNullOrEmpty())
			{
				if (property.Name.Contains("."))
					throw new ArgumentException("Property names cannot contain dots.");
				return property.Name;
			}
			string f;
			if (this.Properties.TryGetValue(property, out f))
				return f;
			f = this.ResolveToLastToken(property.Expression, property.Property);
			this.Properties.TryAdd(property, f);
			return f;
		}

		private string Resolve(Expression expression, MemberInfo member)
		{
			var name = expression != null
				? this.Resolve(expression)
				: member != null
					? this.Resolve(member)
					: null;

			if (name == null)
				throw new ArgumentException("Could not resolve a name from the given Expression or MemberInfo.");

			return name;
		}

		private string ResolveToLastToken(Expression expression, MemberInfo member)
		{
			var name = expression != null
				? this.ResolveToLastToken(expression)
				: member != null
					? this.Resolve(member)
					: null;

			if (name == null)
				throw new ArgumentException("Could not resolve a name from the given Expression or MemberInfo.");

			return name;
		}

		private string Resolve(MemberInfo info)
		{
			if (info == null)
				return null;

			var name = info.Name;

			IPropertyMapping propertyMapping = null;
			if (this._settings.PropertyMappings.TryGetValue(info, out propertyMapping))
				return propertyMapping.Name;

			var att = ElasticsearchPropertyAttribute.From(info);
			if (att != null && !att.Name.IsNullOrEmpty())
				return att.Name;

			return _settings.Serializer?.CreatePropertyName(info) ?? _settings.DefaultFieldNameInferrer(name);
		}

		private string Resolve(Expression expression)
		{
			Stack = new Stack<string>();
			Visit(expression);
			return Stack
				.Aggregate(
					new StringBuilder(),
					(sb, name) =>
					(sb.Length > 0 ? sb.Append(".") : sb).Append(name))
				.ToString();
		}

		private string ResolveToLastToken(Expression expression)
		{
			Stack = new Stack<string>();
			Visit(expression);
			return Stack.Last();
		}

		protected override Expression VisitMember(MemberExpression expression)
		{
			if (Stack == null) return base.VisitMember(expression);
			var resolvedName = this.Resolve(expression.Member);
			Stack.Push(resolvedName);
			return base.VisitMember(expression);
		}

		protected override Expression VisitMethodCall(MethodCallExpression methodCall)
		{
			if (methodCall.Method.Name == "Suffix" && methodCall.Arguments.Any())
			{
				VisitConstantOrVariable(methodCall, Stack);
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
				VisitConstantOrVariable(methodCall, Stack);
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
