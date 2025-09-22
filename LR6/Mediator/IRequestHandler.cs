namespace LR6.Mediator
{
	/// <summary>
	/// Обработчик запроса
	/// </summary>
	/// <typeparam name="TRequest">Тип запроса</typeparam>
	internal interface IRequestHandler<in TRequest> where TRequest : IRequest
	{
		void Handle(TRequest request);
	}

	/// <summary>
	/// Обработчик запроса, возвращающий данные
	/// </summary>
	/// <typeparam name="TRequest">Тип запроса</typeparam>
	/// <typeparam name="TResponse">Тип возвращаемых данных</typeparam>
	internal interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		TResponse Handle(TRequest request);
	}
}
