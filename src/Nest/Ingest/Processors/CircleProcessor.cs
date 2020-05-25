// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[StringEnum]
	public enum ShapeType
	{
		[EnumMember(Value = "geo_shape")]
		GeoShape,

		[EnumMember(Value = "shape")]
		Shape
	}

	[InterfaceDataContract]
	public interface ICircleProcessor : IProcessor
	{
		/// <summary>
		/// The string-valued field to trim whitespace from.
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The field to assign the polygon shape to, by default field is updated in-place.
		/// </summary>
		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// If true and field does not exist, the processor quietly exits without modifying the document.
		/// </summary>
		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }

		/// <summary>
		///  The difference between the resulting inscribed distance from center to side and the circle’s radius
		/// (measured in meters for geo_shape, unit-less for shape)
		/// </summary>
		[DataMember(Name = "error_distance")]
		double? ErrorDistance { get; set; }

		/// <summary>
		/// Which field mapping type is to be used when processing the circle.
		/// </summary>
		[DataMember(Name = "shape_type")]
		ShapeType? ShapeType { get; set; }
	}

	public class CircleProcessor : ProcessorBase, ICircleProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc />
		public double? ErrorDistance { get; set; }

		/// <inheritdoc />
		public ShapeType? ShapeType { get; set; }

		/// <inheritdoc />
		protected override string Name => "circle";
	}

	public class CircleProcessorDescriptor<T>
		: ProcessorDescriptorBase<CircleProcessorDescriptor<T>, ICircleProcessor>, ICircleProcessor
		where T : class
	{
		protected override string Name => "circle";

		Field ICircleProcessor.Field { get; set; }
		Field ICircleProcessor.TargetField { get; set; }
		bool? ICircleProcessor.IgnoreMissing { get; set; }
		double? ICircleProcessor.ErrorDistance { get; set; }
		ShapeType? ICircleProcessor.ShapeType { get; set; }

		/// <inheritdoc cref="ICircleProcessor.Field" />
		public CircleProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ICircleProcessor.Field" />
		public CircleProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ICircleProcessor.TargetField" />
		public CircleProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="ICircleProcessor.TargetField" />
		public CircleProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="ICircleProcessor.IgnoreMissing">
		public CircleProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) =>
			Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);

		/// <inheritdoc cref="ICircleProcessor.IgnoreMissing">
		public CircleProcessorDescriptor<T> ErrorDistance(double? errorDistance) =>
			Assign(errorDistance, (a, v) => a.ErrorDistance = v);

		/// <inheritdoc cref="ICircleProcessor.ShapeType">
		public CircleProcessorDescriptor<T> ShapeType(ShapeType? shapeType) =>
			Assign(shapeType, (a, v) => a.ShapeType = v);
	}
}
