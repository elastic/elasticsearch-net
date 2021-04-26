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

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExamplesGenerator
{
	/// <summary>
	/// A doc example that has been implemented
	/// </summary>
	internal class ImplementedExample
	{
		public ImplementedExample(string referenceFileAndLineNumber, string method, int startLineNumber, int endLineNumber, string path, string hash, BlockSyntax body)
		{
			ReferenceFileAndLineNumber = referenceFileAndLineNumber;
			Method = method;
			StartLineNumber = startLineNumber;
			EndLineNumber = endLineNumber;
			Path = path;
			Hash = hash;
			Body = body;
		}

		/// <summary>
		/// The end line number in the C# file
		/// </summary>
		public int EndLineNumber { get; set; }

		/// <summary>
		/// The collection of statements that make up this example
		/// </summary>
		public BlockSyntax Body { get; }

		/// <summary>
		/// The example hash
		/// </summary>
		public string Hash { get; }

		/// <summary>
		/// The path to the source C# file
		/// </summary>
		public string Path { get; }

		/// <summary>
		/// The original reference file and line number (from the DescriptionAttribute on the C# method)
		/// </summary>
		public string ReferenceFileAndLineNumber { get; }

		/// <summary>
		/// The method name in the C# file
		/// </summary>
		public string Method { get; }

		/// <summary>
		/// The start line number in the C# file
		/// </summary>
		public int StartLineNumber { get; }
	}
}
