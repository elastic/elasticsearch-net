// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Processes elements in an array of unknown length.
	/// All processors can operate on elements inside an array, but if all elements of
	/// an array need to be processed in the same way, defining a processor for each
	/// element becomes cumbersome and tricky because it is likely that the number of
	/// elements in an array is unknown. For this reason the foreach processor exists.
	/// By specifying the field holding array elements and a processor that defines what
	/// should happen to each element, array fields can easily be preprocessed.
	/// </summary>
	[InterfaceDataContract]
	public interface IForeachProcessor : IProcessor
	{
		/// <summary>
		/// The array field
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The processor to execute against each field
		/// </summary>
		[DataMember(Name ="processor")]
		IProcessor Processor { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IForeachProcessor"/>
	public class ForeachProcessor : ProcessorBase, IForeachProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public IProcessor Processor { get; set; }
		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }
		protected override string Name => "foreach";
	}

	/// <inheritdoc cref="IForeachProcessor"/>
	public class ForeachProcessorDescriptor<T>
		: ProcessorDescriptorBase<ForeachProcessorDescriptor<T>, IForeachProcessor>, IForeachProcessor
		where T : class
	{
		protected override string Name => "foreach";

		Field IForeachProcessor.Field { get; set; }

		IProcessor IForeachProcessor.Processor { get; set; }
		bool? IForeachProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc cref="IForeachProcessor.Field"/>
		public ForeachProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IForeachProcessor.Field"/>
		public ForeachProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IForeachProcessor.Processor"/>
		public ForeachProcessorDescriptor<T> Processor(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(selector, (a, v) => a.Processor = v?.Invoke(new ProcessorsDescriptor())?.Value?.FirstOrDefault());

		/// <inheritdoc cref="IForeachProcessor.IgnoreMissing" />
		public ForeachProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);
	}
}
