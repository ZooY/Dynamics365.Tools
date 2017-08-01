//using System;
//using System.Activities;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Net;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Query;
//using Microsoft.Xrm.Sdk.Workflow;
//using Newtonsoft.Json;
//using PZone.Activities;
//using PZone.Common.Workflow;
//using PZone.Common.Workflow.Exceptions;


//namespace PZone.DaDataTools.Workflow
//{
//    /// <summary>
//    /// Получение адреса.
//    /// </summary>
//    public class GetAddress : WorkflowBase
//    {

//        #region Private Fields


//        private readonly Dictionary<string, string> _fiasLevels = new Dictionary<string, string>
//        {
//            { "0", "страна" }, { "1", "регион" }, { "3", "район" }, { "4", "город" }, { "5", "район города" }, { "6", "населенный пункт" },
//            { "7", "улица" }, { "8", "дом" }, { "90", "доп.территория" }, { "91", "улица в доп.территории" }, { "-1", "иностранный или пустой" }
//        };


//        private readonly Dictionary<string, string> _capitalMarkers = new Dictionary<string, string>
//        {
//            { "1", "центр района" }, { "2", "центр региона" }, { "3", "центр района и региона" }, { "0", "ни то, ни другое" }
//        };


//        private readonly Dictionary<int, string> _qcGeos = new Dictionary<int, string>
//        {
//            { 0, "Точные координаты" }, { 1, "Ближайший дом" }, { 2, "Улица" }, { 3, "Населенный пункт" }, { 4, "Город" }, { 5, "Координаты не определены" }
//        };


//        #endregion


//        #region In Arguments


//        /// <summary>
//        /// Входная строка адреса.
//        /// </summary>
//        [RequiredArgument]
//        [Input("Строка адреса")]
//        public InArgument<string> Address { get; set; }


//        #endregion


//        #region Out Arguments


//        /// <summary>
//        /// Адрес одной строкой (как показывается в списке подсказок)
//        /// </summary>
//        [Output("Адрес одной строкой (как показывается в списке подсказок)")]
//        public OutArgument<string> Value { get; set; }


//        /// <summary>
//        /// Адрес одной строкой (полный, от региона)
//        /// </summary>
//        [Output("Адрес одной строкой (полный, от региона)")]
//        public OutArgument<string> UnrestrictedValue { get; set; }


//        /// <summary>
//        /// Индекс
//        /// </summary>
//        [Output("Индекс")]
//        public OutArgument<string> PostalCode { get; set; }


//        /// <summary>
//        /// Страна
//        /// </summary>
//        [Output("Страна")]
//        public OutArgument<string> Country { get; set; }

        
//        /// <summary>
//        /// Код ФИАС региона
//        /// </summary>
//        [Output("Код ФИАС региона")]
//        public OutArgument<string> RegionFiasId { get; set; }


//        /// <summary>
//        /// Код КЛАДР региона
//        /// </summary>
//        [Output("Код КЛАДР региона")]
//        public OutArgument<string> RegionKladrId { get; set; }


//        /// <summary>
//        /// Регион с типом
//        /// </summary>
//        [Output("Регион с типом")]
//        public OutArgument<string> RegionWithType { get; set; }


//        /// <summary>
//        /// Тип региона (сокращенный)
//        /// </summary>
//        [Output("Тип региона (сокращенный)")]
//        public OutArgument<string> RegionType { get; set; }


//        /// <summary>
//        /// Тип региона
//        /// </summary>
//        [Output("Тип региона")]
//        public OutArgument<string> RegionTypeFull { get; set; }


//        /// <summary>
//        /// Регион
//        /// </summary>
//        [Output("Регион")]
//        public OutArgument<string> Region { get; set; }
        

//        /// <summary>
//        /// Код ФИАС района в регионе
//        /// </summary>
//        [Output("Код ФИАС района в регионе")]
//        public OutArgument<string> AreaFiasId { get; set; }


//        /// <summary>
//        /// Код КЛАДР района в регионе
//        /// </summary>
//        [Output("Код КЛАДР района в регионе")]
//        public OutArgument<string> AreaKladrId { get; set; }


//        /// <summary>
//        /// Район в регионе с типом
//        /// </summary>
//        [Output("Район в регионе с типом")]
//        public OutArgument<string> AreaWithType { get; set; }


