using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json.Serialization;

namespace ElasticSearch.DSL
{
	public class Term : IQuery, IFieldQuery, IValue
	{
		public string Field { get; private set; }
		public string Value { get; private set; }
		public double Boost { get; private set; }
		
		public Term(string field, string value, double boost)
		{
			this.Value = value;
			this.Field = field;
			this.Boost = boost;
		}
		public Term(Field field)
		{
			field.ThrowIfNull("field");
		
			this.Field = field.Name;
			this.Value = field.Value;
			this.Boost = (field.Boost.HasValue) ? field.Boost.Value : 1.0;
		}		
	}


	public static class Query<T> where T : class
	{
		private static string FindMemberExpression(Expression<Func<T, object>> expression)
		{
			if (expression.Body is MemberExpression)
			{
				MemberExpression memberExpression = (MemberExpression)expression.Body;
				return memberExpression.Member.Name;
			}



			throw new Exception("Expression doesn't represent a property or field: " + expression.ToString());
		}


		public static Fuzzy Fuzzy(Expression<Func<T, object>> expression, string value)
		{
			string p = Query<T>.FindMemberExpression(expression);
			
			return new Fuzzy("", "");
		}
	}


}
