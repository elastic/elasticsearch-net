using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// Returns any documents that match with at least one or more of the provided terms.
	/// The terms are not analyzed and thus must match exactly.
	/// The number of terms that must match varies per document and is either controlled by a minimum should match
	/// field or computed per document in a minimum should match script.
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<TermsSetQuery, ITermsSetQuery>))]
	public interface ITermsSetQuery : IFieldNameQuery
	{
		/// <summary>
		/// A field containing the number of required terms that must match
		/// </summary>
		[DataMember(Name = "minimum_should_match_field")]
		Field MinimumShouldMatchField { get; set; }

		/// <summary>
		/// A script to control how many terms are required to match
		/// </summary>
		[DataMember(Name = "minimum_should_match_script")]
		IScript MinimumShouldMatchScript { get; set; }

		/// <summary>
		/// The required terms to match
		/// </summary>
		[DataMember(Name = "terms")]
		[JsonFormatter(typeof(SourceWriteFormatter<IEnumerable<object>>))]
		IEnumerable<object> Terms { get; set; }
	}

	/// <
	/// <inheritdoc cref="ITermsSetQuery" />
	public class TermsSetQuery : FieldNameQueryBase, ITermsSetQuery
	{
		/// <inheritdoc />
		public Field MinimumShouldMatchField { get; set; }

		/// <inheritdoc />
		public IScript MinimumShouldMatchScript { get; set; }

		/// <inheritdoc />
		public IEnumerable<object> Terms { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.TermsSet = this;

		internal static bool IsConditionless(ITermsSetQuery q) =>
			q.Field.IsConditionless() ||
			q.Terms == null ||
			!q.Terms.HasAny() ||
			q.Terms.All(t => t == null || ((t as string)?.IsNullOrEmpty()).GetValueOrDefault(false)) ||
			q.MinimumShouldMatchField.IsConditionless() && q.MinimumShouldMatchScript == null;
	}

	/// <summary>
	/// Returns any documents that match with at least one or more of the provided terms.
	/// The terms are not analyzed and thus must match exactly.
	/// The number of terms that must match varies per document and is either controlled by a minimum should match
	/// field or computed per document in a minimum should match script.
	/// </summary>
	/// <typeparam name="T">The type that represents the expected hit type</typeparam>
	public class TermsSetQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<TermsSetQueryDescriptor<T>, ITermsSetQuery, T>
			, ITermsSetQuery where T : class
	{
		protected override bool Conditionless => TermsSetQuery.IsConditionless(this);
		Field ITermsSetQuery.MinimumShouldMatchField { get; set; }
		IScript ITermsSetQuery.MinimumShouldMatchScript { get; set; }
		IEnumerable<object> ITermsSetQuery.Terms { get; set; }

		/// <summary>
		/// The required terms to match
		/// </summary>
		public TermsSetQueryDescriptor<T> Terms<TValue>(IEnumerable<TValue> terms) => Assign(a => a.Terms = terms?.Cast<object>());

		/// <summary>
		/// The required terms to match
		/// </summary>
		public TermsSetQueryDescriptor<T> Terms<TValue>(params TValue[] terms) => Assign(a =>
		{
			if (terms?.Length == 1 && typeof(IEnumerable).IsAssignableFrom(typeof(TValue)) && typeof(TValue) != typeof(string))
				a.Terms = (terms.First() as IEnumerable)?.Cast<object>();
			else a.Terms = terms?.Cast<object>();
		});

		/// <summary>
		/// A field containing the number of required terms that must match
		/// </summary>
		public TermsSetQueryDescriptor<T> MinimumShouldMatchField(Field field) => Assign(a => a.MinimumShouldMatchField = field);

		/// <summary>
		/// A field containing the number of required terms that must match
		/// </summary>
		public TermsSetQueryDescriptor<T> MinimumShouldMatchField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.MinimumShouldMatchField = objectPath);

		/// <summary>
		/// A script to control how many terms are required to match
		/// </summary>
		public TermsSetQueryDescriptor<T> MinimumShouldMatchScript(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.MinimumShouldMatchScript = scriptSelector?.Invoke(new ScriptDescriptor()));
	}
}
