using System.ComponentModel;


namespace PZone.Common.Plugins
{
    /// <summary>    
    /// Стадии выполнения подключаемого модуля.
    /// </summary>
    /// <seealso cref="StageExtension"/>
    public enum Stage
    {
        /// <summary>
        /// Стадия Pre-Validation.
        /// </summary>
        /// <value>10</value>
        [Description("Pre-Validation")]
        PreValidation = 10,


        /// <summary>
        /// Стадия Pre-Opearation.
        /// </summary>
        /// <value>20</value>
        [Description("Pre-Operation")]
        PreOperation = 20,


        /// <summary>
        /// Стадия Post-Opearation.
        /// </summary>
        /// <value>40</value>
        [Description("Post-Operation")]
        PostOperation = 40
    }
}