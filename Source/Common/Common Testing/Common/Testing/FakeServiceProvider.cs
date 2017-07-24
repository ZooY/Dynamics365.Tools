using System;
using Microsoft.Xrm.Sdk;


namespace PZone.Common.Testing
{
    /// <summary>
    /// Заглушка для классов, реализующих интерфейс <see cref="IServiceProvider"/>.
    /// </summary>
    public class FakeServiceProvider : IServiceProvider
    {
        private readonly IPluginExecutionContext _context;


        /// <inheritdoc />
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IPluginExecutionContext))
                return _context;
            throw new ArgumentOutOfRangeException($@"Неизвестный тип сервиса ""{serviceType.FullName}"".");
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="context">Контекст выполненеия подключаемого модуля.</param>
        public FakeServiceProvider(IPluginExecutionContext context)
        {
            _context = context;
        }
    }
}