using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<NestedSort>))]
	public interface INestedSort
	{
		[JsonProperty("filter")]
		QueryContainer Filter { get; set; }

		[JsonProperty("nested")]
		INestedSort Nested { get; set; }

		[JsonProperty("path")]
		Field Path { get; set; }
	}

	public class NestedSort : INestedSort
	{
		public QueryContainer Filter { get; set; }
		public INestedSort Nested { get; set; }
		public Field Path { get; set; }
	}

	public class NestedSortDescriptor<T>
		: DescriptorBase<NestedSortDescriptor<T>, INestedSort>, INestedSort where T : class
	{
		QueryContainer INestedSort.Filter { get; set; }
		INestedSort INestedSort.Nested { get; set; }
		Field INestedSort.Path { get; set; }

		public NestedSortDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) =>
			Assign(a => a.Filter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));

		public NestedSortDescriptor<T> Nested(Func<NestedSortDescriptor<T>, INestedSort> filterSelector) =>
			Assign(a => a.Nested = filterSelector?.Invoke(new NestedSortDescriptor<T>()));

		public NestedSortDescriptor<T> Path(Field path) => Assign(a => a.Path = path);

		public NestedSortDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);
	}
}
