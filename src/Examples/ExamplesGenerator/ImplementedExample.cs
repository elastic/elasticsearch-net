using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExamplesGenerator
{
	/// <summary>
	/// A doc example that has been implemented
	/// </summary>
	internal class ImplementedExample
	{
		public ImplementedExample(string path, string hash, BlockSyntax body)
		{
			Path = path;
			Hash = hash;
			Body = body;
		}

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
	}
}
