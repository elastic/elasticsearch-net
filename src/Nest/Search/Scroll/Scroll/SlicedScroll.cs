using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(SlicedScroll))]
	public interface ISlicedScroll
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="id")]
		int? Id { get; set; }

		[DataMember(Name ="max")]
		int? Max { get; set; }
	}

	public class SlicedScroll : ISlicedScroll
	{
		public Field Field { get; set; }
		public int? Id { get; set; }
		public int? Max { get; set; }
	}

	public class SlicedScrollDescriptor<T> : DescriptorBase<SlicedScrollDescriptor<T>, ISlicedScroll>, ISlicedScroll
		where T : class
	{
		Field ISlicedScroll.Field { get; set; }
		int? ISlicedScroll.Id { get; set; }
		int? ISlicedScroll.Max { get; set; }

		public SlicedScrollDescriptor<T> Id(int? id) => Assign(a => a.Id = id);

		public SlicedScrollDescriptor<T> Max(int? max) => Assign(a => a.Max = max);

		public SlicedScrollDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SlicedScrollDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);
	}
}
