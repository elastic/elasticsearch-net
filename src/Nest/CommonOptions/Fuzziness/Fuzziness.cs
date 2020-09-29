// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(FuzzinessFormatter))]
	public class Fuzziness : IFuzziness
	{
		private bool _auto;
		private int? _low;
		private int? _high;
		private int? _editDistance;
		private double? _ratio;

		/// <summary>
		/// Generates an edit distance based on the length of the term.
		/// </summary>
		/// <remarks>
		/// <para>Equivalent to <see cref="AutoLength"/> with parameters 3 and 6.</para>
		/// <para><see cref="Auto"/> should generally be the preferred value for <see cref="Fuzziness"/></para>
		/// </remarks>
		public static Fuzziness Auto => new Fuzziness { _auto = true };
		/// <summary>
		/// Generates an edit distance based on the length of the term.
		/// </summary>
		/// <param name="low">Must match exactly for terms with less length</param>
		/// <param name="high">Two edits allowed for terms with greater length</param>
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

		/// <summary>
		/// The maximum allowed Levenshtein Edit Distance (or number of edits)
		/// </summary>
		/// <param name="distance">Levenshtein Edit Distance</param>
		public static Fuzziness EditDistance(int distance) => new Fuzziness { _editDistance = distance };

		public static Fuzziness Ratio(double ratio) => new Fuzziness { _ratio = ratio };
	}
}
