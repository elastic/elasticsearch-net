using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using static Nest.Indices;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Inferrence
{
	public class FeaturesInference
	{
		/** # Features
		 * Some urls in Elasticsearch take a {feature} enum
		 * RouteValues in NEST are represented as classes implementing IUrlParameter
		 * Since enums can not implement interfaces in C# this route param is represented using the Features class that can
		 * be implicitly converted to from the Feature enum
		 */

		/** Using the constructor directly is possible but rather involved */
		[U] public void Serializes()
		{
			Features fieldString = Feature.Mappings | Feature.Aliases;
			Expect("_mappings,_aliases")
				.WhenSerializing(fieldString);
		}
		
		[U]
		public void ImplicitConversion()
		{
			/**
			* Here we new an GET index elasticsearch request whichs takes Indices and Features.
			* Notice how we can use the Feature enum directly.
			*/
			var request = new GetIndexRequest(All, Feature.Settings | Feature.Warmers);
		} 
	}
}
