using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<WeightedAverageAggregation>))]
	public interface IWeightedAverageAggregation : IAggregation
	{
		/// <summary> The configuration for the field or script that provides the values </summary>
		[JsonProperty("value")]
		IWeightedAverageValue Value { get; set; }
		/// <summary> The configuration for the field or script that provides the weights /// </summary>
		[JsonProperty("weight")]
		IWeightedAverageValue Weight { get; set; }
		/// <summary> The optional numeric response formatter </summary>
		[JsonProperty("format")]
		string Format { get; set; }
		/// <summary> A hint about the values for pure scripts or unmapped fields </summary>
		[JsonProperty("value_type")]
		// TODO map as on server enum ?
		// https://github.com/elastic/elasticsearch/blob/master/server/src/main/java/org/elasticsearch/search/aggregations/support/ValueType.java
		string ValueType { get; set; }
	}

	public class WeightedAverageAggregation : AggregationBase, IWeightedAverageAggregation
	{
		internal WeightedAverageAggregation() { }
		public WeightedAverageAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.WeightedAverage = this;

		/// <inheritdoc cref="IWeightedAverageAggregation.Value"/>>
		public IWeightedAverageValue Value { get; set; }
		/// <inheritdoc cref="IWeightedAverageAggregation.Weight"/>>
		public IWeightedAverageValue Weight { get; set; }
		/// <inheritdoc cref="IWeightedAverageAggregation.Format"/>>
		public string Format { get; set; }
		/// <inheritdoc cref="IWeightedAverageAggregation.ValueType"/>>
		public string ValueType { get; set; }
	}

	public class WeightedAverageAggregationDescriptor<T>
		: DescriptorBase<WeightedAverageAggregationDescriptor<T>, IWeightedAverageAggregation>
			, IWeightedAverageAggregation
		where T : class
	{
		IWeightedAverageValue IWeightedAverageAggregation.Value { get; set; }
		IWeightedAverageValue IWeightedAverageAggregation.Weight { get; set; }
		string IWeightedAverageAggregation.Format { get; set; }
		string IWeightedAverageAggregation.ValueType { get; set; }

		string IAggregation.Name { get; set; }
		IDictionary<string, object> IAggregation.Meta { get; set; }
		/// <inheritdoc cref="IAggregation.Meta"/>>
		public WeightedAverageAggregationDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Meta = selector?.Invoke(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IWeightedAverageAggregation.Value"/>>
		public WeightedAverageAggregationDescriptor<T> Value(Func<WeightedAverageValueDescriptor<T>, IWeightedAverageValue> selector) =>
			Assign(a => a.Value = selector?.Invoke(new WeightedAverageValueDescriptor<T>()));

		/// <inheritdoc cref="IWeightedAverageAggregation.Weight"/>>
		public WeightedAverageAggregationDescriptor<T> Weight(Func<WeightedAverageValueDescriptor<T>, IWeightedAverageValue> selector) =>
			Assign(a => a.Weight = selector?.Invoke(new WeightedAverageValueDescriptor<T>()));

		/// <inheritdoc cref="IWeightedAverageAggregation.Format"/>>
		public WeightedAverageAggregationDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		/// <inheritdoc cref="IWeightedAverageAggregation.ValueType"/>>
		public WeightedAverageAggregationDescriptor<T> ValueType(string valueType) => Assign(a => a.ValueType = valueType);


	}

}
