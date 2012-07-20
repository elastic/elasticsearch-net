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

namespace Nest.Resolvers
{
	//Shout out to http://tomlev2.wordpress.com/2010/10/03/entity-framework-using-include-with-lambda-expressions/
	//replaces my sloppy 300+ lines (though working!) first attempt, thanks Thomas Levesque.

	public class PropertyNameResolver : ExpressionVisitor
	{
		private Stack<ElasticPropertyAttribute> _properties;
		private JsonSerializerSettings SerializationSettings { get; set; }
		private ElasticResolver ContractResolver { get; set; }

		public PropertyNameResolver(JsonSerializerSettings settings)
		{
			this.SerializationSettings = settings;
			this.ContractResolver = settings.ContractResolver as ElasticResolver;
		}

		public ElasticPropertyAttribute GetElasticProperty(MemberInfo info)
		{
			var attributes = info.GetCustomAttributes(typeof(ElasticPropertyAttribute), true);
			if (attributes != null && attributes.Any())
				return ((ElasticPropertyAttribute)attributes.First());
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
			var resolvedName = this.ContractResolver.ResolvePropertyName(name);
			resolvedName = resolvedName.ToCamelCase();
			var att = this.GetElasticProperty(info);
			if (att != null && !att.Name.IsNullOrEmpty())
				resolvedName = att.Name;

			return resolvedName;
		}


		public string Resolve(Expression expression)
		{
			var stack = new Stack<string>();
			_properties = new Stack<ElasticPropertyAttribute>();
			Visit(expression, stack);
			return stack
				.Aggregate(
					new StringBuilder(),
					(sb, name) =>
						(sb.Length > 0 ? sb.Append(".") : sb).Append(name))
				.ToString();
		}
		public string ResolveToSort(Expression expression)
		{
			var stack = new Stack<string>();
			_properties = new Stack<ElasticPropertyAttribute>();
			Visit(expression, stack);
			return stack
					  .Aggregate(
						  new StringBuilder(),
						  (sb, name) =>
							  (sb.Length > 0 ? sb.Append(".") : sb)
								  .Append((_properties.Count > 0 && _properties.Pop().AddSortField) ? name + ".sort" : name))
					  .ToString();
		}
		protected override Expression VisitMemberAccess(MemberExpression expression, Stack<string> stack)
		{
			if (stack != null)
			{
				var name = expression.Member.Name;
				var resolvedName = this.ContractResolver.ResolvePropertyName(name).ToCamelCase();

				var att = this.GetElasticProperty(expression.Member);
				if (att != null)
				{
					_properties.Push(att);
				}
				if (att != null && !att.Name.IsNullOrEmpty())
				{

					resolvedName = att.Name;
				}
				stack.Push(resolvedName);
			}
			return base.VisitMemberAccess(expression, stack);
		}

		protected override Expression VisitMethodCall(MethodCallExpression m, Stack<string> stack)
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
					Visit(m.Arguments[i], stack);
				}
				Visit(m.Arguments[0], stack);
				return m;
			}
			return base.VisitMethodCall(m, stack);
		}
		private static bool IsLinqOperator(MethodInfo method)
		{
			if (method.DeclaringType != typeof(Queryable) && method.DeclaringType != typeof(Enumerable))
				return false;
			return Attribute.GetCustomAttribute(method, typeof(ExtensionAttribute)) != null;
		}
	}
}
