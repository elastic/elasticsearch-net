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
	/// URL-decodes a string
	/// </summary>
	[InterfaceDataContract]
	public interface IUrlDecodeProcessor : IProcessor
	{
		/// <summary>
		/// The field to decode
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[DataMember(Name ="ignore_missing")]
		bool? IgnoreMissing { get; set; }

		/// <summary>
		/// The field to assign the converted value to, by default <see cref="Field" /> is updated in-place
		/// </summary>
		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }
	}

	/// <inheritdoc cref="IUrlDecodeProcessor" />
	public class UrlDecodeProcessor : ProcessorBase, IUrlDecodeProcessor
	{
		/// <inheritdoc />
		[DataMember(Name ="field")]
		public Field Field { get; set; }

		/// <inheritdoc />
		[DataMember(Name ="ignore_missing")]
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc />
		[DataMember(Name ="target_field")]
		public Field TargetField { get; set; }

		protected override string Name => "urldecode";
	}

	/// <inheritdoc cref="IUrlDecodeProcessor" />
	public class UrlDecodeProcessorDescriptor<T>
		: ProcessorDescriptorBase<UrlDecodeProcessorDescriptor<T>, IUrlDecodeProcessor>, IUrlDecodeProcessor
		where T : class
	{
		protected override string Name => "urldecode";

		Field IUrlDecodeProcessor.Field { get; set; }
		bool? IUrlDecodeProcessor.IgnoreMissing { get; set; }
		Field IUrlDecodeProcessor.TargetField { get; set; }

		/// <inheritdoc cref="IUrlDecodeProcessor.Field" />
		public UrlDecodeProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IUrlDecodeProcessor.Field" />
		public UrlDecodeProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IUrlDecodeProcessor.TargetField" />
		public UrlDecodeProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IUrlDecodeProcessor.TargetField" />
		public UrlDecodeProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IUrlDecodeProcessor.IgnoreMissing" />
		public UrlDecodeProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) =>
			Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);
	}
}
