using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;
namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermsQuery : IQuery
	{
		PropertyPathMarker Field { get; set; }
		int? MinMatch { get; set; }
		bool DisableCord { get; set; }
		IEnumerable<object> Terms { get; set; }
		IExternalFieldDeclarationDescriptor ExternalField { get; set; }
		string CacheKey { get; set; }
	}

	/// <summary>
	/// A query that match on any (configurable) of the provided terms. 
	/// This is a simpler syntax query for using a bool query with several term queries in the should clauses.
	/// </summary>
	/// <typeparam name="T">The type that represents the expected hit type</typeparam>
	/// <typeparam name="K">The type of the field that we want to specfify terms for</typeparam>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TermsQueryDescriptor<T, K> : ITermsQuery, ICustomJson where T : class
	{
		PropertyPathMarker ITermsQuery.Field { get; set; }
		int? ITermsQuery.MinMatch { get; set; }
		bool ITermsQuery.DisableCord { get; set; }
		IEnumerable<object> ITermsQuery.Terms { get; set; }
		IExternalFieldDeclarationDescriptor ITermsQuery.ExternalField { get; set; }
		string ITermsQuery.CacheKey { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((ITermsQuery)this).Field.IsConditionless() 
					|| 
					(!((ITermsQuery)this).Terms.HasAny() && ((ITermsQuery)this).ExternalField == null);
			}
		}
	
		public TermsQueryDescriptor<T, K> CacheKey(string cacheKey)
		{
			((ITermsQuery)this).CacheKey = cacheKey;
			return this;
		}
		public TermsQueryDescriptor<T, K> OnField(string field)
		{
			((ITermsQuery)this).Field = field;
			return this;
		}
		public TermsQueryDescriptor<T, K> OnField(Expression<Func<T, K>> objectPath)
		{
			((ITermsQuery)this).Field = objectPath;
			return this;
		}

		public TermsQueryDescriptor<T, K> OnExternalField<TOther>(
			Func<ExternalFieldDeclarationDescriptor<TOther>, ExternalFieldDeclarationDescriptor<TOther>> externalFieldSelector
			)
			where TOther : class
		{
			externalFieldSelector.ThrowIfNull("externalFieldSelector");
			var descriptor = externalFieldSelector(new ExternalFieldDeclarationDescriptor<TOther>());
			((ITermsQuery)this).ExternalField = descriptor;
			return this;
		}


		public TermsQueryDescriptor<T, K> MinimumMatch(int minMatch)
		{
			((ITermsQuery)this).MinMatch = minMatch;
			return this;
		}
		public TermsQueryDescriptor<T, K> Terms(IEnumerable<string> terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty());

			((ITermsQuery)this).Terms = terms;
			return this;
		}
		public TermsQueryDescriptor<T, K> DisableCoord()
		{
			((ITermsQuery)this).DisableCord = true;
			return this;
		}

		public TermsQueryDescriptor<T, K> Terms(params string[] terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty()).ToArray();

			((ITermsQuery)this).Terms = terms;
			return this;
		}	
		
		public TermsQueryDescriptor<T, K> Terms(params K[] terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => t != null).ToArray();

			((ITermsQuery)this).Terms = terms.Cast<object>();
			return this;
		}

		object ICustomJson.GetCustomJson()
		{
			var termsQueryDescriptor = new Dictionary<PropertyPathMarker, object>();

			if (((ITermsQuery)this).ExternalField == null)
			{
				termsQueryDescriptor.Add(((ITermsQuery)this).Field, ((ITermsQuery)this).Terms);
			}
			else
			{
				termsQueryDescriptor.Add(((ITermsQuery)this).Field, ((ITermsQuery)this).ExternalField);
			}

			if (((ITermsQuery)this).MinMatch.HasValue)
			{
				termsQueryDescriptor.Add("minimum_match", ((ITermsQuery)this).MinMatch);
			}
			if (((ITermsQuery)this).DisableCord)
			{
				termsQueryDescriptor.Add("disable_coord", ((ITermsQuery)this).DisableCord);
			}
			if (!((ITermsQuery)this).CacheKey.IsNullOrEmpty())
			{
				termsQueryDescriptor.Add("_cache_key", ((ITermsQuery)this).CacheKey);
			}
			return termsQueryDescriptor;
		}
	}
}
