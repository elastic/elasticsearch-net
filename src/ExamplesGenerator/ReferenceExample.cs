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

namespace ExamplesGenerator
{
	/// <summary>
	/// Compares Reference examples
	/// </summary>
	public class ReferenceExampleComparer : IEqualityComparer<ReferenceExample>
	{
		public static readonly ReferenceExampleComparer Instance = new ReferenceExampleComparer();

		public bool Equals(ReferenceExample x, ReferenceExample y) =>
			(x == null && y == null) || (y != null && x != null && x.Hash == y.Hash);

		public int GetHashCode(ReferenceExample obj) => HashCode.Combine(obj.Hash);
	}

	/// <summary>
	/// An example from the Elasticsearch asciidoc reference of all examples
	/// </summary>
	public class ReferenceExample
	{
		public ReferenceExample(string file, string hash, int lineNumber, string name, string content)
		{
			File = file;
			Hash = hash ?? throw new ArgumentNullException(nameof(hash));
			LineNumber = lineNumber;
			Content = content ?? throw new ArgumentNullException(nameof(content));
			Name = name;
			StartTag = $"// tag::{Hash}[]";
			EndTag = $"// end::{Hash}[]";
		}

		/// <summary>
		/// Full path of the file, e.g. /intro/getting-started.asciidoc
		/// </summary>
		public string File { get; }

		/// <summary>
		/// The content of the example
		/// </summary>
		public string Content { get; }

		/// <summary>
		/// The end tag for targeting with an asciidoc include
		/// </summary>
		public string EndTag { get; }

		/// <summary>
		/// The example hash
		/// </summary>
		public string Hash { get; }

		public List<Language> Languages { get; } = new List<Language>();

		/// <summary>
		/// The line number of the example within the reference
		/// </summary>
		public int LineNumber { get; }

		/// <summary>
		/// The name of the example within the generated C# code
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// The start tag for targeting with an asciidoc include
		/// </summary>
		public string StartTag { get; }
	}
}
