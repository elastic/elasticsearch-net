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
	/// Expands a field with dots into an object field.
	/// This processor allows fields with dots in the name to be accessible by other processors in the pipeline.
	/// Otherwise these fields can’t be accessed by any processor.
	/// </summary>
	[InterfaceDataContract]
	public interface IDotExpanderProcessor : IProcessor
	{
		/// <summary>
		/// The field to expand into an object field
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The field that contains the field to expand.
		/// Only required if the field to expand is part another object field,
		/// because the field option can only understand leaf fields.
		/// </summary>
		[DataMember(Name ="path")]
		string Path { get; set; }
	}

	/// <inheritdoc cref="IDotExpanderProcessor" />
	public class DotExpanderProcessor : ProcessorBase, IDotExpanderProcessor
	{
		/// <summary>
		/// The field to expand into an object field
		/// </summary>
		[DataMember(Name ="field")]
		public Field Field { get; set; }

		/// <summary>
		/// The field that contains the field to expand.
		/// Only required if the field to expand is part another object field,
		/// because the field option can only understand leaf fields.
		/// </summary>
		[DataMember(Name ="path")]
		public string Path { get; set; }

		protected override string Name => "dot_expander";
	}

	/// <inheritdoc cref="IDotExpanderProcessor" />
	public class DotExpanderProcessorDescriptor<T>
		: ProcessorDescriptorBase<DotExpanderProcessorDescriptor<T>, IDotExpanderProcessor>, IDotExpanderProcessor
		where T : class
	{
		protected override string Name => "dot_expander";

		Field IDotExpanderProcessor.Field { get; set; }
		string IDotExpanderProcessor.Path { get; set; }

		/// <inheritdoc cref="IDotExpanderProcessor.Field" />
		public DotExpanderProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <summary>
		/// The field to expand into an object field
		/// </summary>
		public DotExpanderProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IDotExpanderProcessor.Path" />
		public DotExpanderProcessorDescriptor<T> Path(string path) => Assign(path, (a, v) => a.Path = v);
	}
}
