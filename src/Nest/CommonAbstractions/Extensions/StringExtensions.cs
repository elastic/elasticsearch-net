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

namespace Nest
{
	internal static class StringExtensions
	{
		internal static string ToCamelCase(this string s)
		{
			if (string.IsNullOrEmpty(s)) return s;

			if (!char.IsUpper(s[0])) return s;

			var camelCase = char.ToLowerInvariant(s[0]).ToString();
			if (s.Length > 1)
				camelCase += s.Substring(1);

			return camelCase;
		}
	}
}
