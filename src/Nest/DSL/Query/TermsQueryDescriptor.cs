using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;
namespace Nest
{
	/// <summary>
	/// A query that match on any (configurable) of the provided terms. 
	/// This is a simpler syntax query for using a bool query with several term queries in the should clauses.
	/// </summary>
	/// <typeparam name="T">The type that represents the expected hit type</typeparam>
	/// <typeparam name="K">The type of the field that we want to specfify terms for</typeparam>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TermsQueryDescriptor<T, K> : IQuery where T : class
	{
		internal PropertyPathMarker _Field { get; set; }
		internal int? _MinMatch { get; set; }
		internal bool _DisableCord { get; set; }
		internal IEnumerable<object> _Terms { get; set; }

		internal IExternalFieldDeclarationDescriptor _ExternalField { get; set; }

		internal string _CacheKey { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return this._Field.IsConditionless() 
					|| 
					(!this._Terms.HasAny() && this._ExternalField == null);
			}
		}
		public TermsQueryDescriptor<T, K> CacheKey(string cacheKey)
		{
			this._CacheKey = cacheKey;
			return this;
		}
		public TermsQueryDescriptor<T, K> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public TermsQueryDescriptor<T, K> OnField(Expression<Func<T, K>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}

		public TermsQueryDescriptor<T, K> OnExternalField<TOther>(
			Func<ExternalFieldDeclarationDescriptor<TOther>, ExternalFieldDeclarationDescriptor<TOther>> externalFieldSelector
			)
			where TOther : class
		{
			externalFieldSelector.ThrowIfNull("externalFieldSelector");
			var descriptor = externalFieldSelector(new ExternalFieldDeclarationDescriptor<TOther>());
			this._ExternalField = descriptor;
			return this;
		}


		public TermsQueryDescriptor<T, K> MinimumMatch(int minMatch)
		{
			this._MinMatch = minMatch;
			return this;
		}
		public TermsQueryDescriptor<T, K> Terms(IEnumerable<string> terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty());

			this._Terms = terms;
			return this;
		}
		public TermsQueryDescriptor<T, K> DisableCoord()
		{
			this._DisableCord = true;
			return this;
		}

		public TermsQueryDescriptor<T, K> Terms(params string[] terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty()).ToArray();

			this._Terms = terms;
			return this;
		}	
		
		public TermsQueryDescriptor<T, K> Terms(params K[] terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => t != null).ToArray();

			this._Terms = terms.Cast<object>();
			return this;
		}
	}
}
