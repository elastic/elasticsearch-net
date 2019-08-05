using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExamplesGenerator
{
	public class Page
	{
		private string _fileName;
		private string[] _nameParts;
		private string[] _pascalNameParts;
		private string _directory;

		public Page(string name) => Name = name ?? throw new ArgumentNullException();

		public string Directory => _directory ?? (_directory = string.Join(Path.DirectorySeparatorChar, PascalNameParts.SkipLast(1)));

		public List<Example> Examples { get; } = new List<Example>();

		public string FileName => _fileName ?? (_fileName = PascalNameParts.Last() + ".cs");

		public string Name { get; }

		public string[] NameParts => _nameParts ?? (_nameParts = Name.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries));

		public string[] PascalNameParts => _pascalNameParts ?? (_pascalNameParts = NameParts
			.Select(p => LowercaseHyphenToPascal(p.Trim()))
			.ToArray());

		private static string LowercaseHyphenToPascal(string lowercaseHyphenatedInput) =>
			Regex.Replace(lowercaseHyphenatedInput.Replace("-", " "), @"\b([a-z])", m => m.Captures[0].Value.ToUpper())
				.Replace(" ", string.Empty);

		public string FullPath(string root) => Path.GetFullPath(Path.Combine(root, Directory, FileName));
	}
}
