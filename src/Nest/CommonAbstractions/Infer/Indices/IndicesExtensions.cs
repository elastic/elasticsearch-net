
namespace Nest
{
	public static class IndicesExtensions
	{
		public static string Resolve(this Indices marker, IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			return connectionSettings.Inferrer.Resolve(marker);
		}
	}
}
