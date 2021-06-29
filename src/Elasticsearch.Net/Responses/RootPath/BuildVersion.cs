// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Information about the build version.
	/// </summary>
	internal class BuildVersion
	{
		/// <summary>
		/// The SemVer version number, which may include pre-release labels.
		/// </summary>
		[DataMember(Name = "number")]
		public string Number { get; set; }

		/// <summary>
		/// The flavor of the build.
		/// </summary>
		[DataMember(Name = "build_flavor")]
		public string BuildFlavor { get; set; }
	}
}
