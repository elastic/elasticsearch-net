using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type simple that is built using a Lower Case Tokenizer.
	/// </summary>
	public class SimpleAnalyzer : AnalyzerBase
    {
		public SimpleAnalyzer()
        {
            Type = "simple";
        }
    }
}