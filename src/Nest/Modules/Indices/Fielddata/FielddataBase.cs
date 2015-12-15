using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFielddata
	{
		[JsonProperty("loading")]
		FielddataLoading? Loading { get; set; }

		[JsonProperty("filter")]
		IFielddataFilter Filter { get; set; }
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
			Assign(a => a.Filter = filterSelector(new FielddataFilterDescriptor()));

		public TDescriptor Loading(FielddataLoading loading) => Assign(a => a.Loading = loading);
	}
}