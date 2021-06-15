// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Represents the response from a call to the root path of an Elasticsearch server.
	/// </summary>
	internal class RootResponse : ElasticsearchResponseBase
	{
		/// <summary>
		/// The build version information of the product.
		/// </summary>
		[DataMember(Name = "version")]
		public BuildVersion Version { get; set; }

		/// <summary>
		/// The tagline of the product.
		/// </summary>
		[DataMember(Name = "tagline")]
		public string Tagline { get; set; }
	}
}
