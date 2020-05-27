namespace Nest
{
	public interface ITermsGroupSource : ISingleGroupSource {}

	public class TermsGroupSource : SingleGroupSourceBase, ITermsGroupSource
	{
	}

	public class TermsGroupSourceDescriptor<T>
		: SingleGroupSourceDescriptorBase<TermsGroupSourceDescriptor<T>, ITermsGroupSource, T>,
			ITermsGroupSource
	{
	}
}
