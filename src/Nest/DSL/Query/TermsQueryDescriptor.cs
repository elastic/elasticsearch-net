using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest.Resolvers.Converters.Queries;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(TermsQueryJsonConverter))]
	public interface ITermsQuery : IQuery
	{
		PropertyPathMarker Field { get; set; }
		string MinimumShouldMatch { get; set; }
		bool? DisableCoord { get; set; }
		IEnumerable<object> Terms { get; set; }
		IExternalFieldDeclarationDescriptor ExternalField { get; set; }
		double? Boost { get; set; }
	}

	public class TermsQuery : PlainQuery, ITermsQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Terms = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public PropertyPathMarker Field { get; set; }
		public string MinimumShouldMatch { get; set; }
		public bool? DisableCoord { get; set; }
		public IEnumerable<object> Terms { get; set; }
		public IExternalFieldDeclarationDescriptor ExternalField { get; set; }
		public double? Boost { get; set; }
	}

	/// <summary>
	/// A query that match on any (configurable) of the provided terms. 
	/// This is a simpler syntax query for using a bool query with several term queries in the should clauses.
	/// </summary>
	/// <typeparam name="T">The type that represents the expected hit type</typeparam>
	/// <typeparam name="K">The type of the field that we want to specfify terms for</typeparam>
	public class TermsQueryDescriptor<T, K> : ITermsQuery where T : class
	{
		PropertyPathMarker ITermsQuery.Field { get; set; }
		string ITermsQuery.MinimumShouldMatch { get; set; }
		bool? ITermsQuery.DisableCoord { get; set; }
		IEnumerable<object> ITermsQuery.Terms { get; set; }
		IExternalFieldDeclarationDescriptor ITermsQuery.ExternalField { get; set; }
		double? ITermsQuery.Boost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				var termsQuery = ((ITermsQuery)this);
				return termsQuery.Field.IsConditionless() 
					|| (!termsQuery.Terms.HasAny() && termsQuery.ExternalField == null);
			}
		}
	
		public TermsQueryDescriptor<T, K> Boost(double boost)
		{
			((ITermsQuery)this).Boost = boost;
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

		public TermsQueryDescriptor<T, K> MinimumShouldMatch(string minMatch)
		{
			((ITermsQuery)this).MinimumShouldMatch = minMatch;
			return this;
		}
		public TermsQueryDescriptor<T, K> MinimumShouldMatch(int minMatch)
		{
			((ITermsQuery)this).MinimumShouldMatch = minMatch.ToString(CultureInfo.InvariantCulture);
			return this;
		}


		public TermsQueryDescriptor<T, K> DisableCoord()
		{
			((ITermsQuery)this).DisableCoord = true;
			return this;
		}

		
		public TermsQueryDescriptor<T, K> Terms(IEnumerable<string> terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty());

			((ITermsQuery)this).Terms = terms;
			return this;
		}
		public TermsQueryDescriptor<T, K> Terms(IEnumerable<K> terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => t != null).ToArray();

			((ITermsQuery)this).Terms = terms.Cast<object>();
			return this;
		}

	}
}
