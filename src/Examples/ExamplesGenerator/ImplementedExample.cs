namespace ExamplesGenerator
{
	/// <summary>
	/// A doc example that has been implemented
	/// </summary>
	internal class ImplementedExample
	{
		public ImplementedExample(string path, string hash)
		{
			Path = path;
			Hash = hash;
		}

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
