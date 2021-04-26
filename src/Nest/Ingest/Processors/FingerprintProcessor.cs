/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
