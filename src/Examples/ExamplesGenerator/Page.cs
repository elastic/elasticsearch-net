using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExamplesGenerator
{
	public class Page
	{
		private const string Root = nameof(Root);
		private string _directory;
		private string _fileName;
		private string[] _nameParts;
		private string _namespace;
		private string[] _pascalNameParts;

		public Page(string name) => Name = name ?? throw new ArgumentNullException();

		public string ClassName => PascalNameParts.Last() + "Page";

		public string Directory => _directory ?? (_directory = PascalNameParts.Length == 1
			? Root
			: string.Join(Path.DirectorySeparatorChar, PascalNameParts.SkipLast(1)));

		public HashSet<Example> Examples { get; } = new HashSet<Example>(ExampleComparer.Instance);

		public string FileName => _fileName ?? (_fileName = ClassName + ".cs");

		public string Name { get; }

		public string[] NameParts => _nameParts ?? (_nameParts = Name.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
			// remove leading relative paths. This has the possibility of generating clashing file names in future
			.Where(n => n != "..")
			.ToArray());

		public string Namespace => _namespace ?? (_namespace = "Examples." + (PascalNameParts.Length == 1
			? Root
			: string.Join(".", PascalNameParts.SkipLast(1))));

		public string[] PascalNameParts => _pascalNameParts ?? (_pascalNameParts = NameParts
			.Select(p => p.Trim().LowercaseHyphenUnderscoreToPascal())
			.ToArray());

		public string FullPath(string root) => Path.GetFullPath(Path.Combine(root, Directory, FileName));
	}
}
