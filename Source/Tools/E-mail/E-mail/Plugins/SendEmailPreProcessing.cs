using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using PZone.Common.Plugins;


namespace PZone.EmailTools.Plugins
{
    /// <summary>
    /// Предварительная обработка события отправки электронного письма с помощью действий.
    /// </summary>
    /// <remarks>
    /// <para>Подключаемый модуль позволяет выполнить алгоритмы обработки, описанные в действиях 
    /// (Action), перед отправкой письма.</para>
    /// <para>Действия (Action) должны иметь выходной параметр типа <c>Boolean</c> с именем 
    /// <c>IssueSend</c>. Параметр определяет, будет ли письмо отправлено штатными средствами после
    /// обработки или только помечено как отправленное: <c>true</c> — письмо отправляется, 
    /// <c>false</c> — письмо только помечается как отправленное. Окончательное значение параметра 
    /// <c>IssueSend</c> определяется последним выполненным действием.</para>
    /// <para>
    /// <code title="Настройка подключаемого модуля">
    /// Step
    /// Message:    	        Send
    /// Primary Entity:         email
    /// Stage:                  Pre-operation
    /// Unsecure Confuguration: [
    ///                             "actionName1",
    ///                             "actionName2",                        
    ///                             ...
    ///                             "actionNameN"
    ///                         ]
    /// </code>
    /// <list type="definition">
    ///   <item>
    ///     <term>actionName1..actionNameN</term>
    ///     <description>Системные имена действий (Action).</description>
    ///   </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// 
    // ReSharper disable once RedundantExtendsListEntry
    public class SendEmailPreProcessing : PluginBase, IPlugin
    {
        private List<string> _config;


        /// <inheritdoc />
        public SendEmailPreProcessing(string unsecureConfig) : base(unsecureConfig)
        {
        }


        /// <inheritdoc />
        public override void Configuring(Context context)
        {
            _config = JsonConvert.DeserializeObject<List<string>>(UnsecureConfiguration);
        }


        /// <inheritdoc />
        public override void Execute(Context context)
        {
            foreach (var action in _config)
            {
                var response = context.Service.Execute(new OrganizationRequest(action)
                {
                    ["Target"] = new EntityReference("email", (Guid)context.SourceContext.InputParameters["EmailId"])
                });
                var issueSend = response.Results.ContainsKey("IssueSend") && response.Results["IssueSend"] != null ? response.Results["IssueSend"] : false;
                context.SourceContext.InputParameters["IssueSend"] = issueSend;
            }
        }
    }
}