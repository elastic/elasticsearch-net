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
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermsQuery
	{
		PropertyPathMarker _Field { get; set; }
		int? _MinMatch { get; set; }
		bool _DisableCord { get; set; }
		IEnumerable<object> _Terms { get; set; }
		IExternalFieldDeclarationDescriptor _ExternalField { get; set; }
		string _CacheKey { get; set; }
	}

	/// <summary>
	/// A query that match on any (configurable) of the provided terms. 
	/// This is a simpler syntax query for using a bool query with several term queries in the should clauses.
	/// </summary>
	/// <typeparam name="T">The type that represents the expected hit type</typeparam>
	/// <typeparam name="K">The type of the field that we want to specfify terms for</typeparam>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TermsQueryDescriptor<T, K> : IQuery, ITermsQuery where T : class
	{
		PropertyPathMarker ITermsQuery._Field { get; set; }
		int? ITermsQuery._MinMatch { get; set; }
		bool ITermsQuery._DisableCord { get; set; }
		IEnumerable<object> ITermsQuery._Terms { get; set; }
		IExternalFieldDeclarationDescriptor ITermsQuery._ExternalField { get; set; }
		string ITermsQuery._CacheKey { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((ITermsQuery)this)._Field.IsConditionless() 
					|| 
					(!((ITermsQuery)this)._Terms.HasAny() && ((ITermsQuery)this)._ExternalField == null);
			}
		}
		public TermsQueryDescriptor<T, K> CacheKey(string cacheKey)
		{
			((ITermsQuery)this)._CacheKey = cacheKey;
			return this;
		}
		public TermsQueryDescriptor<T, K> OnField(string field)
		{
			((ITermsQuery)this)._Field = field;
			return this;
		}
		public TermsQueryDescriptor<T, K> OnField(Expression<Func<T, K>> objectPath)
		{
			((ITermsQuery)this)._Field = objectPath;
			return this;
		}

		public TermsQueryDescriptor<T, K> OnExternalField<TOther>(
			Func<ExternalFieldDeclarationDescriptor<TOther>, ExternalFieldDeclarationDescriptor<TOther>> externalFieldSelector
			)
			where TOther : class
		{
			externalFieldSelector.ThrowIfNull("externalFieldSelector");
			var descriptor = externalFieldSelector(new ExternalFieldDeclarationDescriptor<TOther>());
			((ITermsQuery)this)._ExternalField = descriptor;
			return this;
		}


		public TermsQueryDescriptor<T, K> MinimumMatch(int minMatch)
		{
			((ITermsQuery)this)._MinMatch = minMatch;
			return this;
		}
		public TermsQueryDescriptor<T, K> Terms(IEnumerable<string> terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty());

			((ITermsQuery)this)._Terms = terms;
			return this;
		}
		public TermsQueryDescriptor<T, K> DisableCoord()
		{
			((ITermsQuery)this)._DisableCord = true;
			return this;
		}

		public TermsQueryDescriptor<T, K> Terms(params string[] terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty()).ToArray();

			((ITermsQuery)this)._Terms = terms;
			return this;
		}	
		
		public TermsQueryDescriptor<T, K> Terms(params K[] terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => t != null).ToArray();

			((ITermsQuery)this)._Terms = terms.Cast<object>();
			return this;
		}
	}
}
