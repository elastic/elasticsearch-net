using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;

namespace Nest.Resolvers
{
	//Shout out to http://tomlev2.wordpress.com/2010/10/03/entity-framework-using-include-with-lambda-expressions/
	//replaces my sloppy 300+ lines (though working!) first attempt, thanks Thomas Levesque.

	public static class ElasticAttributes
	{
		private static readonly ConcurrentDictionary<Type, ElasticTypeAttribute> CachedTypeLookups =
			new ConcurrentDictionary<Type, ElasticTypeAttribute>();

		public static IEnumerable<IElasticPropertyAttribute> Property(MemberInfo info)
		{
			var attributes = info.GetCustomAttributes(typeof(IElasticPropertyAttribute), true);
			if (attributes.Any())
				return ((IEnumerable<IElasticPropertyAttribute>)attributes);

			var ignoreAttrutes = info.GetCustomAttributes(typeof(JsonIgnoreAttribute), true);
			if (ignoreAttrutes.Any())
				return ((IEnumerable<IElasticPropertyAttribute>)new ElasticPropertyAttribute { OptOut = true });

			return null;
		}

		public static ElasticTypeAttribute Type<T>() where T : class
		{
			return _Type(typeof(T));
		}

		public static ElasticTypeAttribute Type(Type type)
		{
			return _Type(type);
		}

		private static ElasticTypeAttribute _Type(Type type)
		{
			ElasticTypeAttribute attr = null;
			if (CachedTypeLookups.TryGetValue(type, out attr))
				return attr;

			if (!type.IsClass && !type.IsInterface)
				throw new ArgumentException("Type is not a class or interface", "type");

			var attributes = type.GetCustomAttributes(typeof(ElasticTypeAttribute), true);
			if (attributes.HasAny())
				attr = ((ElasticTypeAttribute)attributes.First());
			CachedTypeLookups.TryAdd(type, attr);
			return attr;
		}
	}

	public class PropertyNameResolver : ExpressionVisitor
	{

		private readonly IConnectionSettingsValues _settings;
		public PropertyNameResolver(IConnectionSettingsValues settings)
		{
			_settings = settings;
		}

		public string Resolve(MemberInfo info)
		{
			if (info == null)
				return null;

			var name = info.Name;
			var resolvedName = _settings.DefaultPropertyNameInferrer(name);
			var att = ElasticAttributes.Property(info);
			if (att != null && att.Any() != null && !att.First().Name.IsNullOrEmpty())
				resolvedName = att.First().Name;

			return resolvedName;
		}

		public string ResolveToLastToken(MemberInfo info)
		{
			var propertyName = this.Resolve(info);
			return propertyName == null ? null : propertyName.Split(',').Last();
		}

		public string Resolve(Expression expression)
		{
			var stack = new Stack<string>();
			var properties = new Stack<IElasticPropertyAttribute>();
			Visit(expression, stack, properties);
			return stack
				.Aggregate(
					new StringBuilder(),
					(sb, name) =>
					(sb.Length > 0 ? sb.Append(".") : sb).Append(name))
				.ToString();
		}

		public string ResolveToLastToken(Expression expression)
		{
			var stack = new Stack<string>();
			var properties = new Stack<IElasticPropertyAttribute>();
			Visit(expression, stack, properties);
			return stack.Last();
		}

		public Stack<IElasticPropertyAttribute> ResolvePropertyAttributes(Expression expression)
		{
			var stack = new Stack<string>();
			var attributes = new Stack<IElasticPropertyAttribute>();

			Visit(expression, stack, attributes);

			return attributes;
		}

		protected override Expression VisitMemberAccess(MemberExpression expression, Stack<string> stack, Stack<IElasticPropertyAttribute> properties)
		{
			if (stack != null)
			{
				var name = expression.Member.Name;
				var resolvedName = this._settings.DefaultPropertyNameInferrer(name);

				var att = ElasticAttributes.Property(expression.Member);
				if (att != null && att.Any())
				{
					properties.Push(att.First());
				}
				if (att != null && att.Any() && !att.First().Name.IsNullOrEmpty())
				{

					resolvedName = att.First().Name;
				}
				stack.Push(resolvedName);
			}
			return base.VisitMemberAccess(expression, stack, properties);
		}

		protected override Expression VisitMethodCall(MethodCallExpression m, Stack<string> stack, Stack<IElasticPropertyAttribute> properties)
		{
			if (m.Method.Name == "Suffix" && m.Arguments.Any())
			{
				var constantExpression = m.Arguments.Last() as ConstantExpression;
				if (constantExpression != null)
					stack.Push(constantExpression.Value as string);
			}
			else if (m.Method.Name == "get_Item" && m.Arguments.Any())
			{
				if (!typeof(IDictionary).IsAssignableFrom(m.Object.Type))
				{
					return base.VisitMethodCall(m, stack, properties);
				}
				var lastArg = m.Arguments.Last();
				var constantExpression = lastArg as ConstantExpression;
				var value = constantExpression != null
					? constantExpression.Value.ToString()
					: Expression.Lambda(lastArg).Compile().DynamicInvoke().ToString();
				stack.Push(value);
				Visit(m.Object, stack, properties);
				return m;
			}
			if (IsLinqOperator(m.Method))
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
		private static bool IsLinqOperator(MethodInfo method)
		{
			if (method.DeclaringType != typeof(Queryable) && method.DeclaringType != typeof(Enumerable))
				return false;
			return Attribute.GetCustomAttribute(method, typeof(ExtensionAttribute)) != null;
		}
	}
}
