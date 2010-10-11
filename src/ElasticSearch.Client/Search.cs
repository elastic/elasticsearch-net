using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.DSL;
using System.Linq.Expressions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
	internal class Search<T> where T : class
	{
		public Search(IQuery<T> query)
		{
		
		}
		 
	
	
		protected string FindMemberExpression(Expression expression, Type typeinfo)
		{
			if (expression is MemberExpression)
			{
				MemberExpression memberExpression = (MemberExpression)expression;

				if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess
					|| memberExpression.Expression.NodeType == ExpressionType.Call)
				{
					if (memberExpression.Member.DeclaringType.IsGenericType
						&& memberExpression.Member.DeclaringType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
					{
						// it's a Nullable<T>, so ignore any .Value
						if (memberExpression.Member.Name == "Value")
							return FindMemberExpression(memberExpression.Expression, typeinfo);
					}

					return FindMemberExpression(memberExpression.Expression, typeinfo) + "." + memberExpression.Member.Name;
				}
				else
				{
					var mi = memberExpression.Member;
					var x = new CamelCasePropertyNamesContractResolver();
					var props = mi.GetCustomAttributes(true);
					var jsonProps = props.OfType<JsonPropertyAttribute>();


					return mi.Name;
				}
			}

			if (expression is UnaryExpression)
			{
				UnaryExpression unaryExpression = (UnaryExpression)expression;

				if (unaryExpression.NodeType != ExpressionType.Convert)
					throw new Exception("Cannot interpret member from " + expression.ToString());

				return FindMemberExpression(unaryExpression.Operand, typeinfo);
			}

			if (expression is MethodCallExpression)
			{
				MethodCallExpression methodCallExpression = (MethodCallExpression)expression;

				if (methodCallExpression.Method.Name == "GetType")
					return FindMemberExpression(methodCallExpression.Object, typeinfo) + ".class";

				if (methodCallExpression.Method.Name == "get_Item")
					return FindMemberExpression(methodCallExpression.Object, typeinfo);

				if (methodCallExpression.Method.Name == "First")
					return FindMemberExpression(methodCallExpression.Arguments[0], typeinfo);

				throw new Exception("Unrecognised method call in epression " + expression.ToString());
			}

			throw new Exception("Could not determine member from " + expression.ToString());
		}
	}
}
