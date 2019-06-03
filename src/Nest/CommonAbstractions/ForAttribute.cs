using System;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Makes it explicit which API this request interface maps, the name of the interface informs
	/// The generator how to name related types
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface)]
	internal class MapsApiAttribute : Attribute
	{
		// ReSharper disable once UnusedParameter.Local
		public MapsApiAttribute(string restSpecName) { }
	}

	/// <summary>
	/// The preferred way to wire in a custom response formatter is for requests to override
	/// <see cref="RequestBase{TParameters}.RequestDefaults"/> however sometimes a request does not have
	/// access to enough type information. This attribute will set up the <see cref="RequestParameters{T}.CustomResponseBuilder"/>
	/// in the generated client methods instead.
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface)]
	internal class ResponseBuilderWithGeneric : Attribute
	{
		// ReSharper disable once UnusedParameter.Local
		public ResponseBuilderWithGeneric(string pathToBuilder) { }
	}
}
