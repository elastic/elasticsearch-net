using System;

namespace Nest
{
	public class Fuzziness : IFuzziness
	{
		private bool _auto;
		private int? _editDistance;
		private double? _ratio;
		bool IFuzziness.Auto => this._auto;

		int? IFuzziness.EditDistance => this._editDistance;

		[Obsolete("Deprecated. Setting this is a noop")]
		double? IFuzziness.Ratio => this._ratio;

		public static Fuzziness Auto => new Fuzziness { _auto = true };

		public static Fuzziness EditDistance(int distance) => new Fuzziness { _editDistance = distance };

		[Obsolete("Deprecated. Setting this is a noop")]
		public static Fuzziness Ratio(double ratio) => new Fuzziness { _ratio = ratio };
	}
}
