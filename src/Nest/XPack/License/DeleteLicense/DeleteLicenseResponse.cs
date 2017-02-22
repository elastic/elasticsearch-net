using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IDeleteLicenseResponse : IResponse { }

	public class DeleteLicenseResponse : ResponseBase, IDeleteLicenseResponse { }
}
