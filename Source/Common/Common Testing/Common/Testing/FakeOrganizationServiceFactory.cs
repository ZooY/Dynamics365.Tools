using System;
using Microsoft.Xrm.Sdk;


namespace PZone.Common.Testing
{
    /// <summary>
    /// Заглушка для классов, реализующих интерфейс <see cref="IOrganizationServiceFactory"/>.
    /// </summary>
    public class FakeOrganizationServiceFactory : IOrganizationServiceFactory
    {
        private readonly IOrganizationService _service;

        /// <inheritdoc />
        public IOrganizationService CreateOrganizationService(Guid? userId)
        {
            return _service;
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        public FakeOrganizationServiceFactory()
        {
            _service = new FakeOrganizationService();
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="service">Реальный или фейковый Organization Service.</param>
        public FakeOrganizationServiceFactory(IOrganizationService service)
        {
            _service = service;
        }
    }
}