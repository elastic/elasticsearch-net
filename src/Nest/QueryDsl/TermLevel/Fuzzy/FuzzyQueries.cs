using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;

namespace Nest
{
	public class FuzzyQuery : FuzzyQueryBase<string, Fuzziness>  
	{
	}

	public class FuzzyQueryDescriptor<T> 
		: FuzzyQueryDescriptorBase<FuzzyQueryDescriptor<T>, T, string, Fuzziness>
		, IFuzzyQuery where T : class
	{
		public FuzzyQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public FuzzyQueryDescriptor<T> Value(string value) => Assign(a => a.Value = value);
	}

	public class FuzzyNumericQuery : FuzzyQueryBase<double?, double?>  
	{
	}

	public class FuzzyNumericQueryDescriptor<T> 
		: FuzzyQueryDescriptorBase<FuzzyNumericQueryDescriptor<T>, T, double?, double?>
		, IFuzzyQuery where T : class
	{
		public FuzzyNumericQueryDescriptor<T> Fuzziness(double? fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public FuzzyNumericQueryDescriptor<T> Value(double? value) => Assign(a => a.Value = value);
	}

	public class FuzzyDateQuery : FuzzyQueryBase<DateTime?, TimeUnitExpression>  
	{
	}

	public class FuzzyDateQueryDescriptor<T> 
		: FuzzyQueryDescriptorBase<FuzzyDateQueryDescriptor<T>, T, DateTime?, TimeUnitExpression>
		, IFuzzyQuery where T : class
	{
		public FuzzyDateQueryDescriptor<T> Fuzziness(TimeUnitExpression fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public FuzzyDateQueryDescriptor<T> Value(DateTime? value) => Assign(a => a.Value = value);
	}
}
