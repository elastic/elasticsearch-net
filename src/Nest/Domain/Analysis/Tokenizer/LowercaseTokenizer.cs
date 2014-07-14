using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type lowercase that performs the function of Letter Tokenizer and Lower Case Token Filter together. 
	/// <para>It divides text at non-letters and converts them to lower case. </para>
	/// <para>While it is functionally equivalent to the combination of Letter Tokenizer and Lower Case Token Filter, </para>
	/// <para>there is a performance advantage to doing the two tasks at once, hence this (redundant) implementation.</para>
	/// </summary>
	public class LowercaseTokenizer : TokenizerBase
    {
		public LowercaseTokenizer()
        {
            Type = "lowercase";
        }
    }
}