//        /// <summary>
//        /// Тип района в регионе (сокращенный)
//        /// </summary>
//        [Output("Тип района в регионе (сокращенный)")]
//        public OutArgument<string> AreaType { get; set; }


//        /// <summary>
//        /// Тип района в регионе
//        /// </summary>
//        [Output("Тип района в регионе")]
//        public OutArgument<string> AreaTypeFull { get; set; }


//        /// <summary>
//        /// Район в регионе
//        /// </summary>
//        [Output("Район в регионе")]
//        public OutArgument<string> Area { get; set; }

        
//        /// <summary>
//        /// Код ФИАС города
//        /// </summary>
//        [Output("Код ФИАС города")]
//        public OutArgument<string> CityFiasId { get; set; }


//        /// <summary>
//        /// Код КЛАДР города
//        /// </summary>
//        [Output("Код КЛАДР города")]
//        public OutArgument<string> CityKladrId { get; set; }


//        /// <summary>
//        /// Город с типом
//        /// </summary>
//        [Output("Город с типом")]
//        public OutArgument<string> CityWithType { get; set; }


//        /// <summary>
//        /// Тип города (сокращенный)
//        /// </summary>
//        [Output("Тип города (сокращенный)")]
//        public OutArgument<string> CityType { get; set; }


//        /// <summary>
//        /// Тип города
//        /// </summary>
//        [Output("Тип города")]
//        public OutArgument<string> CityTypeFull { get; set; }


//        /// <summary>
//        /// Город
//        /// </summary>
//        [Output("Город")]
//        public OutArgument<string> City { get; set; }


//        /// <summary>
//        /// Административный округ города (только для Москвы)
//        /// </summary>
//        [Output("Административный округ города (только для Москвы)")]
//        public OutArgument<string> CityArea { get; set; }


//        /// <summary>
//        /// Район города
//        /// </summary>
//        [Output("Район города")]
//        public OutArgument<string> CityDistrict { get; set; }


//        /// <summary>
//        /// Район города с типом
//        /// </summary>
//        [Output("Район города с типом")]
//        public OutArgument<string> CityDistrictWithType { get; set; }


//        /// <summary>
//        /// Тип района города (сокращенный)
//        /// </summary>
//        [Output("Тип района города (сокращенный)")]
//        public OutArgument<string> CityDistrictType { get; set; }


//        /// <summary>
//        /// Тип района города
//        /// </summary>
//        [Output("Тип района города")]
//        public OutArgument<string> CityDistrictTypeFull { get; set; }
        

//        /// <summary>
//        /// Код ФИАС нас. пункта
//        /// </summary>
//        [Output("Код ФИАС нас. пункта")]
//        public OutArgument<string> SettlementFiasId { get; set; }


//        /// <summary>
//        /// Код КЛАДР нас. пункта
//        /// </summary>
//        [Output("Код КЛАДР нас. пункта")]
//        public OutArgument<string> SettlementKladrId { get; set; }


//        /// <summary>
//        /// Населенный пункт с типом
//        /// </summary>
//        [Output("Населенный пункт с типом")]
//        public OutArgument<string> SettlementWithType { get; set; }


//        /// <summary>
//        /// Тип населенного пункта (сокращенный)
//        /// </summary>
//        [Output("Тип населенного пункта (сокращенный)")]
//        public OutArgument<string> SettlementType { get; set; }


//        /// <summary>
//        /// Тип населенного пункта
//        /// </summary>
//        [Output("Тип населенного пункта")]
//        public OutArgument<string> SettlementTypeFull { get; set; }


//        /// <summary>
//        /// Населенный пункт
//        /// </summary>
//        [Output("Населенный пункт")]
//        public OutArgument<string> Settlement { get; set; }


//        /// <summary>
//        /// Код ФИАС улицы
//        /// </summary>
//        [Output("Код ФИАС улицы")]
//        public OutArgument<string> StreetFiasId { get; set; }


//        /// <summary>
//        /// Код КЛАДР улицы
//        /// </summary>
//        [Output("Код КЛАДР улицы")]
//        public OutArgument<string> StreetKladrId { get; set; }


