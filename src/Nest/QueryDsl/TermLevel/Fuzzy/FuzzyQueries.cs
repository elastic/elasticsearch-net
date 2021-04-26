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

using System;

namespace Nest
{
	public interface IFuzzyStringQuery : IFuzzyQuery<string, Fuzziness> { }

	public class FuzzyQuery : FuzzyQueryBase<string, Fuzziness>, IFuzzyStringQuery { }

	public class FuzzyQueryDescriptor<T>
		: FuzzyQueryDescriptorBase<FuzzyQueryDescriptor<T>, T, string, Fuzziness>
			, IFuzzyStringQuery where T : class
	{
		public FuzzyQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(fuzziness, (a, v) => a.Fuzziness = v);

		public FuzzyQueryDescriptor<T> Value(string value) => Assign(value, (a, v) => a.Value = v);
	}

	public interface IFuzzyNumericQuery : IFuzzyQuery<double?, double?> { }

	public class FuzzyNumericQuery : FuzzyQueryBase<double?, double?>, IFuzzyNumericQuery { }

	public class FuzzyNumericQueryDescriptor<T>
		: FuzzyQueryDescriptorBase<FuzzyNumericQueryDescriptor<T>, T, double?, double?>
			, IFuzzyNumericQuery where T : class
	{
		public FuzzyNumericQueryDescriptor<T> Fuzziness(double? fuzziness) => Assign(fuzziness, (a, v) => a.Fuzziness = v);

		public FuzzyNumericQueryDescriptor<T> Value(double? value) => Assign(value, (a, v) => a.Value = v);
	}

	public interface IFuzzyDateQuery : IFuzzyQuery<DateTime?, Time> { }

	public class FuzzyDateQuery : FuzzyQueryBase<DateTime?, Time>, IFuzzyDateQuery { }


	public class FuzzyDateQueryDescriptor<T>
		: FuzzyQueryDescriptorBase<FuzzyDateQueryDescriptor<T>, T, DateTime?, Time>
			, IFuzzyDateQuery where T : class
	{
		public FuzzyDateQueryDescriptor<T> Fuzziness(Time fuzziness) => Assign(fuzziness, (a, v) => a.Fuzziness = v);

		public FuzzyDateQueryDescriptor<T> Value(DateTime? value) => Assign(value, (a, v) => a.Value = v);
	}
}
