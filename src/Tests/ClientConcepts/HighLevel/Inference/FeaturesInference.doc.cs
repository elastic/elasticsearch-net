using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using static Nest.Indices;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	public class FeaturesInference
	{
		/**[[features-inference]]
		 * == Features Inference
		 * Some URIs in Elasticsearch take a `Feature` enum.
		 * Within NEST, route values on the URI are represented as classes that implement an interface, `IUrlParameter`.
		 * Since enums _cannot_ implement interfaces in C#, a route parameter that would be of type `Feature` is represented using the `Features` class that
		 * the `Feature` enum implicitly converts to.
		 */

		/**=== Constructor
		* Using the `Features` constructor directly is possible but rather involved */
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
			* Here we instantiate a GET index request which takes two features, settings and warmers.
			* Notice how we can use the `Feature` enum directly.
			*/
			var request = new GetIndexRequest(All, Feature.Settings | Feature.Warmers);
		}
	}
}
