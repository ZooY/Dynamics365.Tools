using Microsoft.Xrm.Sdk;
using PZone.Common.Plugins;
using PZone.Common.Plugins.Exceptions;


namespace PZone.Xrm.Sdk
{
    /// <summary>
    /// Расширение стандартного функционала класса <see cref="IPluginExecutionContext"/>.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IPluginExecutionContextExtension
    {
        /// <summary>
        /// Название снимка состояния по умолчанию.
        /// </summary>
        public const string DefaultImageName = "Image";


        /// <summary> 
        /// Получение сущности из контекста подключаемого модуля.
        /// </summary>
        /// <param name="context">Контекст подключаемого модуля.</param>
        /// <exception cref="System.Exception">
        /// Сущность не доступна в контексте данного события.
        /// </exception>
        /// <returns>
        /// Метод возвращает сущность из контекста плагина (если событие предусматривает передачу сущности).
        /// </returns>
        /// <remarks>
        /// Сущность в контекст передается, например, при событиях <see cref="Microsoft.Xrm.Sdk.Messages.CreateRequest"/>, <see cref="Microsoft.Xrm.Sdk.Messages.UpdateRequest"/> и др.
        /// </remarks>
        /// <example>
        /// <code>
        /// public class MyPlugin : IPlugin
        /// {
        ///     public void Execute(IServiceProvider serviceProvider)
        ///     {
        ///         ...
        ///         var context = serviceProvider.GetContext();
        ///         var entity = context.GetContextEntity();
        ///         ...
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="IServiceProviderExtension"/>
        public static Entity GetContextEntity(this IPluginExecutionContext context)
        {
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
                return (Entity)context.InputParameters["Target"];
            const string ERROR_MESSAGE = "Entity is not accessible in a context of {0} {1} event.";
            throw new PluginConfigurationException(string.Format(ERROR_MESSAGE, ((Stage)context.Stage).GetDisplayName(), context.MessageName));
        }


        /// <summary>    
        /// Получение ссылки на сущность из контекста плагина.
        /// </summary>
        /// <param name="context">Контекст плагина.</param>
        /// <exception cref="System.Exception">
        /// Ссылка на сущность не доступна в контексте данного события.
        /// </exception>
        /// <returns>
        /// Метод возвращает ссылку на сущность из контекста плагина, если событие предусматривает передачу ссылки.
        /// </returns>
        /// <remarks>
        /// Ссылки передаются, например, при событиях <see cref="Microsoft.Xrm.Sdk.Messages.DeleteRequest"/>, 
        /// <see cref="Microsoft.Xrm.Sdk.Messages.AssociateRequest"/>, <see cref="Microsoft.Xrm.Sdk.Messages.DisassociateRequest"/> и др.
        /// </remarks>
        /// <example>
        /// <code>
        /// public class MyPlugin : IPlugin
        /// {
        ///     public void Execute(IServiceProvider serviceProvider)
        ///     {
        ///         ...
        ///         var context = serviceProvider.GetContext();
        ///         var entityRef = context.GetContextEntityReference();
        ///         ...
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="IServiceProviderExtension"/>
        public static EntityReference GetContextEntityReference(this IPluginExecutionContext context)
        {
            // Delete message
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference)
                return (EntityReference)context.InputParameters["Target"];
            // QualifyLead message
            if (context.InputParameters.Contains("LeadId") && context.InputParameters["LeadId"] is EntityReference)
                return (EntityReference)context.InputParameters["LeadId"];
            // SetStateDynamicEntity message
            if (context.InputParameters.Contains("EntityMoniker") && context.InputParameters["EntityMoniker"] is EntityReference)
                return (EntityReference)context.InputParameters["EntityMoniker"];
            const string ERROR_MESSAGE = "EntityReference is not accessible in a context of {0} {1} event.";
            throw new PluginConfigurationException(string.Format(ERROR_MESSAGE, ((Stage)context.Stage).GetDisplayName(), context.MessageName));
        }



        /// <summary>
        /// Получение Pre Image сущности с именем по умолчанию.
        /// </summary>
        /// <param name="context">Контекст плагина.</param>
        /// <returns>Метод возвращает Pre Image сущности с именем по умолчанию. Имя по умолчанию - "Image".</returns>
        /// <exception cref="System.Exception">В шаге плагина не определен Image с именем по умолчанию.</exception>
        public static Entity GetDefaultPreEntityImage(this IPluginExecutionContext context)
        {
            const string DEFAILT_IMAGE_NAME = DefaultImageName;
            if (context.PreEntityImages.ContainsKey(DEFAILT_IMAGE_NAME))
                return context.PreEntityImages[DEFAILT_IMAGE_NAME];
            var entityName = context.PrimaryEntityName;
            var stageName = ((Stage)context.Stage).GetDisplayName();
            var message = $@"Для сущности {entityName} на шаге {stageName} не определен Image с именем ""{DEFAILT_IMAGE_NAME}"".";
            throw new PluginConfigurationException(message);
        }


        /// <summary>
        /// Получение Post Image сущности с именем по умолчанию.
        /// </summary>
        /// <param name="context">Контекст плагина.</param>
        /// <returns>Метод возвращает Post Image сущности с именем по умолчанию. Имя по умолчанию - "Image".</returns>
        /// <exception cref="System.Exception">В шаге плагина не определен Image с именем по умолчанию.</exception>
        public static Entity GetDefaultPostEntityImage(this IPluginExecutionContext context)
        {
            const string DEFAILT_IMAGE_NAME = DefaultImageName;
            if (context.PostEntityImages.ContainsKey(DEFAILT_IMAGE_NAME))
                return context.PostEntityImages[DEFAILT_IMAGE_NAME];
            var entityName = context.PrimaryEntityName;
            var stageName = ((Stage)context.Stage).GetDisplayName();
            var message = $@"Для сущности {entityName} на шаге {stageName} не определен Image с именем ""{DEFAILT_IMAGE_NAME}"".";
            throw new PluginConfigurationException(message);
        }
    }
}