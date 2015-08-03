using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFielddata
	{
		[JsonProperty("format")]
		FielddataFormatOption? Format { get; set; }

		[JsonProperty("loading")]
		FielddataLoadingOption? Loading { get; set; }

		[JsonProperty("filter")]
		IFielddataFilter Filter { get; set; }

		[JsonProperty("precision")]
		GeoPrecision Precision { get; set; }
	}

	public class Fielddata : IFielddata
	{
		public FielddataFormatOption? Format { get; set; }
		public FielddataLoadingOption? Loading { get; set; }
		public IFielddataFilter Filter { get; set; }
		public GeoPrecision Precision { get; set; }
	}

	public class FielddataDescriptor
		: DescriptorBase<FielddataDescriptor, IFielddata>, IFielddata
	{
		IFielddataFilter IFielddata.Filter { get; set; }
		FielddataFormatOption? IFielddata.Format { get; set; }
		FielddataLoadingOption? IFielddata.Loading { get; set; }
		GeoPrecision IFielddata.Precision { get; set; }

		public FielddataDescriptor Filter(Func<FielddataFilterDescriptor, IFielddataFilter> filterSelector) =>
			Assign(a => a.Filter = filterSelector(new FielddataFilterDescriptor()));

		public FielddataDescriptor Format(FielddataFormatOption format) => Assign(a => a.Format = format);

		public FielddataDescriptor Loading(FielddataLoadingOption loading) => Assign(a => a.Loading = loading);

		public FielddataDescriptor GeoPrecision(GeoPrecision precision) => Assign(a => a.Precision = precision);
	}
}
