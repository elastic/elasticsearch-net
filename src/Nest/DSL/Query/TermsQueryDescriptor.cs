using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;
namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TermsQueryDescriptor<T> : IQuery where T : class
	{
		internal string _Field { get; set; }
		internal int? _MinMatch { get; set; }
		internal bool _DisableCord { get; set; }
		internal IEnumerable<string> _Terms { get; set; }

		internal IExternalFieldDeclarationDescriptor _ExternalField { get; set; }

		internal string _CacheKey { get; set; }

		internal bool IsConditionless
		{
			get
			{
				return this._Field.IsNullOrEmpty() 
					|| 
					(!this._Terms.HasAny() && this._ExternalField == null);
			}
		}
		public TermsQueryDescriptor<T> CacheKey(string cacheKey)
		{
			this._CacheKey = cacheKey;
			return this;
		}
		public TermsQueryDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public TermsQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			var fieldName = new PropertyNameResolver().Resolve(objectPath);
			return this.OnField(fieldName);
		}

		public TermsQueryDescriptor<T> OnExternalField<K>(
			Func<ExternalFieldDeclarationDescriptor<K>, ExternalFieldDeclarationDescriptor<K>> externalFieldSelector
			)
			where K : class
		{
			externalFieldSelector.ThrowIfNull("externalFieldSelector");
			var descriptor = externalFieldSelector(new ExternalFieldDeclarationDescriptor<K>());
			this._ExternalField = descriptor;
			return this;
		}


		public TermsQueryDescriptor<T> MinimumMatch(int minMatch)
		{
			this._MinMatch = minMatch;
			return this;
		}
		public TermsQueryDescriptor<T> Terms(IEnumerable<string> terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty());

			this._Terms = terms;
			return this;
		}
		public TermsQueryDescriptor<T> DisableCoord()
		{
			this._DisableCord = true;
			return this;
		}

		public TermsQueryDescriptor<T> Terms(params string[] terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty()).ToArray();

			this._Terms = terms;
			return this;
		}
	}
}