//        /// <summary>
//        /// Улица с типом
//        /// </summary>
//        [Output("Улица с типом")]
//        public OutArgument<string> StreetWithType { get; set; }


//        /// <summary>
//        /// Тип улицы (сокращенный)
//        /// </summary>
//        [Output("Тип улицы (сокращенный)")]
//        public OutArgument<string> StreetType { get; set; }


//        /// <summary>
//        /// Тип улицы
//        /// </summary>
//        [Output("Тип улицы")]
//        public OutArgument<string> StreetTypeFull { get; set; }


//        /// <summary>
//        /// Улица
//        /// </summary>
//        [Output("Улица")]
//        public OutArgument<string> Street { get; set; }


//        /// <summary>
//        /// Код ФИАС дома
//        /// </summary>
//        [Output("Код ФИАС дома")]
//        public OutArgument<string> HouseFiasId { get; set; }


//        /// <summary>
//        /// Код КЛАДР дома
//        /// </summary>
//        [Output("Код КЛАДР дома")]
//        public OutArgument<string> HouseKladrId { get; set; }


//        /// <summary>
//        /// Тип дома (сокращенный)
//        /// </summary>
//        [Output("Тип дома (сокращенный)")]
//        public OutArgument<string> HouseType { get; set; }


//        /// <summary>
//        /// Тип дома
//        /// </summary>
//        [Output("Тип дома")]
//        public OutArgument<string> HouseTypeFull { get; set; }


//        /// <summary>
//        /// Дом
//        /// </summary>
//        [Output("Дом")]
//        public OutArgument<string> House { get; set; }


//        /// <summary>
//        /// Тип корпуса/строения (сокращенный)
//        /// </summary>
//        [Output("Тип корпуса/строения (сокращенный)")]
//        public OutArgument<string> BlockType { get; set; }


//        /// <summary>
//        /// Тип корпуса/строения
//        /// </summary>
//        [Output("Тип корпуса/строения")]
//        public OutArgument<string> BlockTypeFull { get; set; }


//        /// <summary>
//        /// Корпус/строение
//        /// </summary>
//        [Output("Корпус/строение")]
//        public OutArgument<string> Block { get; set; }


//        /// <summary>
//        /// 
//        /// </summary>
//        [Output("Тип квартиры (сокращенный)")]
//        public OutArgument<string> FlatType { get; set; }


//        /// <summary>
//        /// Тип квартиры
//        /// </summary>
//        [Output("Тип квартиры")]
//        public OutArgument<string> FlatTypeFull { get; set; }


//        /// <summary>
//        /// Квартира
//        /// </summary>
//        [Output("Квартира")]
//        public OutArgument<string> Flat { get; set; }


//        /// <summary>
//        /// Абонентский ящик
//        /// </summary>
//        [Output("Абонентский ящик")]
//        public OutArgument<string> PostalBox { get; set; }


//        /// <summary>
//        /// Код ФИАС
//        /// </summary>
//        [Output("Код ФИАС")]
//        public OutArgument<string> FiasId { get; set; }


//        /// <summary>
//        /// Уровень детализации, до которого адрес найден в ФИАС
//        /// </summary>
//        [Output("Уровень детализации, до которого адрес найден в ФИАС")]
//        public OutArgument<string> FiasLevel { get; set; }


//        /// <summary>
//        /// Название уровеня детализации, до которого адрес найден в ФИАС
//        /// </summary>
//        [Output("Название уровеня детализации, до которого адрес найден в ФИАС")]
//        public OutArgument<string> FiasLevelName { get; set; }


//        /// <summary>
//        /// Код КЛАДР
//        /// </summary>
//        [Output("Код КЛАДР")]
//        public OutArgument<string> KladrId { get; set; }


//        /// <summary>
//        /// Является ли город центром
//        /// </summary>
//        [Output("Является ли город центром")]
//        public OutArgument<string> CapitalMarker { get; set; }


//        /// <summary>
//        /// Является ли город центром (текстом)
//        /// </summary>
//        [Output("Является ли город центром (текстом)")]
//        public OutArgument<string> CapitalMarkerName { get; set; }


//        /// <summary>
//        /// Код ОКАТО
//        /// </summary>
//        [Output("Код ОКАТО")]
//        public OutArgument<string> Okato { get; set; }


