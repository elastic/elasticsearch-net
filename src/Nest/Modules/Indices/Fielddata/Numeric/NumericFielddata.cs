using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(NumericFielddata))]
	public interface INumericFielddata : IFielddata
	{
		[DataMember(Name ="format")]
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
