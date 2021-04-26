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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IFieldSort : ISort
	{
		[DataMember(Name ="ignore_unmapped")]
		bool? IgnoreUnmappedFields { get; set; }

		[DataMember(Name ="unmapped_type")]
		FieldType? UnmappedType { get; set; }
	}

	public class FieldSort : SortBase, IFieldSort
	{
		private const string ShardDoc = "_shard_doc";
		
		public static readonly IList<ISort> ByDocumentOrder = new ReadOnlyCollection<ISort>(new List<ISort> { new FieldSort { Field = "_doc" } });
		public static readonly IList<ISort> ByShardDocumentOrder = new ReadOnlyCollection<ISort>(new List<ISort> { new FieldSort { Field = ShardDoc } });

		public static readonly FieldSort ShardDocumentOrderAscending = new() { Field = ShardDoc, Order = SortOrder.Ascending };
		public static readonly FieldSort ShardDocumentOrderDescending = new() { Field = ShardDoc, Order = SortOrder.Descending };

		public Field Field { get; set; }
		public bool? IgnoreUnmappedFields { get; set; }
		public FieldType? UnmappedType { get; set; }
		protected override Field SortKey => Field;
	}

	public class FieldSortDescriptor<T> : SortDescriptorBase<FieldSortDescriptor<T>, IFieldSort, T>, IFieldSort where T : class
	{
		private Field _field;
		protected override Field SortKey => _field;

		bool? IFieldSort.IgnoreUnmappedFields { get; set; }
		FieldType? IFieldSort.UnmappedType { get; set; }

		public virtual FieldSortDescriptor<T> Field(Field field)
		{
			_field = field;
			return this;
		}

		public virtual FieldSortDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath)
		{
			_field = objectPath;
			return this;
		}

		public virtual FieldSortDescriptor<T> UnmappedType(FieldType? type) => Assign(type, (a, v) => a.UnmappedType = v);

		public virtual FieldSortDescriptor<T> IgnoreUnmappedFields(bool? ignore = true) => Assign(ignore, (a, v) => a.IgnoreUnmappedFields = v);
	}
}
