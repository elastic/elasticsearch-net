/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Elasticsearch.Net.Utf8Json;

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
