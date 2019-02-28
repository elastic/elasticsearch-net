namespace Nest
{
	public class Fuzziness : IFuzziness
	{
		private bool _auto;
		private int? _low;
		private int? _high;
		private int? _editDistance;
		private double? _ratio;

		public static Fuzziness Auto => new Fuzziness { _auto = true };
		public static Fuzziness AutoLength(int low, int high) => new Fuzziness
		{
			_auto = true,
			_low = low,
			_high = high
		};

		bool IFuzziness.Auto => _auto;
		int? IFuzziness.Low => _low;
		int? IFuzziness.High => _high;
		int? IFuzziness.EditDistance => _editDistance;
		double? IFuzziness.Ratio => _ratio;

		public static Fuzziness EditDistance(int distance) => new Fuzziness { _editDistance = distance };

		public static Fuzziness Ratio(double ratio) => new Fuzziness { _ratio = ratio };
	}
}
