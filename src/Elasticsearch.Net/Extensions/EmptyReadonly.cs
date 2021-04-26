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
using System.Collections.ObjectModel;
using System.Linq;

namespace Elasticsearch.Net
{
	internal static class EmptyReadOnlyExtensions
	{
		public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable) =>
			enumerable == null ? EmptyReadOnly<T>.Collection : new ReadOnlyCollection<T>(enumerable.ToList());
		
		public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IList<T> enumerable) =>
			enumerable == null || enumerable.Count == 0 ? EmptyReadOnly<T>.Collection : new ReadOnlyCollection<T>(enumerable);
	}
	
	
	internal static class EmptyReadOnly<TElement>
	{
		public static readonly IReadOnlyCollection<TElement> Collection = new ReadOnlyCollection<TElement>(new TElement[0]);
		public static readonly IReadOnlyList<TElement> List = new List<TElement>();
	}

	internal static class EmptyReadOnly<TKey, TValue>
	{
		public static readonly IReadOnlyDictionary<TKey, TValue> Dictionary = new ReadOnlyDictionary<TKey, TValue>(new Dictionary<TKey, TValue>(0));
	}

}
