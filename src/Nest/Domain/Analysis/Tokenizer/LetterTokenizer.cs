namespace Nest
{
	/// <summary>
	/// A tokenizer of type letter that divides text at non-letters. That’s to say, it defines tokens as maximal strings of adjacent letters. 
	/// <para>Note, this does a decent job for most European languages, but does a terrible job for some Asian languages, where words are not separated by spaces.</para>
	/// </summary>
	public class LetterTokenizer : TokenizerBase
    {
		public LetterTokenizer()
        {
            Type = "letter";
        }
    }
}