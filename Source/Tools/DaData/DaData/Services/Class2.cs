//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Query;


//namespace PZone.DaDataTools.Services
//{
//    /// <summary>
//    /// Сервис для работы с настройками DaData.
//    /// </summary>
//    public class DaDataSettingsService : ServiceBase
//    {
//        /// <inheritdoc />
//        public DaDataSettingsService(IOrganizationService service) : base(service)
//        {
//        }


//        /// <summary>
//        /// Запрос настроек DaData.
//        /// </summary>
//        /// <returns></returns>
//        public DaDataSettings Retrieve()
//        {
//            var fethXml = $@"<fetch count='1' no-lock='true'>
//  <entity name='{CommonMetadata.Setting.LogicalName}' >
//    <attribute name='{CommonMetadata.Setting.Key}' />
//    <attribute name='{CommonMetadata.Setting.StringValue1}' />
//    <attribute name='{CommonMetadata.Setting.StringValue2}' />
//    <attribute name='{CommonMetadata.Setting.BooleanValue1}' />
//    <attribute name='{CommonMetadata.Setting.BooleanValue2}' />
//    <filter>
//      <condition attribute='{CommonMetadata.Setting.Key}' operator='eq' value='DaData' />
//    </filter>
//  </entity>
//</fetch>";
//            var settings = Service.RetrieveMultiple(new FetchExpression(fethXml)).Entities.FirstOrDefault();
//            if (settings == null)
//                throw new IncorrectSystemConfigurationException("Отсутствует настройка с ключем DaData.");
//            return ModelFactory.Create<DaDataSettings>(settings);
//        }
//    }
//}