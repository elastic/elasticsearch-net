namespace Nest_5_2_0
{
	public static class IndicesExtensions
	{
		public static string Resolve(this Indices marker, IConnectionSettingsValues connectionSettings)
		{
			if (marker == null) return null;
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			return connectionSettings.Inferrer.Resolve(marker);
		}
	}
}
