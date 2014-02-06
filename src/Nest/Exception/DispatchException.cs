namespace Nest
{
	/// <summary>
	/// Occurs when an IElasticClient call does not have 
	/// enough information to dispatch into the raw client.
	/// </summary>
	public class DispatchException : System.Exception
	{
		public DispatchException(string msg) : base(msg)
		{
		}

		public DispatchException(string msg, System.Exception exp) : base(msg, exp)
		{
		}
	}
}