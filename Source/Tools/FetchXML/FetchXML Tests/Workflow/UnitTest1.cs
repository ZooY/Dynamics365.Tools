using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace PZone.Tests.FetchXmlTools
{
    [TestClass]
    public class ExecuteActionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var entityRef = new EntityReference("incident", new Guid("a7d058b8-e528-eb11-a92c-005056011bf9"));
            var json = JsonConvert.SerializeObject(entityRef);


            var parametersJsonStr = "{\"parentIncidentRef\":{\"Id\":\"a7d058b8-e528-eb11-a92c-005056011bf9\",\"LogicalName\":\"incident\"}}";
                Dictionary<string, Type> _actionProperties = new Dictionary<string, Type> {
                { "parentIncidentRef", typeof( Microsoft.Xrm.Sdk.EntityReference) },
                { "Target", typeof(Microsoft.Xrm.Sdk.EntityReference) }
            };

            var parameters = new Dictionary<string, object>();

            foreach (var property in JObject.Parse(parametersJsonStr).Properties())
                parameters.Add(property.Name, property.Value.ToObject(_actionProperties[property.Name]));
        }
    }
}