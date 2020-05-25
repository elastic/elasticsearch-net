// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExamplesGenerator
{
	public class ReferencePage
	{
		private const string Root = nameof(Root);
		private string _directory;
		private string _fileName;
		private string[] _nameParts;
		private string _namespace;
		private string[] _pascalNameParts;

		public ReferencePage(string name) => Name = name ?? throw new ArgumentNullException();

		/// <summary>
		/// The name of the class within the generated C# code
		/// </summary>
		public string ClassName => PascalNameParts.Last() + "Page";

		public string Directory => _directory ??= PascalNameParts.Length == 1
			? Root
			: string.Join(Path.DirectorySeparatorChar, PascalNameParts.SkipLast(1));

		public HashSet<ReferenceExample> Examples { get; } = new HashSet<ReferenceExample>(ReferenceExampleComparer.Instance);

		public string FileName => _fileName ??= ClassName + ".cs";

		/// <summary>
		/// The name of the reference page
		/// </summary>
		public string Name { get; }

		public string Namespace => _namespace ??= "Examples." + (PascalNameParts.Length == 1
			? Root
			: string.Join(".", PascalNameParts.SkipLast(1)));

		private string[] NameParts => _nameParts ??= Name.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
			// remove leading relative paths. This has the possibility of generating clashing file names in future
			.Where(n => n != "..")
			.ToArray();

		private string[] PascalNameParts => _pascalNameParts ??= NameParts
			.Select(p => p.Trim().LowercaseHyphenUnderscoreToPascal())
			.ToArray();

		public string FullPath(DirectoryInfo root) => Path.GetFullPath(Path.Combine(root.FullName, Directory, FileName));
	}
}
