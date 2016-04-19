using System;

namespace Nest
{
	public class Fuzziness : IFuzziness
	{
		private bool _auto;
		private int? _editDistance;
		private double? _ratio;
		bool IFuzziness.Auto { get { return this._auto; } }
		int? IFuzziness.EditDistance { get { return this._editDistance; } }

		[Obsolete("Deprecated in Elasticsearch 2.0")]
		double? IFuzziness.Ratio { get { return this._ratio; } }

		public static Fuzziness Auto { get { return new Fuzziness() { _auto = true }; } }

		public static Fuzziness EditDistance(int distance)
		{
			return new Fuzziness() { _editDistance = distance };
		}

		[Obsolete("Deprecated in Elasticsearch 2.0")]
		public static Fuzziness Ratio(double ratio)
		{
			return new Fuzziness() { _ratio = ratio };
		}
	}
}