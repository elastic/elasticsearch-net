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
	public class Fields : IUrlParameter, IEnumerable<Field>, IEquatable<Fields>
	{
		internal readonly List<Field> ListOfFields;

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			if (!(settings is IConnectionSettingsValues nestSettings))
				throw new ArgumentNullException(nameof(settings), $"Can not resolve {nameof(Fields)} if no {nameof(IConnectionSettingsValues)} is provided");

			return string.Join(",", ListOfFields.Where(f => f != null).Select(f => ((IUrlParameter)f).GetString(nestSettings)));
		}

		private string DebugDisplay =>
			$"Count: {ListOfFields.Count} [" + string.Join(",", ListOfFields.Select((t, i) => $"({i + 1}: {t?.DebugDisplay ?? "NULL"})")) + "]";

		internal Fields() { this.ListOfFields = new List<Field>(); }
		internal Fields(IEnumerable<Field> fieldNames) { this.ListOfFields = fieldNames.ToList(); }

		public static implicit operator Fields(string[] fields) => fields.IsEmpty() ? null : new Fields(fields.Select(f => (Field)f));

		public static implicit operator Fields(string field) => field.IsNullOrEmptyCommaSeparatedList(out var split)
			? null : new Fields(split.Select(f=>(Field)f));

		public static implicit operator Fields(Expression[] fields) => fields.IsEmpty() ? null : new Fields(fields.Select(f => (Field)f));

		public static implicit operator Fields(Expression field) => field == null ? null : new Fields(new [] { (Field)field });

		public static implicit operator Fields(Field field) => field == null ? null : new Fields(new[] { field });

		public static implicit operator Fields(PropertyInfo field) => field == null ? null : new Fields(new Field[] { field });

		public static implicit operator Fields(PropertyInfo[] fields) => fields.IsEmpty() ? null : new Fields(fields.Select(f=>(Field)f));

		public static implicit operator Fields(Field[] fields) => fields.IsEmpty() ? null : new Fields(fields);

		public Fields And<T>(Expression<Func<T, object>> field, double? boost = null) where T : class
		{
			this.ListOfFields.Add(new Field(field, boost));
			return this;
		}

		public Fields And(string field, double? boost = null)
		{
			this.ListOfFields.Add(new Field(field, boost));
			return this;
		}

		public Fields And(PropertyInfo property, double? boost = null)
		{
			this.ListOfFields.Add(new Field(property, boost));
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

		public Fields And(params PropertyInfo[] properties)
		{
			this.ListOfFields.AddRange(properties.Select(f => (Field)f));
			return this;
		}

		public Fields And(params Field[] fields)
		{
			this.ListOfFields.AddRange(fields);
			return this;
		}

		public IEnumerator<Field> GetEnumerator() => this.ListOfFields.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		public static bool operator ==(Fields left, Fields right) => Equals(left, right);

		public static bool operator !=(Fields left, Fields right) => !Equals(left, right);

		public bool Equals(Fields other) => EqualsAllFields(this.ListOfFields, other.ListOfFields);

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case Fields f: return Equals(f);
				case string s: return Equals(s);
				case Field fn: return Equals(fn);
				case Field[] fns: return Equals(fns);
				case Expression e: return Equals(e);
				case Expression[] es: return Equals(es);
				default: return false;
			}
		}

		private static bool EqualsAllFields(IReadOnlyList<Field> thisTypes, IReadOnlyList<Field> otherTypes)
		{
			if (thisTypes == null && otherTypes == null) return true;
			if (thisTypes == null || otherTypes == null) return false;
			if (thisTypes.Count != otherTypes.Count) return false;
			return thisTypes.Count == otherTypes.Count && !thisTypes.Except(otherTypes).Any();
		}

		public override int GetHashCode() => this.ListOfFields.GetHashCode();
	}
}
