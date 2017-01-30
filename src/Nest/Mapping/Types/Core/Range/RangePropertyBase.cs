using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(PropertyJsonConverter))]
	public interface IRangeProperty : IDocValuesProperty
	{
		/// <summary>
		/// Try to convert strings to numbers and truncate fractions for integers. Accepts true (default) and false.
		/// </summary>
		[JsonProperty("coerce")]
		bool? Coerce { get; set; }

		/// <summary>
		/// Mapping field-level query time boosting. Accepts a floating point number, defaults to 1.0.
		/// </summary>
		[JsonProperty("boost")]
		double? Boost { get; set; }

		/// <summary>
		/// Whether or not the field value should be included in the _all field? Accepts true or false. Defaults
		/// to false if index is set to false, or if a parent object field sets include_in_all to false.
		/// Otherwise defaults to true.
		/// </summary>
		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }

		/// <summary>
		/// Should the field be searchable? Accepts true (default) and false.
		/// </summary>
		[JsonProperty("index")]
		bool? Index { get; set; }
	}

	public abstract class RangePropertyBase : DocValuesPropertyBase, IRangeProperty
	{
		protected RangePropertyBase(RangeType rangeType) : base(rangeType.ToFieldType()) { }

		/// <inheritdoc/>
		public bool? Coerce { get; set; }
		/// <inheritdoc/>
		public double? Boost { get; set; }
		/// <inheritdoc/>
		public bool? IncludeInAll { get; set; }
		/// <inheritdoc/>
		public bool? Index { get; set; }
	}

	public abstract class RangePropertyDescriptorBase<TDescriptor, TInterface, T>
		: DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>, IRangeProperty
		where TDescriptor : RangePropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IRangeProperty
		where T : class
	{
		bool? IRangeProperty.Coerce { get; set; }
		double? IRangeProperty.Boost { get; set; }
		bool? IRangeProperty.IncludeInAll { get; set; }
		bool? IRangeProperty.Index { get; set; }

		protected RangePropertyDescriptorBase(RangeType type) : base(type.ToFieldType()) { }

		/// <inheritdoc/>
		public TDescriptor Coerce(bool coerce = true) => Assign(a => a.Coerce = coerce);
		/// <inheritdoc/>
		public TDescriptor Boost(double boost) => Assign(a => a.Boost = boost);
		/// <inheritdoc/>
		public TDescriptor IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);
		/// <inheritdoc/>
		public TDescriptor Index(bool index = true) => Assign(a => a.Index = index);
	}
}
