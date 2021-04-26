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
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(SortFormatter))]
	public interface ISort
	{
		/// <summary>
		/// A format to apply to the sort value.
		/// </summary>
		[DataMember(Name ="format")]
		string Format { get; set; }

		/// <summary>
		/// Specifies how documents which are missing the sort field should
		/// be treated.
		/// </summary>
		[DataMember(Name ="missing")]
		object Missing { get; set; }

		/// <summary>
		/// Controls what collection value is picked for sorting a document
		/// when the field is a collection
		/// </summary>
		[DataMember(Name ="mode")]
		SortMode? Mode { get; set; }

		/// <summary>
		/// Set a single resolution for the sort
		/// </summary>
		[DataMember(Name ="numeric_type")]
		NumericType? NumericType { get; set; }

		/// <summary>
		/// Specifies the path and filter to apply when sorting on a nested field
		/// </summary>
		[DataMember(Name ="nested")]
		INestedSort Nested { get; set; }

		/// <summary>
		/// Controls the order of sorting
		/// </summary>
		[DataMember(Name ="order")]
		SortOrder? Order { get; set; }

		/// <summary>
		/// The field on which to sort
		/// </summary>
		[IgnoreDataMember]
		Field SortKey { get; }
	}

	public abstract class SortBase : ISort
	{
		/// <inheritdoc />
		public string Format { get; set; }

		/// <inheritdoc />
		public object Missing { get; set; }

		/// <inheritdoc />
		public SortMode? Mode { get; set; }

		/// <inheritdoc />
		public NumericType? NumericType { get; set; }

		/// <inheritdoc />
		public INestedSort Nested { get; set; }

		/// <inheritdoc />
		public SortOrder? Order { get; set; }

		/// <summary>
		/// The field on which to sort
		/// </summary>
		protected abstract Field SortKey { get; }

		/// <inheritdoc />
		Field ISort.SortKey => SortKey;
	}

	public abstract class SortDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISort
		where T : class
		where TDescriptor : SortDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISort
		where TInterface : class, ISort
	{
		/// <summary>
		/// The field on which to sort
		/// </summary>
		protected abstract Field SortKey { get; }

		string ISort.Format { get; set; }
		object ISort.Missing { get; set; }
		SortMode? ISort.Mode { get; set; }
		NumericType? ISort.NumericType { get; set; }
		INestedSort ISort.Nested { get; set; }
		SortOrder? ISort.Order { get; set; }
		Field ISort.SortKey => SortKey;

		/// <summary>
		/// Sorts by ascending sort order
		/// </summary>
		public virtual TDescriptor Ascending() => Assign(SortOrder.Ascending, (a, v) => a.Order = v);

		/// <summary>
		/// Sorts by descending sort order
		/// </summary>
		public virtual TDescriptor Descending() => Assign(SortOrder.Descending, (a, v) => a.Order = v);
		
		/// <inheritdoc cref="ISort.Format" />
		public virtual TDescriptor Format(string format) => Assign(format, (a, v) => a.Format = v);

		/// <inheritdoc cref="ISort.Order" />
		public virtual TDescriptor Order(SortOrder? order) => Assign(order, (a, v) => a.Order = v);

		/// <inheritdoc cref="ISort.NumericType" />
		public virtual TDescriptor NumericType(NumericType? numericType) => Assign(numericType, (a, v) => a.NumericType = v);

		/// <inheritdoc cref="ISort.Mode" />
		public virtual TDescriptor Mode(SortMode? mode) => Assign(mode, (a, v) => a.Mode = v);

		/// <summary>
		/// Specifies that documents which are missing the sort field should be ordered last
		/// </summary>
		public virtual TDescriptor MissingLast() => Assign("_last", (a, v) => a.Missing = v);

		/// <summary>
		/// Specifies that documents which are missing the sort field should be ordered first
		/// </summary>
		public virtual TDescriptor MissingFirst() => Assign("_first", (a, v) => a.Missing = v);

		/// <inheritdoc cref="ISort.Missing" />
		public virtual TDescriptor Missing(object value) => Assign(value, (a, v) => a.Missing = v);

		/// <inheritdoc cref="ISort.Nested" />
		public virtual TDescriptor Nested(Func<NestedSortDescriptor<T>, INestedSort> selector) =>
			Assign(selector, (a, v) => a.Nested = v?.Invoke(new NestedSortDescriptor<T>()));
	}
}
