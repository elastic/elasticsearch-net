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
	public class Fuzzy<T> : Query<T> where T : class
	{
		public Fuzzy(QueryDescriptor queryDescriptor) : base(queryDescriptor) { }
	}
	public class QueryDescriptor 
	{
		public MemberExpression MemberExpression { get; set; }
		public Func<string, IQuery> QuerySelector { get; set; }
	}
	public class Query<T> where T : class
	{
		internal IList<QueryDescriptor> Queries { get; set; }

		public Query(QueryDescriptor queryDescriptor)
		{
			this.Queries = new List<QueryDescriptor>();
			this.Queries.Add(queryDescriptor);
		}

		public static Query<T> Fuzzy(Expression<Func<T, object>> expression, string value)
		{
			var queryDescriptor = new QueryDescriptor()
			{
				MemberExpression = Query<T>.FindMemberExpression(expression),
				QuerySelector = (s) => new Fuzzy(s, value)
			};
			return new Query<T>(queryDescriptor);
		}
		public Query<T> AndFuzzy(Expression<Func<T, object>> expression, string value)
		{
			var queryDescriptor = new QueryDescriptor()
			{
				MemberExpression = Query<T>.FindMemberExpression(expression),
				QuerySelector = (s) => new Fuzzy(s, value)
			};
			this.Queries.Add(queryDescriptor);
			return this;
		}

		internal static MemberExpression FindMemberExpression(Expression<Func<T, object>> expression) 
		{
			if (expression.Body is MemberExpression)
			{
				//((MemberExpression)memberExpression.Expression).Expression
				var memberExpression = (MemberExpression)expression.Body;
				return memberExpression;
			}



			throw new Exception("Expression doesn't represent a property or field: " + expression.ToString());
		}
	}



}