//        /// <summary>
//        /// Код ОКТМО
//        /// </summary>
//        [Output("Код ОКТМО")]
//        public OutArgument<string> Oktmo { get; set; }


//        /// <summary>
//        /// Код ИФНС для физических лиц
//        /// </summary>
//        [Output("Код ИФНС для физических лиц")]
//        public OutArgument<string> TaxOffice { get; set; }


//        /// <summary>
//        /// Координаты: широта
//        /// </summary>
//        [Output("Координаты: широта")]
//        public OutArgument<string> GeoLat { get; set; }


//        /// <summary>
//        /// Координаты: долгота
//        /// </summary>
//        [Output("Координаты: долгота")]
//        public OutArgument<string> GeoLon { get; set; }


//        /// <summary>
//        /// Код точности координат
//        /// </summary>
//        [Output("Код точности координат")]
//        public OutArgument<string> QcGeo { get; set; }


//        /// <summary>
//        /// Код точности координат (строкой)
//        /// </summary>
//        [Output("Код точности координат (строкой)")]
//        public OutArgument<string> QcGeoName { get; set; }


//        /// <summary>
//        /// Код точности координат
//        /// </summary>
//        [Output("Код точности координат (Address Picklist)")]
//        [AttributeTarget("npf_address", "npf_geo_quality_code")]
//        public OutArgument<OptionSetValue> QcGeoAddressPicklist { get; set; }


//        #endregion


//        /// <inheritdoc />
//        protected override void Execute(Context context)
//        {
//            var sourceAddress = Address.Get(context);

//            string json;
//            try
//            {
//                var daDataService = new DaDataService(context.Service);
//                json = daDataService.RetrieveAddresses(sourceAddress, 1);
//                Debug.WriteLine($"DaData Result: {json}");
//            }
//            catch (WebException ex)
//            {
//                string errorMessage;
//                try
//                {
//                    using (var stream = ex.Response.GetResponseStream())
//                    using (var reader = new StreamReader(stream))
//                    {
//                        var errorJson = reader.ReadToEnd();
//                        var error = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(errorJson);
//                        errorMessage = error.ContainsKey("Message") ? error["Message"] : "Ответ прокси-сервиса DaData имеет непредвиденный формат и не содержит элемента \"Message\".";
//                        if (error.ContainsKey("ExceptionMessage"))
//                            errorMessage += $" {error["ExceptionMessage"]}";
//                    }
//                }
//                catch (Exception ee)
//                {
//                    errorMessage = $"Не удалось получить содержимое сообщения об ошибке прокси-сервиса DaData. {ee.Message}";
//                }

//                var webResponse = ex.Response as HttpWebResponse;
//                if (webResponse != null)
//                {
//                    if (webResponse.StatusCode == HttpStatusCode.BadRequest)
//                        throw new InvalidWorkflowExecutionException($"Ошибка запроса при вызове прокси-сервиса DaData. {errorMessage}", ex);
//                    if (webResponse.StatusCode == HttpStatusCode.InternalServerError)
//                        throw new InvalidWorkflowExecutionException($"Ошибка прокси-сервиса DaData. {errorMessage}", ex);
//                    throw new InvalidWorkflowExecutionException($"Ошибка вызова прокси-сервиса DaData. {errorMessage}", ex);
//                }

//                throw new InvalidWorkflowExecutionException($"Ошибка взаимодействия с прокси-сервисом DaData. {errorMessage}", ex);
//            }
//            catch (Exception ex)
//            {
//                throw new InvalidWorkflowExecutionException("Ошибка взаимодействия с прокси-сервисом DaData.", ex);
//            }

//            Dictionary<string, dynamic> result;
//            try
//            {
//                result = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
//                if (result["suggestions"].Count == 0)
//                    return;
//            }
//            catch (Exception ex)
//            {
//                throw new InvalidWorkflowExecutionException("Ошибка обработки ответа от прокси-сервиса DaData.", ex);
//            }

//            #region Set Out Arguments Values


//            var cache = new Dictionary<string, EntityReference>();

//            Value.Set(context, (string)result["suggestions"][0]["value"]);
//            UnrestrictedValue.Set(context, (string)result["suggestions"][0]["unrestricted_value"]);
//            PostalCode.Set(context, (string)result["suggestions"][0]["data"]["postal_code"]);
//            Country.Set(context, (string)result["suggestions"][0]["data"]["country"]);
            
