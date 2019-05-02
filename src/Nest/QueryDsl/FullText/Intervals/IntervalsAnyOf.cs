using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A rule that emits intervals produced by any of its sub-rules.
	/// </summary>
	[ReadAs(typeof(IntervalsAnyOf))]
	public interface IIntervalsAnyOf : IIntervals
	{
		/// <summary>
		/// An array of rules to match.
		/// </summary>
		[DataMember(Name = "intervals")]
		IEnumerable<IntervalsContainer> Intervals { get; set; }
	}

	/// <inheritdoc cref="IIntervalsAnyOf" />
	public class IntervalsAnyOf : IntervalsBase, IIntervalsAnyOf
	{
		/// <inheritdoc />
		public IEnumerable<IntervalsContainer> Intervals { get; set; }

		internal override void WrapInContainer(IIntervalsContainer container) => container.AnyOf = this;
	}

	/// <inheritdoc cref="IIntervalsAnyOf" />
	public class IntervalsAnyOfDescriptor : IntervalsDescriptorBase<IntervalsAnyOfDescriptor, IIntervalsAnyOf>, IIntervalsAnyOf
	{
		IEnumerable<IntervalsContainer> IIntervalsAnyOf.Intervals { get; set; }

		/// <inheritdoc cref="IIntervalsAnyOf.Intervals" />
		public IntervalsAnyOfDescriptor Intervals(Func<IntervalsListDescriptor, IPromise<List<IntervalsContainer>>> selector) =>
			Assign(selector, (a, v) => a.Intervals = v.InvokeOrDefault(new IntervalsListDescriptor())?.Value);
	}
}
