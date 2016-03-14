using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using static Nest.Indices;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Inferrence
{
	public class FeaturesInference
	{
		/** :section-number: 2.6 
		 * == Features
		 * Some URIs in Elasticsearch take a `Feature` enum.
		 * Within NEST, route values on the URI are represented as classes that implement an interface, `IUrlParameter`.
		 * Since enums _cannot_ implement interfaces in C#, a route parameter that would be of type `Feature` is represented using the `Features` class that
		 * the `Feature` enum implicitly converts to.
		 */

		/** Using the `Features` constructor directly is possible but rather involved */
		[U] public void Serializes()
		{
			Features fieldString = Feature.Mappings | Feature.Aliases;
			Expect("_mappings,_aliases")
				.WhenSerializing(fieldString);
		}
		
		[U]
		public void ImplicitConversion()
		{
			/** === Implicit conversion
			* Here we new an GET index elasticsearch request whichs takes Indices and Features.
			* Notice how we can use the `Feature` enum directly.
			*/
			var request = new GetIndexRequest(All, Feature.Settings | Feature.Warmers);
		} 
	}
}
