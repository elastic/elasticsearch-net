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

using System.Collections.Generic;

namespace Nest
{
	public class TermsOrderDescriptor<T> : DescriptorPromiseBase<TermsOrderDescriptor<T>, IList<TermsOrder>>
		where T : class
	{
		public TermsOrderDescriptor() : base(new List<TermsOrder>()) { }

		public TermsOrderDescriptor<T> CountAscending() => Assign(a => a.Add(TermsOrder.CountAscending));

		public TermsOrderDescriptor<T> CountDescending() => Assign(a => a.Add(TermsOrder.CountDescending));

		public TermsOrderDescriptor<T> KeyAscending() => Assign(a => a.Add(TermsOrder.KeyAscending));

		public TermsOrderDescriptor<T> KeyDescending() => Assign(a => a.Add(TermsOrder.KeyDescending));

		public TermsOrderDescriptor<T> Ascending(string key) =>
			string.IsNullOrWhiteSpace(key) ? this : Assign(key,(a, v) => a.Add(new TermsOrder { Key = v, Order = SortOrder.Ascending }));

		public TermsOrderDescriptor<T> Descending(string key) =>
			string.IsNullOrWhiteSpace(key) ? this : Assign(key,(a, v) => a.Add(new TermsOrder { Key = v, Order = SortOrder.Descending }));
	}
}
