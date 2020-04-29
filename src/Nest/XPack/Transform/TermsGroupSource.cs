namespace Nest
{
	public interface ITermsGroupSource : ISingleGroupSource {}

	public class TermsGroupSource : SingleGroupSourceBase, ITermsGroupSource
	{
		public TermsGroupSource(string name) : base(name) { }

		protected override string Type => "terms";
	}

	public class TermsGroupSourceDescriptor<T>
		: SingleGroupSourceDescriptorBase<TermsGroupSourceDescriptor<T>, ITermsGroupSource, T>,
			ITermsGroupSource
	{
		public TermsGroupSourceDescriptor(string name) : base(name, "terms") { }
	}
}
