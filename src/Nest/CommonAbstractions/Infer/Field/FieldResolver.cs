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
using Elasticsearch.Net;

namespace Nest
{
	//Shout out to http://tomlev2.wordpress.com/2010/10/03/entity-framework-using-include-with-lambda-expressions/
	//replaces my sloppy 300+ lines (though working!) first attempt, thanks Thomas Levesque.	
	public class FieldResolver : ExpressionVisitor
	{
		private readonly IConnectionSettingsValues _settings;

		private readonly ConcurrentDictionary<Field, string> Fields = new ConcurrentDictionary<Field, string>();
		private readonly ConcurrentDictionary<PropertyName, string> Properties = new ConcurrentDictionary<PropertyName, string>();

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

			return _settings.Serializer?.CreatePropertyMapping(info)?.Name ?? _settings.DefaultFieldNameInferrer(name);
		}

		private string Resolve(Expression expression)
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

		private string ResolveToLastToken(Expression expression)
		{
			var stack = new Stack<string>();
			var properties = new Stack<ElasticsearchPropertyAttribute>();
			Visit(expression, stack, properties);
			return stack.Last();
		}

		protected override Expression VisitMemberAccess(MemberExpression expression, Stack<string> stack, Stack<ElasticsearchPropertyAttribute> properties)
		{
			if (stack == null) return base.VisitMemberAccess(expression, stack, properties);
			var resolvedName = this.Resolve(expression.Member);
			stack.Push(resolvedName);
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
					|| (t.IsGeneric() && t.GetGenericTypeDefinition() == typeof(IDictionary<,>));

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

			return method.GetCustomAttribute<ExtensionAttribute>() != null;
			//return Attribute.GetCustomAttribute(method, typeof(ExtensionAttribute)) != null;
		}
	}
}
