using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFielddata
	{
		[JsonProperty("filter")]
		IFielddataFilter Filter { get; set; }

		[JsonProperty("loading")]
		FielddataLoading? Loading { get; set; }
	}

	public abstract class FielddataBase : IFielddata
	{
		public IFielddataFilter Filter { get; set; }
		public FielddataLoading? Loading { get; set; }
	}

	public abstract class FielddataDescriptorBase<TDescriptor, TInterface>
		: DescriptorBase<TDescriptor, TInterface>, IFielddata
		where TDescriptor : FielddataDescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class, IFielddata
	{
		IFielddataFilter IFielddata.Filter { get; set; }
		FielddataLoading? IFielddata.Loading { get; set; }

		public TDescriptor Filter(Func<FielddataFilterDescriptor, IFielddataFilter> filterSelector) =>
			Assign(filterSelector(new FielddataFilterDescriptor()), (a, v) => a.Filter = v);

		public TDescriptor Loading(FielddataLoading? loading) => Assign(loading, (a, v) => a.Loading = v);
	}
}
