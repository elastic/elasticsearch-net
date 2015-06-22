using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IDeleteSearchTemplateResponse : IResponse
	{
	}

	public class DeleteSearchTemplateResponse : BaseResponse, IDeleteSearchTemplateResponse
	{
	}
}
