using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

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

	public class SortField : SortBase, IFieldSort
	{
		public static readonly IList<ISort> ByDocumentOrder = new ReadOnlyCollection<ISort>(new List<ISort> { new SortField { Field = "_doc" } });
		public Field Field { get; set; }
		public bool? IgnoreUnmappedFields { get; set; }
		public FieldType? UnmappedType { get; set; }
		protected override Field SortKey => Field;
	}

	public class SortFieldDescriptor<T> : SortDescriptorBase<SortFieldDescriptor<T>, IFieldSort, T>, IFieldSort where T : class
	{
		private Field _field;
		protected override Field SortKey => _field;

		bool? IFieldSort.IgnoreUnmappedFields { get; set; }
		FieldType? IFieldSort.UnmappedType { get; set; }

		public virtual SortFieldDescriptor<T> Field(Field field) => Assign(a => _field = field);

		public virtual SortFieldDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => _field = objectPath);

		public virtual SortFieldDescriptor<T> UnmappedType(FieldType? type) => Assign(a => a.UnmappedType = type);

		public virtual SortFieldDescriptor<T> IgnoreUnmappedFields(bool? ignore = true) => Assign(a => a.IgnoreUnmappedFields = ignore);
	}
}
