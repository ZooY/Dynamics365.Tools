using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using PZone.StringTools.Plugins;
using PZone.Xrm.Plugins;
using PZone.Xrm.Testing;


namespace PZone.Tests.StringTools.Plugins
{
    [TestClass]
    public class ToFirstTitleCaseTest
    {
        private const string CONFIG_STR = @"
[
    ""quote"",
]";


        [TestMethod]
        public void SimpleData()
        {
            var entity = new Entity("contact", Guid.NewGuid())
            {
                ["quote"] = "никогда НЕ быЛО, И ВОТ опЯть!"
            };
            IPluginExecutionContext context = new FakePluginExecutionContext(entity);
            IServiceProvider serviceProvider = new FakeServiceProvider(context);

            var plugin = new ToFirstTitleCase(CONFIG_STR);
            plugin.Execute(serviceProvider);

            var result = context.GetContextEntity();
            Assert.AreEqual("Никогда НЕ быЛО, И ВОТ опЯть!", result["quote"]);
        }
    }
}