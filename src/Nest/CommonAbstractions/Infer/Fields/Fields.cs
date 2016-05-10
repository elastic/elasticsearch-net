using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(FieldsJsonConverter))]
	public class Fields : IUrlParameter, IEnumerable<Field>
	{
		internal readonly List<Field> ListOfFields;

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) =>
			string.Join(",", ListOfFields.Select(f => ((IUrlParameter)f).GetString(settings)));

		internal Fields() { this.ListOfFields = new List<Field>(); }
		internal Fields(IEnumerable<Field> fieldNames) { this.ListOfFields = fieldNames.ToList(); }

		public static implicit operator Fields(string[] fields) => new Fields(fields.Select(f => (Field)f));

		public static implicit operator Fields(Expression[] fields) => new Fields(fields.Select(f => (Field)f));

		public static implicit operator Fields(Field[] fields) => new Fields(fields);

		public static implicit operator Fields(Field field) => new Fields(new[] { field });

		public Fields And<T>(Expression<Func<T, object>> field, double? boost = null) where T : class
		{
			this.ListOfFields.Add(Field.Create(field, boost));
			return this;
		}
		public Fields And(string field, double? boost = null)
		{
			this.ListOfFields.Add(Field.Create(field, boost));
			return this;
		}

		public Fields And<T>(params Expression<Func<T, object>>[] fields) where T : class
		{
			this.ListOfFields.AddRange(fields.Select(f => (Field)f));
			return this;
		}

		public Fields And(params string[] fields)
		{
			this.ListOfFields.AddRange(fields.Select(f => (Field)f));
			return this;
		}

		public Fields And(params Field[] fields)
		{
			this.ListOfFields.AddRange(fields);
			return this;
		}

		public IEnumerator<Field> GetEnumerator()
		{
			return this.ListOfFields.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
