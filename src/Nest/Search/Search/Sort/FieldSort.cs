// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
		public static readonly IList<ISort> ByDocumentOrder = new ReadOnlyCollection<ISort>(new List<ISort> { new FieldSort { Field = "_doc" } });
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