//            RegionFiasId.Set(context, (string)result["suggestions"][0]["data"]["region_fias_id"]);
//            RegionKladrId.Set(context, (string)result["suggestions"][0]["data"]["region_kladr_id"]);
//            RegionWithType.Set(context, (string)result["suggestions"][0]["data"]["region_with_type"]);
//            RegionType.Set(context, (string)result["suggestions"][0]["data"]["region_type"]);
//            RegionTypeFull.Set(context, (string)result["suggestions"][0]["data"]["region_type_full"]);
//            Region.Set(context, (string)result["suggestions"][0]["data"]["region"]);
            
//            AreaFiasId.Set(context, (string)result["suggestions"][0]["data"]["area_fias_id"]);
//            AreaKladrId.Set(context, (string)result["suggestions"][0]["data"]["area_kladr_id"]);
//            AreaWithType.Set(context, (string)result["suggestions"][0]["data"]["area_with_type"]);
//            AreaType.Set(context, (string)result["suggestions"][0]["data"]["area_type"]);
//            AreaTypeFull.Set(context, (string)result["suggestions"][0]["data"]["area_type_full"]);
//            Area.Set(context, (string)result["suggestions"][0]["data"]["area"]);
            
//            CityFiasId.Set(context, (string)result["suggestions"][0]["data"]["city_fias_id"]);
//            CityKladrId.Set(context, (string)result["suggestions"][0]["data"]["city_kladr_id"]);
//            CityWithType.Set(context, (string)result["suggestions"][0]["data"]["city_with_type"]);
//            CityType.Set(context, (string)result["suggestions"][0]["data"]["city_type"]);
//            CityTypeFull.Set(context, (string)result["suggestions"][0]["data"]["city_type_full"]);
//            City.Set(context, (string)result["suggestions"][0]["data"]["city"]);

//            CityArea.Set(context, (string)result["suggestions"][0]["data"]["city_area"]);
//            CityDistrict.Set(context, (string)result["suggestions"][0]["data"]["city_district"]);
//            CityDistrictWithType.Set(context, (string)result["suggestions"][0]["data"]["city_district_with_type"]);
//            CityDistrictType.Set(context, (string)result["suggestions"][0]["data"]["city_district_type"]);
//            CityDistrictTypeFull.Set(context, (string)result["suggestions"][0]["data"]["city_district_type_full"]);
            
//            SettlementFiasId.Set(context, (string)result["suggestions"][0]["data"]["settlement_fias_id"]);
//            SettlementKladrId.Set(context, (string)result["suggestions"][0]["data"]["settlement_kladr_id"]);
//            SettlementWithType.Set(context, (string)result["suggestions"][0]["data"]["settlement_with_type"]);
//            SettlementType.Set(context, (string)result["suggestions"][0]["data"]["settlement_type"]);
//            SettlementTypeFull.Set(context, (string)result["suggestions"][0]["data"]["settlement_type_full"]);
//            Settlement.Set(context, (string)result["suggestions"][0]["data"]["settlement"]);

