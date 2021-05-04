// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IFingerprintProcessor : IProcessor
	{
		[DataMember(Name = "fields")]
		Fields Fields { get; set; }

		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }

		[DataMember(Name = "method")]
		string Method { get; set; }

		[DataMember(Name = "salt")]
		string Salt { get; set; }
		
		[DataMember(Name = "target_field")]
		Field TargetField { get; set; }
	}

	public class FingerprintProcessor : ProcessorBase, IFingerprintProcessor
	{
		protected override string Name => "fingerprint";

		/// <inheritdoc />
		public Fields Fields { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc />
		public string Method { get; set; }

		/// <inheritdoc />
		public string Salt { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }
	}

	/// <inheritdoc cref="IFingerprintProcessor" />
	public class FingerprintProcessorDescriptor<T>
		: ProcessorDescriptorBase<FingerprintProcessorDescriptor<T>, IFingerprintProcessor>, IFingerprintProcessor
		where T : class
	{
		protected override string Name => "fingerprint";

		Fields IFingerprintProcessor.Fields { get; set; }

		string IFingerprintProcessor.Method { get; set; }

		string IFingerprintProcessor.Salt { get; set; }

		Field IFingerprintProcessor.TargetField { get; set; }

		bool? IFingerprintProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc cref="IFingerprintProcessor.Fields" />
		public FingerprintProcessorDescriptor<T> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		/// <inheritdoc cref="IFingerprintProcessor.Fields" />
		public FingerprintProcessorDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> selector) =>
			Assign(selector, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="IFingerprintProcessor.IgnoreMissing" />
		public FingerprintProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);

		/// <inheritdoc cref="IFingerprintProcessor.Method" />
		public FingerprintProcessorDescriptor<T> Method(string method) => Assign(method, (a, v) => a.Method = v);

		/// <inheritdoc cref="IFingerprintProcessor.Salt" />
		public FingerprintProcessorDescriptor<T> Salt(string salt) => Assign(salt, (a, v) => a.Salt = v);

		/// <inheritdoc cref="IFingerprintProcessor.TargetField" />
		public FingerprintProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IFingerprintProcessor.TargetField" />
		public FingerprintProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);
	}
}
