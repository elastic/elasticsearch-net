using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// An analyzer of type simple that is built using a Lower Case Tokenizer.
	/// </summary>
	public interface ISimpleAnalyzer : IAnalyzer { }

	public class SimpleAnalyzer : AnalyzerBase, ISimpleAnalyzer
	{
		public SimpleAnalyzer()
		{
			Type = "simple";
		}
	}

	public class SimpleAnalyzerDescriptor :
		AnalyzerDescriptorBase<SimpleAnalyzerDescriptor, ISimpleAnalyzer>, ISimpleAnalyzer
	{
		protected override string Type => "simple";
	}
}