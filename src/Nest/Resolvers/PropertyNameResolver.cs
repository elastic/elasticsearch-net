using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq.Expressions;
using System.Reflection;
using Fasterflect;
using System.Runtime.CompilerServices;

namespace Nest.Resolvers
{
	//Shout out to http://tomlev2.wordpress.com/2010/10/03/entity-framework-using-include-with-lambda-expressions/
	//replaces my sloppy 300+ lines (though working!) first attempt, thanks Thomas Levesque.

	public class PropertyNameResolver : ExpressionVisitor
	{
		private Stack<string> _stack;
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
			var attributes = typeof(T).GetCustomAttributes(typeof(ElasticTypeAttribute), true);
			if (attributes != null && attributes.Any())
				return ((ElasticTypeAttribute)attributes.First());
			return null;
		}

		public string Resolve(MemberInfo info)
		{
			var name = info.Name;
			var resolvedName = this.ContractResolver.ResolvePropertyName(name);

			var att = this.GetElasticProperty(info);
			if (att != null && !att.Name.IsNullOrEmpty())
				resolvedName = att.Name;

			return resolvedName;
		}


		public string Resolve(Expression expression)
		{
			_stack = new Stack<string>();
			_properties = new Stack<ElasticPropertyAttribute>();
			Visit(expression);
			return _stack
				.Aggregate(
					new StringBuilder(),
					(sb, name) =>
						(sb.Length > 0 ? sb.Append(".") : sb).Append(name))
				.ToString();
		}
		public string ResolveToSort(Expression expression)
		{
			_stack = new Stack<string>();
			_properties = new Stack<ElasticPropertyAttribute>();
			Visit(expression);
			return _stack
				.Aggregate(
					new StringBuilder(),
					(sb, name) =>
						(sb.Length > 0 ? sb.Append(".") : sb)
							.Append((_properties.Count > 0 && _properties.Pop().AddSortField) ? name + ".sort" : name))
				.ToString();
		}
		protected override Expression VisitMemberAccess(MemberExpression expression)
		{
			if (_stack != null)
			{ 
				var name = expression.Member.Name;
				var resolvedName = this.ContractResolver.ResolvePropertyName(name);

				var att = this.GetElasticProperty(expression.Member);
				if (att != null)
				{
					_properties.Push(att);
				}
				if (att != null && !att.Name.IsNullOrEmpty())
				{
					
					resolvedName = att.Name;
				}
				_stack.Push(resolvedName);
			}
			return base.VisitMemberAccess(expression);
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			if (IsLinqOperator(expression.Method))
			{
				for (int i = 1; i < expression.Arguments.Count; i++)
				{
					Visit(expression.Arguments[i]);
				}
				Visit(expression.Arguments[0]);
				return expression;
			}
			return base.VisitMethodCall(expression);
		}

		private static bool IsLinqOperator(MethodInfo method)
		{
			if (method.DeclaringType != typeof(Queryable) && method.DeclaringType != typeof(Enumerable))
				return false;
			return Attribute.GetCustomAttribute(method, typeof(ExtensionAttribute)) != null;
		}
	}
}
