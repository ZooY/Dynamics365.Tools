using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;


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



    /// <summary>
    /// Расширение функционала перечисления <see cref="Entity"/>.
    /// </summary>
    // ReSharper disable once CheckNamespace
    public static class EntityExtension
    {
        /// <summary>
        /// Расширение атрибутного состава сущности из других сущностей.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="otherEntities">Набор других сущностей, атрибуты которых последовательно переносятся в исходную сущность.</param>
        /// <returns>
        /// Метод сливает атрибуты нескольких сущностей в одну.
        /// </returns>
        public static void Extend(this Entity entity, params Entity[] otherEntities)
        {
            foreach (var otherEntity in otherEntities)
            {
                foreach (var attribute in otherEntity.Attributes)
                    entity[attribute.Key] = attribute.Value;
            }
        }



        /// <summary>
        /// Клонирование объекта <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">Исходная сущность.</param>
        /// <returns>
        /// Метод возвращает новый объект типа <see cref="Entity"/>, полученный путем клонирования исходного объекта.
        /// </returns>
        public static Entity Clone(this Entity entity)
        {
            return ReferenceEquals(entity, null) ? null : JsonConvert.DeserializeObject<Entity>(JsonConvert.SerializeObject(entity));
        }
    }
}