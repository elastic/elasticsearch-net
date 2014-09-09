using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IDeleteAliasResponse : IResponse
	{
	}

	public class DeleteAliasResponse : BaseResponse, IDeleteAliasResponse
	{
	}
}
