using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IFielddata
	{
		[DataMember(Name ="filter")]
		IFielddataFilter Filter { get; set; }

		[DataMember(Name ="loading")]
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
			Assign(a => a.Filter = filterSelector(new FielddataFilterDescriptor()));

		public TDescriptor Loading(FielddataLoading? loading) => Assign(a => a.Loading = loading);
	}
}
