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
using System.Linq;
using System.Text.RegularExpressions;
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.LowLevel
{
	public class LowLevelClientMethod
	{
		public CsharpNames CsharpNames { get; set; }

		public string Arguments { get; set; }
		public string OfficialDocumentationLink { get; set; }

		public Stability Stability { get; set; }
		public string PerPathMethodName { get; set; }
		public string HttpMethod { get; set; }

		public DeprecatedPath DeprecatedPath { get; set; }
		public UrlInformation Url { get; set; }
		public bool HasBody { get; set; }
		public IEnumerable<UrlPart> Parts { get; set; }
		public string Path { get; set; }


		public string UrlInCode
		{
			get
			{
				string Evaluator(Match m)
				{
					var arg = m.Groups[^1].Value.ToCamelCase();
					return $"{{{arg.ReservedKeywordReplacer()}:{arg}}}";
				}

				var url = Path.TrimStart('/');
				var options = Url.OriginalParts?.Select(p => p.Key) ?? Enumerable.Empty<string>();

				var pattern = string.Join("|", options);
				var urlCode = $"\"{url}\"";
				if (Path.Contains("{"))
				{
					var patchedUrl = Regex.Replace(url, "{(" + pattern + ")}", Evaluator);
					urlCode = $"Url($\"{patchedUrl}\")";
				}
				return urlCode;
			}
		}

		public string MapsApiArguments { get; set; }
	}
}
