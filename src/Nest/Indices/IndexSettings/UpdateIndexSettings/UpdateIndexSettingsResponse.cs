namespace Nest
{
	public interface IUpdateIndexSettingsResponse : IAcknowledgedResponse { }

	public class UpdateIndexSettingsResponse : AcknowledgedResponseBase, IUpdateIndexSettingsResponse { }
}
