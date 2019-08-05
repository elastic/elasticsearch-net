using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExamplesGenerator
{
	public class Page
	{
		private string _directory;
		private string _fileName;
		private string[] _nameParts;
		private string[] _pascalNameParts;

		public Page(string name) => Name = name ?? throw new ArgumentNullException();

		public string Directory => _directory ?? (_directory = PascalNameParts.Length == 1
			? "Root"
			: string.Join(Path.DirectorySeparatorChar, PascalNameParts.SkipLast(1)));

		public List<Example> Examples { get; } = new List<Example>();

		public string FileName => _fileName ?? (_fileName = PascalNameParts.Last() + ".cs");

		public string Name { get; }

		public string[] NameParts => _nameParts ?? (_nameParts = Name.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
			// remove leading relative paths. This has the possibility of generating clashing file names in future
			.Where(n => n != "..")
			.ToArray());

		public string[] PascalNameParts => _pascalNameParts ?? (_pascalNameParts = NameParts
			.Select(p => p.Trim().LowercaseHyphenToPascal())
			.ToArray());

		public string FullPath(string root) => Path.GetFullPath(Path.Combine(root, Directory, FileName));
	}
}
