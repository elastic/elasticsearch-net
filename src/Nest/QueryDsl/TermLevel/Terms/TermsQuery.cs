using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest.Resolvers.Converters.Queries;
using Newtonsoft.Json;
using System.Linq.Expressions;

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
		IExternalFieldDeclaration ExternalField { get; set; }
		double? Boost { get; set; }
	}

	public class TermsQuery : PlainQuery, ITermsQuery
	{
		public string Name { get; set; }
		bool IQuery.Conditionless => IsConditionless(this);
		public PropertyPathMarker Field { get; set; }
		public string MinimumShouldMatch { get; set; }
		public bool? DisableCoord { get; set; }
		public IEnumerable<object> Terms { get; set; }
		public IExternalFieldDeclaration ExternalField { get; set; }
		public double? Boost { get; set; }

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
	public class TermsQueryDescriptor<T, K> : ITermsQuery where T : class
	{
		private ITermsQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => TermsQuery.IsConditionless(this);
		PropertyPathMarker ITermsQuery.Field { get; set; }
		string ITermsQuery.MinimumShouldMatch { get; set; }
		bool? ITermsQuery.DisableCoord { get; set; }
		IEnumerable<object> ITermsQuery.Terms { get; set; }
		IExternalFieldDeclaration ITermsQuery.ExternalField { get; set; }
		double? ITermsQuery.Boost { get; set; }

		public TermsQueryDescriptor<T, K> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public TermsQueryDescriptor<T, K> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}

		public TermsQueryDescriptor<T, K> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public TermsQueryDescriptor<T, K> OnField(PropertyPathMarker field)
		{
			Self.Field = field;
			return this;
		}

		public TermsQueryDescriptor<T, K> OnField(Expression<Func<T, K>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public TermsQueryDescriptor<T, K> OnExternalField<TOther>(
			Func<ExternalFieldDeclarationDescriptor<TOther>, ExternalFieldDeclarationDescriptor<TOther>> externalFieldSelector
			)
			where TOther : class
		{
			externalFieldSelector.ThrowIfNull("externalFieldSelector");
			var descriptor = externalFieldSelector(new ExternalFieldDeclarationDescriptor<TOther>());
			Self.ExternalField = descriptor;
			return this;
		}

		public TermsQueryDescriptor<T, K> MinimumShouldMatch(string minMatch)
		{
			Self.MinimumShouldMatch = minMatch;
			return this;
		}

		public TermsQueryDescriptor<T, K> MinimumShouldMatch(int minMatch)
		{
			Self.MinimumShouldMatch = minMatch.ToString(CultureInfo.InvariantCulture);
			return this;
		}

		public TermsQueryDescriptor<T, K> DisableCoord()
		{
			Self.DisableCoord = true;
			return this;
		}

		public TermsQueryDescriptor<T, K> Terms(IEnumerable<string> terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => !t.IsNullOrEmpty());

			Self.Terms = terms;
			return this;
		}

		public TermsQueryDescriptor<T, K> Terms(IEnumerable<K> terms)
		{
			if (terms.HasAny())
				terms = terms.Where(t => t != null).ToArray();

			Self.Terms = terms.Cast<object>();
			return this;
		}
	}
}
