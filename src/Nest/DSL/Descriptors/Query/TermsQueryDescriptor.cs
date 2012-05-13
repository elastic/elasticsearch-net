using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
namespace Nest
{
	public class TermsQueryDescriptor<T> where T : class
	{
		internal string _Field { get; set; }
		internal int? _MinMatch { get; set; }
		internal IEnumerable<string> _Terms { get; set; }

		public TermsQueryDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public TermsQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			var fieldName = ElasticClient.PropertyNameResolver.Resolve(objectPath);
			return this.OnField(fieldName);
		}
		public TermsQueryDescriptor<T> MinimumMatch(int minMatch)
		{
			this._MinMatch = minMatch;
			return this;
		}
		public TermsQueryDescriptor<T> Terms(IEnumerable<string> terms)
		{
			this._Terms = terms;
			return this;
		}
		public TermsQueryDescriptor<T> Terms(params string[] terms)
		{
			this._Terms = terms;
			return this;
		}
	}
}
