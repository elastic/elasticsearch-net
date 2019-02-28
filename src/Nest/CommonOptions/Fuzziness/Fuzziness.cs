using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(FuzzinessFormatter))]
	public class Fuzziness : IFuzziness
	{
		private bool _auto;
		private int? _editDistance;
		private double? _ratio;

		public static Fuzziness Auto => new Fuzziness { _auto = true };

		bool IFuzziness.Auto => _auto;
		int? IFuzziness.EditDistance => _editDistance;
		double? IFuzziness.Ratio => _ratio;

		public static Fuzziness EditDistance(int distance) => new Fuzziness { _editDistance = distance };

		public static Fuzziness Ratio(double ratio) => new Fuzziness { _ratio = ratio };
	}
}
