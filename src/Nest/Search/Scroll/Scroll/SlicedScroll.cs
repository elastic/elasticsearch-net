using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SlicedScroll>))]
	public interface ISlicedScroll
	{
		[JsonProperty("id")]
		int? Id { get; set; }
		[JsonProperty("max")]
		int? Max { get; set; }
		[JsonProperty("field")]
		Field Field { get; set; }
	}

	public class SlicedScroll : ISlicedScroll
	{
		public int? Id { get; set; }
		public int? Max { get; set; }
		public Field Field { get; set; }

	}

	public class SlicedScrollDescriptor<T> : DescriptorBase<SlicedScrollDescriptor<T>, ISlicedScroll>, ISlicedScroll
		where T : class
	{
		int? ISlicedScroll.Id { get; set; }
		int? ISlicedScroll.Max { get; set; }
		Field ISlicedScroll.Field { get; set; }

		public SlicedScrollDescriptor<T> Id(int? id) => Assign(a => a.Id = id);

		public SlicedScrollDescriptor<T> Max(int? max) => Assign(a => a.Max = max);

		public SlicedScrollDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SlicedScrollDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

	}
}
