// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net;


namespace Nest
{
	/// <summary>
	/// Language types used for language analyzers
	/// </summary>
	[StringEnum]
	public enum Language
	{
		Arabic,
		Armenian,
		Basque,
		Brazilian,
		Bulgarian,
		Catalan,
		Chinese,
		Cjk,
		Czech,
		Danish,
		Dutch,
		English,
		/// <summary>Available in Elasticsearch 7.6.0+</summary>
		Estonian,
		Finnish,
		French,
		Galician,
		German,
		Greek,
		Hindi,
		Hungarian,
		Indonesian,
		Irish,
		Italian,
		Latvian,
		Norwegian,
		Persian,
		Portuguese,
		Romanian,
		Russian,
		Sorani,
		Spanish,
		Swedish,
		Turkish,
		Thai
	}
}
