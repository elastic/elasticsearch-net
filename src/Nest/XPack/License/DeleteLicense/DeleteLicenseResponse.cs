using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteLicenseResponse : IResponse
	{
	}

	public class DeleteLicenseResponse : ResponseBase, IDeleteLicenseResponse
	{
	}
}
