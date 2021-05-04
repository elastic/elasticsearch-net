// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Queries documents that contain terms matching a regular expression.
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<RegexpQuery, IRegexpQuery>))]
	public interface IRegexpQuery : IFieldNameQuery
	{
		/// <summary>
		/// Enables optional operators for the regular expression.
		/// </summary>
		[DataMember(Name = "flags")]
		string Flags { get; set; }

		/// <summary>
		/// Maximum number of automaton states required for the query. Default is 10000.
        /// <para />
		/// Elasticsearch uses Apache Lucene internally to parse regular expressions.
		/// Lucene converts each regular expression to a finite automaton containing a number of determinized states.
		/// <para />
		/// You can use this parameter to prevent that conversion from unintentionally consuming too
		/// many resources. You may need to increase this limit to run complex regular expressions.
		/// </summary>
		[DataMember(Name = "max_determinized_states")]
		int? MaximumDeterminizedStates { get; set; }

		/// <summary>
		/// Regular expression for terms you wish to find in the provided field
		/// </summary>
		[DataMember(Name = "value")]
		string Value { get; set; }

		/// <summary>
		/// Method used to rewrite the query.
		/// </summary>
		[DataMember(Name ="rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }
	}

	/// <inheritdoc cref="IRegexpQuery"/>
	public class RegexpQuery : FieldNameQueryBase, IRegexpQuery
	{
		/// <inheritdoc cref="IRegexpQuery.Flags"/>
		public string Flags { get; set; }
		/// <inheritdoc cref="IRegexpQuery.MaximumDeterminizedStates"/>
		public int? MaximumDeterminizedStates { get; set; }
		/// <inheritdoc cref="IRegexpQuery.Value"/>
		public string Value { get; set; }

		/// <inheritdoc cref="IRegexpQuery.Rewrite"/>
		public MultiTermQueryRewrite Rewrite { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Regexp = this;

		internal static bool IsConditionless(IRegexpQuery q) => q.Field.IsConditionless() || q.Value.IsNullOrEmpty();
	}

	/// <inheritdoc cref="IRegexpQuery"/>
	public class RegexpQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<RegexpQueryDescriptor<T>, IRegexpQuery, T>
			, IRegexpQuery where T : class
	{
		protected override bool Conditionless => RegexpQuery.IsConditionless(this);
		string IRegexpQuery.Flags { get; set; }
		int? IRegexpQuery.MaximumDeterminizedStates { get; set; }
		string IRegexpQuery.Value { get; set; }
		MultiTermQueryRewrite IRegexpQuery.Rewrite { get; set; }

		/// <inheritdoc cref="IRegexpQuery.MaximumDeterminizedStates"/>
		public RegexpQueryDescriptor<T> MaximumDeterminizedStates(int? maxDeterminizedStates) =>
			Assign(maxDeterminizedStates, (a, v) => a.MaximumDeterminizedStates = v);

		/// <inheritdoc cref="IRegexpQuery.Value"/>
		public RegexpQueryDescriptor<T> Value(string regex) => Assign(regex, (a, v) => a.Value = v);

		/// <inheritdoc cref="IRegexpQuery.Flags"/>
		public RegexpQueryDescriptor<T> Flags(string flags) => Assign(flags, (a, v) => a.Flags = v);

		/// <inheritdoc cref="IRegexpQuery.Rewrite"/>
		public RegexpQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite) =>
			Assign(rewrite, (a, v) => a.Rewrite = v);
	}
}
