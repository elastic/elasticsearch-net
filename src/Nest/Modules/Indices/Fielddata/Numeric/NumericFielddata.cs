using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<NumericFielddata>))]
	public interface INumericFielddata : IFielddata
	{
		[JsonProperty("format")]
		NumericFielddataFormat? Format { get; set; }
	}

	public class NumericFielddata : FielddataBase, INumericFielddata
	{
		public NumericFielddataFormat? Format { get; set; }
	}

	public class NumericFielddataDescriptor
		: FielddataDescriptorBase<NumericFielddataDescriptor, INumericFielddata>, INumericFielddata
	{
		NumericFielddataFormat? INumericFielddata.Format { get; set; }

		public NumericFielddataDescriptor Format(NumericFielddataFormat? format) => Assign(a => a.Format = format);
	}
}
