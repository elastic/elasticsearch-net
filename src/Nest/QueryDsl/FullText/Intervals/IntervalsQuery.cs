using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A query that allows fine-grained control over the order and proximity of matching terms.
	/// Matching rules are constructed from a small set of definitions, and the rules are then applied to terms from a
	/// particular field.
	/// The definitions produce sequences of minimal intervals that span terms in a body of text.
	/// These intervals can be further combined and filtered by parent sources.
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<IntervalsQuery, IIntervalsQuery>))]
	public interface IIntervalsQuery : IFieldNameQuery, IIntervalsContainer { }

	/// <inheritdoc cref="IIntervalsQuery" />
	public class IntervalsQuery : FieldNameQueryBase, IIntervalsQuery
	{
		/// <inheritdoc cref="IIntervalsAllOf"/>
		public IIntervalsAllOf AllOf { get; set; }
		/// <inheritdoc cref="IIntervalsAnyOf"/>
		public IIntervalsAnyOf AnyOf { get; set; }
		/// <inheritdoc cref="IIntervalsMatch"/>
		public IIntervalsMatch Match { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal static bool IsConditionless(IIntervalsQuery q) =>
			q.Field.IsConditionless() || q.Match == null && q.AllOf == null && q.AnyOf == null;

		internal override void InternalWrapInContainer(IQueryContainer container) => container.Intervals = this;
	}

	/// <inheritdoc cref="IIntervalsQuery" />
	[DataContract]
	public class IntervalsQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<IntervalsQueryDescriptor<T>, IIntervalsQuery, T>
			, IIntervalsQuery where T : class
	{
		protected override bool Conditionless => IntervalsQuery.IsConditionless(this);

		IIntervalsAllOf IIntervalsContainer.AllOf { get; set; }
		IIntervalsAnyOf IIntervalsContainer.AnyOf { get; set; }
		IIntervalsMatch IIntervalsContainer.Match { get; set; }

		/// <inheritdoc cref="IntervalsQuery.Match" />
		public IntervalsQueryDescriptor<T> Match(Func<IntervalsMatchDescriptor, IIntervalsMatch> selector) =>
			Assign(selector, (a, v) => a.Match = v?.Invoke(new IntervalsMatchDescriptor()));

		/// <inheritdoc cref="IntervalsQuery.AnyOf" />
		public IntervalsQueryDescriptor<T> AnyOf(Func<IntervalsAnyOfDescriptor, IIntervalsAnyOf> selector) =>
			Assign(selector, (a, v) => a.AnyOf = v?.Invoke(new IntervalsAnyOfDescriptor()));

		/// <inheritdoc cref="IntervalsQuery.AllOf" />
		public IntervalsQueryDescriptor<T> AllOf(Func<IntervalsAllOfDescriptor, IIntervalsAllOf> selector) =>
			Assign(selector, (a, v) => a.AllOf = v?.Invoke(new IntervalsAllOfDescriptor()));
	}

	/// <summary>
	/// Container for an <see cref="IIntervalsQuery" /> rule
	/// </summary>
	public interface IIntervalsContainer
	{
		/// <inheritdoc cref="IIntervalsAllOf" />
		[DataMember(Name = "all_of")]
		IIntervalsAllOf AllOf { get; set; }

		/// <inheritdoc cref="IIntervalsAnyOf" />
		[DataMember(Name = "any_of")]
		IIntervalsAnyOf AnyOf { get; set; }

		/// <inheritdoc cref="IIntervalsMatch" />
		[DataMember(Name = "match")]
		IIntervalsMatch Match { get; set; }
	}

	/// <inheritdoc cref="IIntervalsContainer" />
	public class IntervalsContainer : IIntervalsContainer, IDescriptor
	{
		public IntervalsContainer() { }

		public IntervalsContainer(IntervalsBase intervals)
		{
			intervals.ThrowIfNull(nameof(intervals));
			intervals.WrapInContainer(this);
		}

		IIntervalsAllOf IIntervalsContainer.AllOf { get; set; }
		IIntervalsAnyOf IIntervalsContainer.AnyOf { get; set; }
		IIntervalsMatch IIntervalsContainer.Match { get; set; }

		public static implicit operator IntervalsContainer(IntervalsBase intervals) => intervals == null
			? null
			: new IntervalsContainer(intervals);
	}

	/// <summary>
	/// Descriptor to construct an <see cref="IIntervalsQuery" /> rule
	/// </summary>
	public class IntervalsDescriptor : IntervalsContainer
	{
		private IntervalsDescriptor Assign<TValue>(TValue value, Action<IIntervalsContainer, TValue> assigner) =>
			Fluent.Assign(this, value, assigner);

		/// <inheritdoc cref="IntervalsMatchDescriptor" />
		public IntervalsDescriptor Match(Func<IntervalsMatchDescriptor, IIntervalsMatch> selector) =>
			Assign(selector, (a, v) => a.Match = v?.Invoke(new IntervalsMatchDescriptor()));

		/// <inheritdoc cref="IntervalsAnyOfDescriptor" />
		public IntervalsDescriptor AnyOf(Func<IntervalsAnyOfDescriptor, IIntervalsAnyOf> selector) =>
			Assign(selector, (a, v) => a.AnyOf = v?.Invoke(new IntervalsAnyOfDescriptor()));

		/// <inheritdoc cref="IntervalsAllOfDescriptor" />
		public IntervalsDescriptor AllOf(Func<IntervalsAllOfDescriptor, IIntervalsAllOf> selector) =>
			Assign(selector, (a, v) => a.AllOf = v?.Invoke(new IntervalsAllOfDescriptor()));
	}

	/// <summary>
	/// An <see cref="IIntervalsQuery" /> rule
	/// </summary>
	public interface IIntervals
	{
		/// <summary>
		/// An optional interval filter
		/// </summary>
		[DataMember(Name = "filter")]
		IIntervalsFilter Filter { get; set; }
	}

	/// <summary>
	/// Base type for an <see cref="IIntervals" /> implementation
	/// </summary>
	public abstract class IntervalsBase : IIntervals
	{
		/// <inheritdoc />
		public IIntervalsFilter Filter { get; set; }

		internal abstract void WrapInContainer(IIntervalsContainer container);
	}

	/// <summary>
	/// Base type for descriptors that define <see cref="IIntervals" />
	/// </summary>
	public abstract class IntervalsDescriptorBase<TDescriptor, TInterface> : DescriptorBase<TDescriptor, TInterface>, IIntervals
		where TDescriptor : DescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class, IIntervals
	{
		IIntervalsFilter IIntervals.Filter { get; set; }

		/// <inheritdoc cref="IIntervalsFilter" />
		public TDescriptor Filter(Func<IntervalsFilterDescriptor, IIntervalsFilter> selector) =>
			Assign(selector, (a, v) => a.Filter = v?.Invoke(new IntervalsFilterDescriptor()));
	}

	/// <summary>
	/// Constructs a collection of <see cref="IntervalsContainer" />
	/// </summary>
	public class IntervalsListDescriptor : DescriptorPromiseBase<IntervalsListDescriptor, List<IntervalsContainer>>
	{
		public IntervalsListDescriptor() : base(new List<IntervalsContainer>()) { }

		/// <inheritdoc cref="IIntervalsMatch" />
		public IntervalsListDescriptor Match(Func<IntervalsMatchDescriptor, IIntervalsMatch> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(new IntervalsDescriptor().Match(v)));

		/// <inheritdoc cref="IIntervalsAnyOf" />
		public IntervalsListDescriptor AnyOf(Func<IntervalsAnyOfDescriptor, IIntervalsAnyOf> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(new IntervalsDescriptor().AnyOf(v)));

		/// <inheritdoc cref="IIntervalsAllOf" />
		public IntervalsListDescriptor AllOf(Func<IntervalsAllOfDescriptor, IIntervalsAllOf> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(new IntervalsDescriptor().AllOf(v)));
	}
}
