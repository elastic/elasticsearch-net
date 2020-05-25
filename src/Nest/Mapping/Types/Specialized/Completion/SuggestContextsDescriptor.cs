// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
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
