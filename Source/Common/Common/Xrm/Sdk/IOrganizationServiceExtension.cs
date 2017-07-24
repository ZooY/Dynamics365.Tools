using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;


namespace PZone.Xrm.Sdk
{
    /// <summary>
    /// Расширение функционала классов, реализующих интерфейс <see cref="IOrganizationService"/>.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IOrganizationServiceExtension
    {
        #region RetrieveMultiple


        /// <summary>
        /// Получение списка записей по запросу FetchXML.
        /// </summary>
        /// <param name="service">Экземпляр сервиса CRM.</param>
        /// <param name="fetchXml">Текст запроса.</param>
        /// <returns>
        /// Метод возвращает список найденных записей.
        /// </returns>
        public static EntityCollection RetrieveMultiple(this IOrganizationService service, string fetchXml)
        {
            return service.RetrieveMultiple(new FetchExpression(fetchXml));
        }


        #endregion
    }
}