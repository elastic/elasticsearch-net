using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IRankingEvaluationResponse<T> : IResponse
	{
	}

	public partial class RankingEvaluationResponse<T> : ResponseBase, IRankingEvaluationResponse<T>
	{
	}
}
