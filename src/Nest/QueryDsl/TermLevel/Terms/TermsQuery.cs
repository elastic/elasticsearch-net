using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(TermsQueryJsonConverter))]
	public interface ITermsQuery : IFieldNameQuery
	{
		string MinimumShouldMatch { get; set; }
		bool? DisableCoord { get; set; }
		IEnumerable<object> Terms { get; set; }
		IExternalFieldDeclaration ExternalField { get; set; }
	}

	public class TermsQuery : FieldNameQueryBase, ITermsQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string MinimumShouldMatch { get; set; }
		public bool? DisableCoord { get; set; }
		public IEnumerable<object> Terms { get; set; }
		public IExternalFieldDeclaration ExternalField { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Terms = this;
		internal static bool IsConditionless(ITermsQuery q)
		{
			return q.Field.IsConditionless() || (!q.Terms.HasAny() && q.ExternalField == null);
		}
	}

	/// <summary>
	/// A query that match on any (configurable) of the provided terms. 
	/// This is a simpler syntax query for using a bool query with several term queries in the should clauses.
	/// </summary>
	/// <typeparam name="T">The type that represents the expected hit type</typeparam>
	/// <typeparam name="K">The type of the field that we want to specfify terms for</typeparam>
	public class TermsQueryDescriptor<T, K> 
		: FieldNameQueryDescriptorBase<TermsQueryDescriptor<T, K>, ITermsQuery, T>
		, ITermsQuery where T : class
	{
		bool IQuery.Conditionless => TermsQuery.IsConditionless(this);
		string ITermsQuery.MinimumShouldMatch { get; set; }
		bool? ITermsQuery.DisableCoord { get; set; }
		IEnumerable<object> ITermsQuery.Terms { get; set; }
		IExternalFieldDeclaration ITermsQuery.ExternalField { get; set; }

		public TermsQueryDescriptor<T, K> OnExternalField<TOther>(
			Func<ExternalFieldDeclarationDescriptor<TOther>, ExternalFieldDeclarationDescriptor<TOther>> selector)
			where TOther : class => Assign(a => a.ExternalField = selector(new ExternalFieldDeclarationDescriptor<TOther>()));

		public TermsQueryDescriptor<T, K> MinimumShouldMatch(string minMatch) => 
			Assign(a => a.MinimumShouldMatch = minMatch);

		public TermsQueryDescriptor<T, K> MinimumShouldMatch(int minMatch) => Assign(a => a.MinimumShouldMatch = minMatch.ToString(CultureInfo.InvariantCulture));

		public TermsQueryDescriptor<T, K> DisableCoord() => Assign(a => a.DisableCoord = true);

		public TermsQueryDescriptor<T, K> Terms(IEnumerable<string> terms) => Assign(a =>
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty());
			a.Terms = terms;
		});

		public TermsQueryDescriptor<T, K> Terms(IEnumerable<K> terms) => Assign(a =>
		{
			if (terms.HasAny())
				terms = terms.Where(t => t != null).ToArray();
			a.Terms = terms.Cast<object>();
		});
	}
}
