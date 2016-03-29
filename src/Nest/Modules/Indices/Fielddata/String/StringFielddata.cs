using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IStringFielddata : IFielddata
	{
		[JsonProperty("format")]
		StringFielddataFormat? Format { get; set; }
	}

	public class StringFielddata : FielddataBase, IStringFielddata
	{
		public StringFielddataFormat? Format { get; set; }
	}

	public class StringFielddataDescriptor
		: FielddataDescriptorBase<StringFielddataDescriptor, IStringFielddata>, IStringFielddata
	{
		StringFielddataFormat? IStringFielddata.Format { get; set; }

		public StringFielddataDescriptor Format(StringFielddataFormat format) => Assign(a => a.Format = format);
	}
}
