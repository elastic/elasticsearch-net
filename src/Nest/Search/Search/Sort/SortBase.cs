using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(SortJsonConverter))]
	public interface ISort
	{
		Field SortKey { get; }

		[JsonProperty("missing")]
		string Missing { get; set; }

		[JsonProperty("order")]
		SortOrder? Order { get; set; }

		[JsonProperty("mode")]
		SortMode? Mode { get; set; }

		[JsonProperty("nested_filter")]
		QueryContainer NestedFilter { get; set; }

		[JsonProperty("nested_path")]
		Field NestedPath { get; set; }
	}

	public abstract class SortBase : ISort
	{
		public string Missing { get; set; }
		public SortOrder? Order { get; set; }
		public SortMode? Mode { get; set; }
		public QueryContainer NestedFilter { get; set; }
		public Field NestedPath { get; set; }
		Field ISort.SortKey => this.SortKey;
		protected abstract Field SortKey { get; }
	}

	public abstract class SortDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISort 
		where T : class 
		where TDescriptor : SortDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISort
		where TInterface : class, ISort
	{
		Field ISort.SortKey => this.SortKey;

		string ISort.Missing { get; set; }

		SortOrder? ISort.Order { get; set; }

		SortMode? ISort.Mode { get; set; }

		QueryContainer ISort.NestedFilter { get; set; }

		Field ISort.NestedPath { get; set; }

		protected abstract Field SortKey { get; }

		public virtual TDescriptor Ascending() => Assign(a => a.Order = SortOrder.Ascending);

		public virtual TDescriptor Descending() => Assign(a => a.Order = SortOrder.Descending);

		public virtual TDescriptor Order(SortOrder order) => Assign(a => a.Order = order);

		public virtual TDescriptor Mode(SortMode? mode) => Assign(a => a.Mode = mode);

		public virtual TDescriptor NestedFilter(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) =>
			Assign(a => a.NestedFilter = filterSelector?.Invoke(new QueryContainerDescriptor<T>()));

		public virtual TDescriptor NestedPath(Field path) => Assign(a => a.NestedPath = path);

		public virtual TDescriptor NestedPath(Expression<Func<T, object>> objectPath) => Assign(a => a.NestedPath = objectPath);

		public virtual TDescriptor MissingLast() => Assign(a => a.Missing = "_last");

		public virtual TDescriptor MissingFirst() => Assign(a => a.Missing = "_first");

		public virtual TDescriptor MissingValue(string value) => Assign(a => a.Missing = value);

	}
}