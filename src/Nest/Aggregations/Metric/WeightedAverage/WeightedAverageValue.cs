// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The configuration for a field or script that provides a value or weight
	/// for <see cref="WeightedAverageAggregation" />
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(WeightedAverageValue))]
	public interface IWeightedAverageValue
	{
		/// <summary>
		/// The field that values should be extracted from
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		///  defines how documents that are missing a value should be treated.
		/// The default behavior is different for value and weight:
		/// By default, if the value field is missing the document is ignored and the aggregation
		/// moves on to the next document.
		/// If the weight field is missing, it is assumed to have a weight of 1 (like a normal average).
		/// </summary>
		[DataMember(Name ="missing")]
		double? Missing { get; set; }

		/// <summary>
		/// A script to derive the value and the weight from
		/// </summary>
		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc />
	public class WeightedAverageValue : IWeightedAverageValue
	{
		internal WeightedAverageValue() { }

		public WeightedAverageValue(Field field) => Field = field;

		public WeightedAverageValue(IScript script) => Script = script;

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public double? Missing { get; set; }

		/// <inheritdoc />
		public IScript Script { get; set; }
	}

	/// <inheritdoc cref="IWeightedAverageAggregation" />
	public class WeightedAverageValueDescriptor<T>
		: DescriptorBase<WeightedAverageValueDescriptor<T>, IWeightedAverageValue>
			, IWeightedAverageValue
		where T : class
	{
		Field IWeightedAverageValue.Field { get; set; }
		double? IWeightedAverageValue.Missing { get; set; }
		IScript IWeightedAverageValue.Script { get; set; }

		/// <inheritdoc cref="IWeightedAverageValue.Field" />
		public WeightedAverageValueDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IWeightedAverageValue.Field" />
		public WeightedAverageValueDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IWeightedAverageValue.Script" />
		public virtual WeightedAverageValueDescriptor<T> Script(string script) => Assign(new InlineScript(script), (a, v) => a.Script = v);

		/// <inheritdoc cref="IWeightedAverageValue.Script" />
		public virtual WeightedAverageValueDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		/// <inheritdoc cref="IWeightedAverageValue.Missing" />
		public WeightedAverageValueDescriptor<T> Missing(double? missing) => Assign(missing, (a, v) => a.Missing = v);
	}

	/// <summary>
	/// The type of value
	/// </summary>
	[StringEnum]
	public enum ValueType
	{
		/// <summary>A string value</summary>
		[EnumMember(Value = "string")] String,

		/// <summary>A long value that can be used to represent byte, short, integer and long</summary>
		[EnumMember(Value = "long")] Long,

		/// <summary>A double value that can be used to represent float and double</summary>
		[EnumMember(Value = "double")] Double,

		/// <summary>A number value</summary>
		[EnumMember(Value = "number")] Number,

		/// <summary>A date value</summary>
		[EnumMember(Value = "date")] Date,

		/// <summary>A date nanos value</summary>
		[EnumMember(Value = "date_nanos")] DateNanos,

		/// <summary>An IP value</summary>
		[EnumMember(Value = "ip")] Ip,

		/// <summary>A numeric value</summary>
		[EnumMember(Value = "numeric")] Numeric,

		/// <summary>A geo_point value</summary>
		[EnumMember(Value = "geo_point")] GeoPoint,

		/// <summary>A boolean value</summary>
		[EnumMember(Value = "boolean")] Boolean,
	}
}
