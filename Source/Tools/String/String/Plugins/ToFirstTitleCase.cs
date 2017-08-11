using Microsoft.Xrm.Sdk;
using PZone.Xrm.Plugins;


namespace PZone.StringTools.Plugins
{
    /// <summary>
    /// Перевод первой буквы строки в верхний регистр.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <code title="Настройка">
    /// Step
    /// Message:    	        Create
    /// Primary Entity:         &lt;&lt;entity_logical_name&gt;&gt;
    /// Name:                   Npf.StringTools.Plugins.ToTitleCase: &lt;&lt;description&gt;&gt;
    /// Run in User's Context:  Calling User
    /// Execution Order:        10
    /// Description:            &lt;&lt;description&gt;&gt;
    /// Stage:                  Pre-operation
    /// Unsecure Configuration: [
    ///                             "attribute_logical_name1",
    ///                             "attribute_logical_name2",
    ///                             ...
    ///                             "attribute_logical_nameN"
    ///                         ]
    /// </code>
    /// <para>Функционал может быть применен к одному или более атрибутов. attribute_logical_nameX - название атрибута, для которого будет применен функционал.</para>
    /// <note type="important">
    /// Функционал работает только для строковых атрибутов. В случае использования атрибута другого типа будет выдано исключение.
    /// </note>
    /// </para>
    /// </remarks>
    // ReSharper disable once RedundantExtendsListEntry
    public class ToFirstTitleCase : AttributesProcessing, IPlugin
    {
        /// <inheritdoc />
        public ToFirstTitleCase(string unsecureConfig) : base(unsecureConfig)
        {
        }
        
        /// <inheritdoc />
        public override void Execute(Context context)
        {
            SetValues(context, value => value.Substring(0, 1).ToUpper() + value.Substring(1));
        }
    }
}