namespace Nest
{
	/// <summary>
	/// Marker class that signals to the CustomJsonConverter to write the string verbatim
	/// </summary>
	internal class RawJson
	{
		public RawJson(string rawJsonData) => Data = rawJsonData;

		public string Data { get; set; }
	}
}
