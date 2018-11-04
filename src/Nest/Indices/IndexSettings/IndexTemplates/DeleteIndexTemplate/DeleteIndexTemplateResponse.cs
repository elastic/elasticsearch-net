namespace Nest
{
	public interface IDeleteIndexTemplateResponse : IAcknowledgedResponse { }

	public class DeleteIndexTemplateResponse : AcknowledgedResponseBase, IDeleteIndexTemplateResponse { }
}
