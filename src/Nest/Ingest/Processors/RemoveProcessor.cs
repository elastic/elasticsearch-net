// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Removes existing fields. If one field doesn't exist, an exception will be thrown.
	/// </summary>
	[InterfaceDataContract]
	public interface IRemoveProcessor : IProcessor
	{
		/// <summary>
		/// fields to be removed. Supports template snippets.
		/// </summary>
		[DataMember(Name ="field")]
		Fields Field { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Nest.Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[DataMember(Name ="ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IRemoveProcessor" />
	public class RemoveProcessor : ProcessorBase, IRemoveProcessor
	{
		/// <inheritdoc />
		public Fields Field { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		protected override string Name => "remove";
	}

	/// <inheritdoc cref="IRemoveProcessor" />
	public class RemoveProcessorDescriptor<T>
		: ProcessorDescriptorBase<RemoveProcessorDescriptor<T>, IRemoveProcessor>, IRemoveProcessor
		where T : class
	{
		protected override string Name => "remove";

		Fields IRemoveProcessor.Field { get; set; }
		bool? IRemoveProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc cref="IRemoveProcessor.Field" />
		public RemoveProcessorDescriptor<T> Field(Fields fields) => Assign(fields, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRemoveProcessor.Field" />
		public RemoveProcessorDescriptor<T> Field(Func<FieldsDescriptor<T>, IPromise<Fields>> selector) =>
			Assign(selector, (a, v) => a.Field = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="IRemoveProcessor.IgnoreMissing" />
		public RemoveProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);
	}
}
