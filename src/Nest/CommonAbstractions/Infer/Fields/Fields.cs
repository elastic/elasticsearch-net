using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(FieldsJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Fields : IUrlParameter, IEnumerable<Field>
	{
		internal readonly List<Field> ListOfFields;


		internal Fields() => ListOfFields = new List<Field>();

		internal Fields(IEnumerable<Field> fieldNames) => ListOfFields = fieldNames.ToList();

		private string DebugDisplay =>
			$"Count: {ListOfFields.Count} [" + string.Join(",", ListOfFields.Select((t, i) => $"({i + 1}: {t.DebugDisplay})")) + "]";

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<Field> GetEnumerator() => ListOfFields.GetEnumerator();

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) =>
			string.Join(",", ListOfFields.Select(f => ((IUrlParameter)f).GetString(settings)));

		public static implicit operator Fields(string[] fields) => new Fields(fields.Select(f => (Field)f));

		public static implicit operator Fields(string field) =>
			new Fields(field.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(f => (Field)f));

		public static implicit operator Fields(Expression[] fields) => new Fields(fields.Select(f => (Field)f));

		public static implicit operator Fields(Expression field) => new Fields(new[] { (Field)field });

		public static implicit operator Fields(Field field) => new Fields(new[] { field });

		public static implicit operator Fields(Field[] fields) => new Fields(fields);

		public Fields And<T>(Expression<Func<T, object>> field, double? boost = null) where T : class
		{
			ListOfFields.Add(new Field(field, boost));
			return this;
		}

		public Fields And(string field, double? boost = null)
		{
			ListOfFields.Add(new Field(field, boost));
			return this;
		}

		public Fields And(PropertyInfo property, double? boost = null)
		{
			ListOfFields.Add(new Field(property, boost));
			return this;
		}

		public Fields And<T>(params Expression<Func<T, object>>[] fields) where T : class
		{
			ListOfFields.AddRange(fields.Select(f => (Field)f));
			return this;
		}

		public Fields And(params string[] fields)
		{
			ListOfFields.AddRange(fields.Select(f => (Field)f));
			return this;
		}

		public Fields And(params PropertyInfo[] properties)
		{
			ListOfFields.AddRange(properties.Select(f => (Field)f));
			return this;
		}

		public Fields And(params Field[] fields)
		{
			ListOfFields.AddRange(fields);
			return this;
		}
	}
}
