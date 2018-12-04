using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(NestedSort))]
	public interface INestedSort
	{
		[DataMember(Name = "filter")]
		QueryContainer Filter { get; set; }

		[DataMember(Name = "nested")]
		INestedSort Nested { get; set; }

		[DataMember(Name = "path")]
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

		public NestedSortDescriptor<T> Path(Field path) => Assign(a => a.Path = path);

		public NestedSortDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);

		public NestedSortDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) =>
			Assign(a => a.Filter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));

		public NestedSortDescriptor<T> Nested(Func<NestedSortDescriptor<T>, INestedSort> filterSelector) =>
			Assign(a => a.Nested = filterSelector?.Invoke(new NestedSortDescriptor<T>()));
	}
}
