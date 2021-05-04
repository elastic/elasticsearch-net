// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A values source that is equivalent to a simple terms aggregation.
	/// The values are extracted from a field or a script exactly like the terms aggregation.
	/// </summary>
	public interface ITermsCompositeAggregationSource : ICompositeAggregationSource
	{
		/// <summary>
		/// A script to create the values for the composite buckets
		/// </summary>
		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc cref="ITermsCompositeAggregationSource" />
	public class TermsCompositeAggregationSource : CompositeAggregationSourceBase, ITermsCompositeAggregationSource
	{
		public TermsCompositeAggregationSource(string name) : base(name) { }

		/// <inheritdoc />
		public IScript Script { get; set; }

		/// <inheritdoc />
		protected override string SourceType => "terms";
	}

	/// <inheritdoc cref="ITermsCompositeAggregationSource" />
	public class TermsCompositeAggregationSourceDescriptor<T>
		: CompositeAggregationSourceDescriptorBase<TermsCompositeAggregationSourceDescriptor<T>, ITermsCompositeAggregationSource, T>,
			ITermsCompositeAggregationSource
	{
		public TermsCompositeAggregationSourceDescriptor(string name) : base(name, "terms") { }

		IScript ITermsCompositeAggregationSource.Script { get; set; }

		/// <inheritdoc cref="ITermsCompositeAggregationSource.Script" />
		public TermsCompositeAggregationSourceDescriptor<T> Script(Func<ScriptDescriptor, IScript> selector) =>
			Assign(selector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