//            StreetFiasId.Set(context, (string)result["suggestions"][0]["data"]["street_fias_id"]);
//            StreetKladrId.Set(context, (string)result["suggestions"][0]["data"]["street_kladr_id"]);
//            StreetWithType.Set(context, (string)result["suggestions"][0]["data"]["street_with_type"]);
//            StreetType.Set(context, (string)result["suggestions"][0]["data"]["street_type"]);
//            StreetTypeFull.Set(context, (string)result["suggestions"][0]["data"]["street_type_full"]);
//            Street.Set(context, (string)result["suggestions"][0]["data"]["street"]);
//            HouseFiasId.Set(context, (string)result["suggestions"][0]["data"]["house_fias_id"]);
//            HouseKladrId.Set(context, (string)result["suggestions"][0]["data"]["house_kladr_id"]);
//            HouseType.Set(context, (string)result["suggestions"][0]["data"]["house_type"]);
//            HouseTypeFull.Set(context, (string)result["suggestions"][0]["data"]["house_type_full"]);
//            House.Set(context, (string)result["suggestions"][0]["data"]["house"]);
//            BlockType.Set(context, (string)result["suggestions"][0]["data"]["block_type"]);
//            BlockTypeFull.Set(context, (string)result["suggestions"][0]["data"]["block_type_full"]);
//            Block.Set(context, (string)result["suggestions"][0]["data"]["block"]);
//            FlatType.Set(context, (string)result["suggestions"][0]["data"]["flat_type"]);
//            FlatTypeFull.Set(context, (string)result["suggestions"][0]["data"]["flat_type_full"]);
//            Flat.Set(context, (string)result["suggestions"][0]["data"]["flat"]);
//            PostalBox.Set(context, (string)result["suggestions"][0]["data"]["postal_box"]);
//            FiasId.Set(context, (string)result["suggestions"][0]["data"]["fias_id"]);
//            var fiasLevel = (string)result["suggestions"][0]["data"]["fias_level"];
//            if (!string.IsNullOrWhiteSpace(fiasLevel))
//            {
//                FiasLevel.Set(context, fiasLevel);
//                if (_fiasLevels.ContainsKey(fiasLevel))
//                    FiasLevelName.Set(context, _fiasLevels[fiasLevel]);
//            }
//            KladrId.Set(context, (string)result["suggestions"][0]["data"]["kladr_id"]);
//            var capitalMarker = (string)result["suggestions"][0]["data"]["capital_marker"];
//            if (!string.IsNullOrWhiteSpace(capitalMarker))
//            {
//                CapitalMarker.Set(context, capitalMarker);
//                if (_capitalMarkers.ContainsKey(capitalMarker))
//                    CapitalMarkerName.Set(context, _capitalMarkers[capitalMarker]);
//            }
//            Okato.Set(context, (string)result["suggestions"][0]["data"]["okato"]);
//            Oktmo.Set(context, (string)result["suggestions"][0]["data"]["oktmo"]);
//            TaxOffice.Set(context, (string)result["suggestions"][0]["data"]["tax_office"]);
//            GeoLat.Set(context, (string)result["suggestions"][0]["data"]["geo_lat"]);
//            GeoLon.Set(context, (string)result["suggestions"][0]["data"]["geo_lon"]);
//            var qcGeo = (string)result["suggestions"][0]["data"]["qc_geo"];
//            // ReSharper disable once InvertIf
//            if (!string.IsNullOrWhiteSpace(qcGeo))
//            {
//                QcGeo.Set(context, qcGeo);
//                int qcGeoValue;
//                if (int.TryParse(qcGeo, out qcGeoValue))
//                {
//                    if (_qcGeos.ContainsKey(qcGeoValue))
//                    {
//                        QcGeoName.Set(context, _qcGeos[qcGeoValue]);
//                        //QcGeoAddressPicklist.Set(context, new OptionSetValue(100000000 + qcGeoValue));
//                    }
//                }
//            }


//            #endregion


//            //context.Log.Debug("Регион", Region.Get(context));
//            //context.Log.Debug("Регион, ФИАС", RegionFiasId.Get(context));
//            //context.Log.Debug("Регион, ссылка", RegionRef.Get(context));

//            //context.Log.Debug("Район", Area.Get(context));
//            //context.Log.Debug("Район, ФИАС", AreaFiasId.Get(context));
//            //context.Log.Debug("Район, ссылка", AreaRef.Get(context));

//            //context.Log.Debug("Город", City.Get(context));
//            //context.Log.Debug("Город, ФИАС", CityFiasId.Get(context));
//            //context.Log.Debug("Город, ссылка", CityRef.Get(context));

//            //context.Log.Debug("Населенный пункт", Settlement.Get(context));
//            //context.Log.Debug("Населенный пункт, ФИАС", SettlementFiasId.Get(context));
//            //context.Log.Debug("Населенный пункт, ссылка", SettlementRef.Get(context));
//        }


//        private static EntityReference _countryRef;


//        private EntityReference FindAddressPart(Context context, Dictionary<string, EntityReference> cache, Dictionary<string, dynamic> addressData, string entityName)
//        {
//            var regionRef = RetrieveOrCreate(context.Service, cache, addressData, Metadata.Region.LogicalName, "region");
//            if (entityName == "npf_region")
//                return regionRef;

