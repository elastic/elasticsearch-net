// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Trims whitespace from field. This only works on leading and trailing whitespace
	/// </summary>
	[InterfaceDataContract]
	public interface ITrimProcessor : IProcessor
	{
		/// <summary>
		/// The string-valued field to trim whitespace from
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }

		/// <summary>
		/// The field to assign the trimmed value to, by default field is updated in-place
		/// </summary>
		[DataMember(Name = "target_field")]
		Field TargetField { get; set; }
	}

	/// <inheritdoc cref="ITrimProcessor" />
	public class TrimProcessor : ProcessorBase, ITrimProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }
		protected override string Name => "trim";
	}

	/// <inheritdoc cref="ITrimProcessor" />
	public class TrimProcessorDescriptor<T>
		: ProcessorDescriptorBase<TrimProcessorDescriptor<T>, ITrimProcessor>, ITrimProcessor
		where T : class
	{
		protected override string Name => "trim";

		Field ITrimProcessor.Field { get; set; }
		bool? ITrimProcessor.IgnoreMissing { get; set; }
		Field ITrimProcessor.TargetField { get; set; }

		/// <inheritdoc cref="ITrimProcessor.Field" />
		public TrimProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ITrimProcessor.Field" />
		public TrimProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ITrimProcessor.TargetField" />
		public TrimProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="ITrimProcessor.TargetField" />
		public TrimProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="ITrimProcessor.IgnoreMissing" />
		public TrimProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);
	}
}
