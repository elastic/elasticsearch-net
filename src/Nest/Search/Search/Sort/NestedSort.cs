using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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

		public NestedSortDescriptor<T> Path(Field path) => Assign(path, (a, v) => a.Path = v);

		public NestedSortDescriptor<T> Path<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Path = v);

		public NestedSortDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) =>
			Assign(filterSelector, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<T>()));

		public NestedSortDescriptor<T> Nested(Func<NestedSortDescriptor<T>, INestedSort> filterSelector) =>
			Assign(filterSelector, (a, v) => a.Nested = v?.Invoke(new NestedSortDescriptor<T>()));
	}
}
