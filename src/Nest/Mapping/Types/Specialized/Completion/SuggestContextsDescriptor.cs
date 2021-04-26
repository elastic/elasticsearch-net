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
using System.Collections.Generic;

namespace Nest
{
	public class SuggestContextsDescriptor<T>
		: DescriptorPromiseBase<SuggestContextsDescriptor<T>, IList<ISuggestContext>>
		where T : class
	{
		public SuggestContextsDescriptor() : base(new List<ISuggestContext>()) { }

		public SuggestContextsDescriptor<T> Category(Func<CategorySuggestContextDescriptor<T>, ICategorySuggestContext> categoryDescriptor) =>
			AddContext(categoryDescriptor?.Invoke(new CategorySuggestContextDescriptor<T>()));

		public SuggestContextsDescriptor<T> GeoLocation(Func<GeoSuggestContextDescriptor<T>, IGeoSuggestContext> geoLocationDescriptor) =>
			AddContext(geoLocationDescriptor?.Invoke(new GeoSuggestContextDescriptor<T>()));

		private SuggestContextsDescriptor<T> AddContext(ISuggestContext context) => context == null ? this : Assign(context, (a, v) => a.Add(v));
	}
}
