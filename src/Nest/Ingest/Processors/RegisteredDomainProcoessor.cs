// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRegisteredDomainProcessor : IProcessor
	{
		[DataMember(Name = "field")]
		Field Field { get; set; }

		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }
		
		[DataMember(Name = "target_field")]
		Field TargetField { get; set; }
	}

	public class RegisteredDomainProcessor : ProcessorBase, IRegisteredDomainProcessor
	{
		protected override string Name => "registered_domain";

		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }
		/// <inheritdoc />
		public Field TargetField { get; set; }
	}

	/// <inheritdoc cref="IFingerprintProcessor" />
	public class RegisteredDomainProcessorDescriptor<T>
		: ProcessorDescriptorBase<RegisteredDomainProcessorDescriptor<T>, IRegisteredDomainProcessor>, IRegisteredDomainProcessor
		where T : class
	{
		protected override string Name => "registered_domain";

		Field IRegisteredDomainProcessor.Field { get; set; }
		bool? IRegisteredDomainProcessor.IgnoreMissing { get; set; }
		Field IRegisteredDomainProcessor.TargetField { get; set; }

		/// <inheritdoc cref="IRegisteredDomainProcessor.Field" />
		public RegisteredDomainProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRegisteredDomainProcessor.Field" />
		public RegisteredDomainProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRegisteredDomainProcessor.IgnoreMissing" />
		public RegisteredDomainProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);

		/// <inheritdoc cref="IRegisteredDomainProcessor.TargetField" />
		public RegisteredDomainProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IRegisteredDomainProcessor.TargetField" />
		public RegisteredDomainProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);
	}
}
