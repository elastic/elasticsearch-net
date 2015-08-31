using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Language types used for language analyzers
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
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