//            var areaRef = RetrieveOrCreate(context.Service, cache, addressData, Metadata.Area.LogicalName, "area", regionRef);
//            if (entityName == "npf_area")
//                return areaRef;

//            var cityRef = RetrieveOrCreate(context.Service, cache, addressData, Metadata.City.LogicalName, "city", regionRef, areaRef);
//            if (entityName == "npf_city")
//                return cityRef;

//            var settlementRef = RetrieveOrCreate(context.Service, cache, addressData, Metadata.Settlement.LogicalName, "settlement", regionRef, areaRef, cityRef);
//            return settlementRef;
//        }


//        private static Guid Create(IOrganizationService service, string entityName, string name, string fiasCode, string kladrCode, Guid? regionId, Guid? areaId, Guid? cityId)
//        {
//            if (_countryRef == null)
//            {
//                var countries = service.RetrieveMultiple(new FetchExpression(
//                    "<fetch top='1' no-lock='true'>" +
//                    "<entity name='npf_country'>" +
//                    "<attribute name='npf_countryid' />" +
//                    "<filter>" +
//                    "<condition attribute='npf_iso_code' operator='eq' value='643' />" +
//                    "</filter>" +
//                    "</entity>" +
//                    "</fetch>")).Entities;
//                if (countries.Count > 0)
//                    _countryRef = countries[0].ToEntityReference();
//            }
//            var be = new Entity(entityName)
//            {
//                ["npf_name"] = name,
//                ["npf_fias_code"] = fiasCode,
//                ["npf_kladr_code"] = kladrCode,
//                ["npf_countryid"] = _countryRef
//            };
//            if (regionId != null)
//                be["npf_regionid"] = new EntityReference("npf_region", regionId.Value);
//            if (areaId != null)
//                be["npf_areaid"] = new EntityReference("npf_area", areaId.Value);
//            if (cityId != null)
//                be["npf_cityid"] = new EntityReference("npf_city", cityId.Value);
//            return service.Create(be);
//        }


//        private static EntityReference RetrieveOrCreate(IOrganizationService service, Dictionary<string, EntityReference> cache, Dictionary<string, dynamic> addressData, string entityName, string typeName, EntityReference regionRef = null, EntityReference areaRef = null, EntityReference cityRef = null)
//        {
//            var fias = (string)addressData["suggestions"][0]["data"][typeName + "_fias_id"];
//            if (fias == null)
//                return null;
//            var cacheKey = fias + entityName;
//            if (cache.ContainsKey(cacheKey))
//                return cache[cacheKey];
//            //var ddTypeName = (string)addressData["suggestions"][0]["data"][typeName + "_type"];
//            //var ddTypeFullName = (string)addressData["suggestions"][0]["data"][typeName + "_type_full"];
//            //var typeUseName = ddTypeName == ddTypeFullName ? ddTypeName : ddTypeName + '.';
//            var name = (string)addressData["suggestions"][0]["data"][typeName] + ' ' + (string)addressData["suggestions"][0]["data"][typeName + "_type"];
//            var kladr = (string)addressData["suggestions"][0]["data"][typeName + "_kladr_id"];
//            var fetchXml = "<fetch top='1' no-lock='true'>" +
//                           $"<entity name='{entityName}'>" +
//                           $"<attribute name='{entityName}id' />" +
//                           "<filter type='or'>" +
//                           $"<condition attribute='npf_fias_code' operator='eq' value='{fias}' />" +
//                           $"<condition attribute='npf_kladr_code' operator='eq' value='{kladr}' />" +
//                           "</filter>" +
//                           "</entity>" +
//                           "</fetch>";
//            var entities = service.RetrieveMultiple(new FetchExpression(fetchXml)).Entities.ToArray();
//            //throw new Exception($"FIAS: {fias}; KLADR: {kladr}; ENTITY NAME: {entityName}; ENTITIES: {entities.Length}; FETCH: {fetchXml}");
//            var id = entities.Length > 0 ? entities[0].Id : Create(service, entityName, name, fias, kladr, regionRef?.Id, areaRef?.Id, cityRef?.Id);
//            var entityRef = new EntityReference(entityName, id);
//            cache.Add(cacheKey, entityRef);
//            return entityRef;
//        }
//    }
//}