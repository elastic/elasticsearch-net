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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Sorts the elements of an array ascending or descending. Homogeneous arrays of numbers
	/// will be sorted numerically, while arrays of strings or heterogeneous arrays
	///  of strings and numbers will be sorted lexicographically.
	/// </summary>
	[InterfaceDataContract]
	public interface ISortProcessor : IProcessor
	{
		/// <summary>
		/// The field to be sorted
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The sort order to use. Default is ascending.
		/// </summary>
		[DataMember(Name ="order")]
		SortOrder? Order { get; set; }

		/// <summary>
		/// The field to assign the sorted value to, by default field is updated in-place
		/// </summary>
		[DataMember(Name = "target_field")]
		Field TargetField { get; set; }
	}

	/// <inheritdoc cref="ISortProcessor" />
	public class SortProcessor : ProcessorBase, ISortProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public SortOrder? Order { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }
		protected override string Name => "sort";
	}

	/// <inheritdoc cref="ISortProcessor" />
	public class SortProcessorDescriptor<T>
		: ProcessorDescriptorBase<SortProcessorDescriptor<T>, ISortProcessor>, ISortProcessor
		where T : class
	{
		protected override string Name => "sort";

		Field ISortProcessor.Field { get; set; }
		SortOrder? ISortProcessor.Order { get; set; }
		Field ISortProcessor.TargetField { get; set; }

		/// <inheritdoc cref="ISortProcessor.Field" />
		public SortProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISortProcessor.Field" />
		public SortProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISortProcessor.TargetField" />
		public SortProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="ISortProcessor.TargetField" />
		public SortProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="ISortProcessor.Order" />
		public SortProcessorDescriptor<T> Order(SortOrder? order = SortOrder.Ascending) =>
			Assign(order, (a, v) => a.Order = v);
	}
}
