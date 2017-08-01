using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using PZone.Common.Testing;
using PZone.StringTools.Plugins;
using PZone.Xrm.Sdk;


namespace PZone.Tests.StringTools.Plugins
{
    [TestClass]
    public class FormatTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var config = @"
{
    fullname: {
        Format: ""{0} {1} {2}"",
        Args: [
            ""lastname"",
            ""firstname"",
            ""middlename"",
        ]
    }
}";
            var entity = new Entity("contact", Guid.NewGuid())
            {
                ["firstname"] = "РОМАН",
                ["lastname"] = "копаев-ЗаДуНайский",
                ["middlename"] = "анатольевич"
            };
            IPluginExecutionContext context = new FakePluginExecutionContext(entity);
            IServiceProvider serviceProvider = new FakeServiceProvider(context);

            var plugin = new Format(config);
            plugin.Execute(serviceProvider);
            var result = context.GetContextEntity();
            Assert.AreEqual("копаев-ЗаДуНайский РОМАН анатольевич", result["fullname"]);
        }
    }
}