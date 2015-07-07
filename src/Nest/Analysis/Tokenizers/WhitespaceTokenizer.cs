using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type whitespace that divides text at whitespace.
	/// </summary>
	public class WhitespaceTokenizer : TokenizerBase
    {
		public WhitespaceTokenizer()
        {
            Type = "whitespace";
        }
    }
}