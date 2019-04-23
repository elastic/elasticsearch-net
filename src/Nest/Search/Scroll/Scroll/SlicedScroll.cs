using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SlicedScroll>))]
	public interface ISlicedScroll
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("id")]
		int? Id { get; set; }

		[JsonProperty("max")]
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

		public SlicedScrollDescriptor<T> Id(int? id) => Assign(id, (a, v) => a.Id = v);

		public SlicedScrollDescriptor<T> Max(int? max) => Assign(max, (a, v) => a.Max = v);

		public SlicedScrollDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public SlicedScrollDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);
	}
}
