using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExamplesGenerator
{
	/// <summary>
	/// A doc example that has been implemented
	/// </summary>
	internal class ImplementedExample
	{
		public ImplementedExample(string method, int startLineNumber, int endLineNumber, string path, string hash, BlockSyntax body)
		{
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
		/// The method name in the C# file
		/// </summary>
		public string Method { get; }

		/// <summary>
		/// The start line number in the C# file
		/// </summary>
		public int StartLineNumber { get; }
	}
}
