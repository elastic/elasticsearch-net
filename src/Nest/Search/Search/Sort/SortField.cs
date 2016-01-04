using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{

	public interface IFieldSort : ISort
	{
		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmappedFields { get; set; }

		[JsonProperty("unmapped_type")]
		FieldType? UnmappedType { get; set; }
	}

	public class SortField : SortBase, IFieldSort
	{
		protected override Field SortKey => this.Field;
		public FieldType? UnmappedType { get; set; }
		public bool? IgnoreUnmappedFields { get; set; }
		public Field Field { get; set; }
	}

	public class SortFieldDescriptor<T> : SortDescriptorBase<SortFieldDescriptor<T>, IFieldSort, T>, IFieldSort where T : class
	{
		private Field _field;
		protected override Field SortKey => _field;

		bool? IFieldSort.IgnoreUnmappedFields { get; set; }
		FieldType? IFieldSort.UnmappedType { get; set; }

		public virtual SortFieldDescriptor<T> Field(Field field) => Assign(a => this._field = field);

		public virtual SortFieldDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => _field = objectPath);

		public virtual SortFieldDescriptor<T> UnmappedType(FieldType? type) => Assign(a => a.UnmappedType = type);

		public virtual SortFieldDescriptor<T> IgnoreUnmappedFields(bool? ignore = true) => Assign(a => a.IgnoreUnmappedFields = ignore);
	}
}
