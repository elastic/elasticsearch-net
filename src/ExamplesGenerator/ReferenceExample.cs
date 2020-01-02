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

		public bool Equals(ReferenceExample x, ReferenceExample y) => x.Hash == y.Hash;

		public int GetHashCode(ReferenceExample obj) => HashCode.Combine(obj.Hash);
	}

	/// <summary>
	/// An example from the Elasticsearch asciidoc reference of all examples
	/// </summary>
	public class ReferenceExample
	{
		public ReferenceExample(string hash, int lineNumber, string content)
		{
			Hash = hash ?? throw new ArgumentNullException(nameof(hash));
			LineNumber = lineNumber;
			Content = content ?? throw new ArgumentNullException(nameof(content));

			Name = $"Line{LineNumber}";
			StartTag = $"// tag::{Hash}[]";
			EndTag = $"// end::{Hash}[]";
		}

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
