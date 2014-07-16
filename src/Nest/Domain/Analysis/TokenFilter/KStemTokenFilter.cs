namespace Nest
{
    /// <summary>
	///The kstem token filter is a high performance filter for english. 
	///<para> All terms must already be lowercased (use lowercase filter) for this filter to work correctly.</para>
    /// </summary>
    public class KStemTokenFilter : TokenFilterBase
    {
		public KStemTokenFilter()
            : base("kstem")
        {

        }

    }
}