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
using System.Collections.ObjectModel;

namespace Tests.Core.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
		{
			T[] array = null;
			var count = 0;

			foreach (var item in source)
			{
				if (array == null) array = new T[size];
				array[count] = item;
				count++;
				if (count == size)
				{
					yield return new ReadOnlyCollection<T>(array);

					array = null;
					count = 0;
				}
			}

			if (array != null)
			{
				Array.Resize(ref array, count);
				yield return new ReadOnlyCollection<T>(array);
			}
		}
	}
}
