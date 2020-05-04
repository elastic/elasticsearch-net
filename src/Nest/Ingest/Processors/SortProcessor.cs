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
