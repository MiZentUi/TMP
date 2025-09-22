using System.Reflection;

namespace LR6.Mediator
{
	internal class Sender : ISender
	{
		/// сборка для поиска хэндлеров
		private readonly Assembly? _assembly;
		
		/// словарь соответствия типа интерфейса хендлера
		/// и типа (класса) реализации интерфейса хэндлера
		private readonly Dictionary<Type, Type> _handlerImplementations = new();

		public Sender(Assembly? assembly = null)
		{
			_assembly = assembly ?? Assembly.GetCallingAssembly();
			RegisterHandlers(_assembly);
		}

		/// <summary>
		/// Запись в словарь всех типов интерфейсов хендлеров
		/// и их реализаций, имеющихся в сборке
		/// </summary>
		/// <param name="assembly">сборка для поиска хэндлеров</param>
		private void RegisterHandlers(Assembly assembly)
		{
			assembly.GetTypes()
				.Where(type => type.IsClass && !type.IsAbstract)
				.SelectMany(type => type.GetInterfaces()
					.Where(i => i.IsGenericType)
					.Select(i => new { InterfaceType = i, ClassType = type }))
				.Where(item => item.InterfaceType.GetGenericTypeDefinition() == typeof(IRequestHandler<>) || item.InterfaceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
				.ToDictionary(item => item.InterfaceType, item => item.ClassType)
				.ToList().ForEach(item => _handlerImplementations.Add(item.Key, item.Value));
		}

		public void Send<TRequest>(TRequest request) where TRequest : IRequest
		{
			Type handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
			try
			{
				var handler = CreateHandler(handlerType);
				handler.Handle((dynamic)request);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.Message);
				throw;
			}
		}

		public TResponse Send<TResponse>(IRequest<TResponse> request)
		{
			Type handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
			try
			{
				var handler = CreateHandler(handlerType);
				return handler.Handle((dynamic)request);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.Message);
				throw;
			}
		}

		/// <summary>
		/// Получить объект handler
		/// </summary>
		/// <param name="handlerType">Искомый тип</param>
		/// <returns>объект handler</returns>
		/// <exception cref="NotImplementedException"></exception>
		dynamic CreateHandler(Type handlerType)
		{
			var implementationType = _handlerImplementations[handlerType];
			if (implementationType is not null)
			{
				return Activator.CreateInstance(implementationType)!;
			}
			throw new NotImplementedException("Handler not found");
		}
	}
}
