using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type whitespace that is built using a Whitespace Tokenizer.
	/// </summary>
	public class WhitespaceAnalyzer : AnalyzerBase
    {
		public WhitespaceAnalyzer()
        {
            Type = "whitespace";
        }
    }
}