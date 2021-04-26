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

namespace DocGenerator.Documentation.Blocks
{
	public abstract class CodeBlock : IDocumentationBlock
	{
		protected readonly IList<string> Lines = new List<string>();

		protected CodeBlock(string text, int startingLine, int depth, string language, string memberName)
		{
			Lines.Add(text);
			LineNumber = startingLine;
			Depth = depth;
			Language = language;
			MemberName = memberName?.ToLowerInvariant() ?? string.Empty;
		}

		public int Depth { get; }

		public string Language { get; }
		public int LineNumber { get; }
		public string MemberName { get; }

		public string Value => string.Join(string.Empty, Lines);

		public abstract string ToAsciiDoc();

		public void AddLine(string line) => Lines.Add(line);
	}
}
