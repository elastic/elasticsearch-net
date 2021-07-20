namespace Nest
{
	public abstract class ExistsResponseBase : ResponseBase
	{
		public bool Exists => ApiCall is {Success: true, HttpStatusCode: 200};
	}
}
