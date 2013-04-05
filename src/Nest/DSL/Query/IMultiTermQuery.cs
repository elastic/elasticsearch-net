namespace Nest
{
	internal interface IMultiTermQuery
	{
		RewriteMultiTerm? Rewrite { get; set; }
	}
}