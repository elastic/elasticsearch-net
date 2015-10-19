using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class FieldNames : IUrlParameter
	{
		private readonly IList<FieldName> _fieldNames;

		public string GetString(IConnectionConfigurationValues settings) =>
			string.Join(",", _fieldNames.Select(f => ((IUrlParameter)f).GetString(settings)));

		internal FieldNames(IEnumerable<FieldName> fieldNames) { this._fieldNames = fieldNames.ToList(); }

		public static implicit operator FieldNames(string[] fields) => new FieldNames(fields.Select(f => (FieldName)f));

		public static implicit operator FieldNames(Expression[] fields) => new FieldNames(fields.Select(f => (FieldName)f));

		public FieldNames And<T>(Expression<Func<T, object>> field) where T : class
		{
			this._fieldNames.Add(field);
			return this;
		}
		public FieldNames And(string field)
		{
			this._fieldNames.Add(field);
			return this;
		}
	}
}
