using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq.Expressions;
using System.Reflection;
using Nest;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;

namespace Nest.Resolvers
{
	//Shout out to http://tomlev2.wordpress.com/2010/10/03/entity-framework-using-include-with-lambda-expressions/
	//replaces my sloppy 300+ lines (though working!) first attempt, thanks Thomas Levesque.

	public class PropertyNameResolver : ExpressionVisitor
	{
		private static readonly ConcurrentDictionary<string, string> _memoizedExpressions = new ConcurrentDictionary<string, string>(); 

		public IElasticPropertyAttribute GetElasticProperty(MemberInfo info)
		{
			var attributes = info.GetCustomAttributes(typeof(IElasticPropertyAttribute), true);
			if (attributes != null && attributes.Any())
				return ((IElasticPropertyAttribute)attributes.First());
			
			var ignoreAttrutes = info.GetCustomAttributes(typeof(JsonIgnoreAttribute), true);
			if (ignoreAttrutes != null && ignoreAttrutes.Any())
				return new ElasticPropertyAttribute { OptOut = true };

			return null;
		}

		public ElasticTypeAttribute GetElasticPropertyFor<T>() where T : class
		{
			return GetElasticPropertyForType(typeof(T));
		}

		public ElasticTypeAttribute GetElasticPropertyFor(Type type)
		{
			if (!type.IsClass && !type.IsInterface)
				throw new ArgumentException("Type is not a class or interface", "type");
			return GetElasticPropertyForType(type);
		}

		private ElasticTypeAttribute GetElasticPropertyForType(Type type)
		{
			if (!type.IsClass && !type.IsInterface)
				throw new ArgumentException("Type is not a class or interface", "type");

			var attributes = type.GetCustomAttributes(typeof(ElasticTypeAttribute), true);
			if (attributes != null && attributes.Any())
				return ((ElasticTypeAttribute)attributes.First());
			return null;
		}

		public string Resolve(MemberInfo info)
		{
			var name = info.Name;
			var resolvedName = name.ToCamelCase();
			var att = this.GetElasticProperty(info);
			if (att != null && !att.Name.IsNullOrEmpty())
				resolvedName = att.Name;

			return resolvedName;
		}


		public string Resolve(Expression expression)
		{
			//We can rely on ToString() because we only work with 
			//Property.Lookups.Only with Some.First().And[0].Mixin'd
			//To get field names
			var key = expression.ToString() + expression.Type.GetHashCode();
			string resolved;
			if (!_memoizedExpressions.TryGetValue(key, out resolved))
			{
				var stack = new Stack<string>();
				var properties = new Stack<IElasticPropertyAttribute>();
				Visit(expression, stack, properties);
				resolved = stack
					.Aggregate(
						new StringBuilder(),
						(sb, name) =>
						(sb.Length > 0 ? sb.Append(".") : sb).Append(name))
					.ToString();
				_memoizedExpressions.TryAdd(key, resolved);
			}
			return resolved;
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
				var resolvedName = name.ToCamelCase();

				var att = this.GetElasticProperty(expression.Member);
				if (att != null)
				{
					properties.Push(att);
				}
				if (att != null && !att.Name.IsNullOrEmpty())
				{

					resolvedName = att.Name;
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
