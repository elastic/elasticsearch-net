// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A rule that returns matches that span a combination of other rules.
	/// </summary>
	[ReadAs(typeof(IntervalsAllOf))]
	public interface IIntervalsAllOf : IIntervals
	{
		/// <summary>
		/// An array of rules to combine. All rules must produce a match in a document for the overall source to match.
		/// </summary>
		[DataMember(Name = "intervals")]
		IEnumerable<IntervalsContainer> Intervals { get; set; }

		/// <summary>
		/// Specify a maximum number of gaps between the terms in the text. Terms that appear further apart than this will not
		/// match.
		/// If unspecified, or set to -1, then there is no width restriction on the match.
		/// If set to 0 then the terms must appear next to each other.
		/// </summary>
		[DataMember(Name = "max_gaps")]
		int? MaxGaps { get; set; }

		/// <summary>
		/// Whether or not the terms must appear in their specified order. Defaults to <c>false</c>
		/// </summary>
		[DataMember(Name = "ordered")]
		bool? Ordered { get; set; }
	}

	/// <inheritdoc cref="IIntervalsAllOf" />
	public class IntervalsAllOf : IntervalsBase, IIntervalsAllOf
	{
		/// <inheritdoc />
		public IEnumerable<IntervalsContainer> Intervals { get; set; }

		/// <inheritdoc />
		public int? MaxGaps { get; set; }

		/// <inheritdoc />
		public bool? Ordered { get; set; }

		internal override void WrapInContainer(IIntervalsContainer container) => container.AllOf = this;
	}

	/// <inheritdoc cref="IIntervalsAllOf" />
	public class IntervalsAllOfDescriptor : IntervalsDescriptorBase<IntervalsAllOfDescriptor, IIntervalsAllOf>, IIntervalsAllOf
	{
		IEnumerable<IntervalsContainer> IIntervalsAllOf.Intervals { get; set; }
		int? IIntervalsAllOf.MaxGaps { get; set; }
		bool? IIntervalsAllOf.Ordered { get; set; }

		/// <inheritdoc cref="IIntervalsAllOf.MaxGaps" />
		public IntervalsAllOfDescriptor MaxGaps(int? maxGaps) => Assign(maxGaps, (a, v) => a.MaxGaps = v);

		/// <inheritdoc cref="IIntervalsAllOf.Ordered" />
		public IntervalsAllOfDescriptor Ordered(bool? ordered = true) => Assign(ordered, (a, v) => a.Ordered = v);

		/// <inheritdoc cref="IIntervalsAllOf.Intervals" />
		public IntervalsAllOfDescriptor Intervals(Func<IntervalsListDescriptor, IPromise<List<IntervalsContainer>>> selector) =>
			Assign(selector, (a, v) => a.Intervals = v.InvokeOrDefault(new IntervalsListDescriptor())?.Value);
	}
}
