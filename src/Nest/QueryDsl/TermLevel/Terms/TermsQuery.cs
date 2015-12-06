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
		MinimumShouldMatch MinimumShouldMatch { get; set; }
		bool? DisableCoord { get; set; }
		IEnumerable<object> Terms { get; set; }
		IFieldLookup TermsLookup { get; set; }
	}

	public class TermsQuery : FieldNameQueryBase, ITermsQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		public bool? DisableCoord { get; set; }
		public IEnumerable<object> Terms { get; set; }
		public IFieldLookup TermsLookup { get; set; }

		internal override void WrapInContainer(IQueryContainer c) => c.Terms = this;
		internal static bool IsConditionless(ITermsQuery q)
		{
			return q.Field.IsConditionless() 
				|| (
				(!q.Terms.HasAny() || q.Terms.All(t=>t == null || ((t as string)?.IsNullOrEmpty()).GetValueOrDefault(false))
				)
				&& 
				(q.TermsLookup == null
					|| q.TermsLookup.Id == null
					|| q.TermsLookup.Path.IsConditionless()
					|| q.TermsLookup.Index == null
					|| q.TermsLookup.Type == null
				));
		}
	}

	/// <summary>
	/// A query that match on any (configurable) of the provided terms. 
	/// This is a simpler syntax query for using a bool query with several term queries in the should clauses.
	/// </summary>
	/// <typeparam name="T">The type that represents the expected hit type</typeparam>
	/// <typeparam name="TValue">The type of the field that we want to specfify terms for</typeparam>
	public class TermsQueryDescriptor<T, TValue> 
		: FieldNameQueryDescriptorBase<TermsQueryDescriptor<T, TValue>, ITermsQuery, T>
		, ITermsQuery where T : class
	{
		protected override bool Conditionless => TermsQuery.IsConditionless(this);
		MinimumShouldMatch ITermsQuery.MinimumShouldMatch { get; set; }
		bool? ITermsQuery.DisableCoord { get; set; }
		IEnumerable<object> ITermsQuery.Terms { get; set; }
		IFieldLookup ITermsQuery.TermsLookup { get; set; }

		public TermsQueryDescriptor<T, TValue> TermsLookup<TOther>(Func<FieldLookupDescriptor<TOther>, IFieldLookup> selector)
			where TOther : class => Assign(a => a.TermsLookup = selector(new FieldLookupDescriptor<TOther>()));

		public TermsQueryDescriptor<T, TValue> MinimumShouldMatch(MinimumShouldMatch minMatch) => Assign(a => a.MinimumShouldMatch = minMatch);

		public TermsQueryDescriptor<T, TValue> DisableCoord(bool? disable = true) => Assign(a => a.DisableCoord = disable);

		public TermsQueryDescriptor<T, TValue> Terms(IEnumerable<TValue> terms) => Assign(a => a.Terms = terms.Cast<object>());

		public TermsQueryDescriptor<T, TValue> Terms(params TValue[] terms) => Assign(a => a.Terms = terms.Cast<object>());

	}
}
