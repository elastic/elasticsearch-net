using System;

namespace Nest
{
	public interface IFuzzyStringQuery : IFuzzyQuery<string, Fuzziness> { }
	public class FuzzyQuery : FuzzyQueryBase<string, Fuzziness>, IFuzzyStringQuery { }

	public class FuzzyQueryDescriptor<T> 
		: FuzzyQueryDescriptorBase<FuzzyQueryDescriptor<T>, T, string, Fuzziness>
		, IFuzzyStringQuery where T : class
	{
		public FuzzyQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public FuzzyQueryDescriptor<T> Value(string value) => Assign(a => a.Value = value);
	}

	public interface IFuzzyNumericQuery : IFuzzyQuery<double?, double?> { }
	public class FuzzyNumericQuery : FuzzyQueryBase<double?, double?>, IFuzzyNumericQuery { }

	public class FuzzyNumericQueryDescriptor<T> 
		: FuzzyQueryDescriptorBase<FuzzyNumericQueryDescriptor<T>, T, double?, double?>
		, IFuzzyNumericQuery where T : class
	{
		public FuzzyNumericQueryDescriptor<T> Fuzziness(double? fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public FuzzyNumericQueryDescriptor<T> Value(double? value) => Assign(a => a.Value = value);
	}

	public interface IFuzzyDateQuery : IFuzzyQuery<DateTime?, Time> { }
	public class FuzzyDateQuery : FuzzyQueryBase<DateTime?, Time>, IFuzzyDateQuery { }


	public class FuzzyDateQueryDescriptor<T> 
		: FuzzyQueryDescriptorBase<FuzzyDateQueryDescriptor<T>, T, DateTime?, Time>
		, IFuzzyDateQuery where T : class
	{
		public FuzzyDateQueryDescriptor<T> Fuzziness(Time fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public FuzzyDateQueryDescriptor<T> Value(DateTime? value) => Assign(a => a.Value = value);
	}
}
