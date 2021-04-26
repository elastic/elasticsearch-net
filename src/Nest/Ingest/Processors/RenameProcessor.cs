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

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Renames an existing field. If the field doesn't exist or the new name is already used, an exception will be thrown.
	/// </summary>
	[InterfaceDataContract]
	public interface IRenameProcessor : IProcessor
	{
		/// <summary>
		/// The field to be renamed. Supports template snippets.
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The new name of the field. Supports template snippets.
		/// </summary>
		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IRenameProcessor" />
	public class RenameProcessor : ProcessorBase, IRenameProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		protected override string Name => "rename";
	}

	/// <inheritdoc cref="IRenameProcessor" />
	public class RenameProcessorDescriptor<T>
		: ProcessorDescriptorBase<RenameProcessorDescriptor<T>, IRenameProcessor>, IRenameProcessor
		where T : class
	{
		protected override string Name => "rename";
		Field IRenameProcessor.Field { get; set; }
		Field IRenameProcessor.TargetField { get; set; }
		bool? IRenameProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc cref="IRenameProcessor.Field" />
		public RenameProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRenameProcessor.Field" />
		public RenameProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRenameProcessor.TargetField" />
		public RenameProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IRenameProcessor.TargetField" />
		public RenameProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IRemoveProcessor.IgnoreMissing" />
		public RenameProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);
	}
}
