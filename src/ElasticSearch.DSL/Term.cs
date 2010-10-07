using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json.Serialization;

namespace ElasticSearch.DSL
{
	public class Term : IQuery
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
			if (field.Boost.HasValue)
				this.Boost = field.Boost.Value;
		}		
	}
	
	public class Query<T> : IQuery<T>
	{
		public Expression<Func<T, object>> Expression { get; private set; }

	}

	public class Term<T> : Query<T>
	{
		
		public Term(Expression<Func<T,object>> bindTo, double boost)
		{
			this.Expression = 
			var t = typeof(T);
			var x = this.FindMemberExpression(bindTo.Body, typeof(T));
			
			
		}
		
	}
}
