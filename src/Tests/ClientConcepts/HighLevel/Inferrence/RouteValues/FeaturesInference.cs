using System;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit.Sdk;
using static Nest.Indices;
using static Tests.Framework.RoundTripper;
using static Nest.Infer;

namespace Tests.ClientConcepts.HighLevel.Inferrence.FieldNames
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
			* Notice how we can the Feature enum directly.
			*/
			var request = new GetIndexRequest(All, Feature.Settings | Feature.Warmers);
		} 


	}
}
