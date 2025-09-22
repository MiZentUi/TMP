namespace LR6.Mediator
{
	/// <summary>
	/// Запрос, не возвращающий данные
	/// </summary>
	internal interface IRequest { }

	/// <summary>
	/// Запрос, возвращающий данные
	/// </summary>
	/// <typeparam name="TResponse">Тип возвращаемых данных</typeparam>
	internal interface IRequest<out TResponse> { }
}
