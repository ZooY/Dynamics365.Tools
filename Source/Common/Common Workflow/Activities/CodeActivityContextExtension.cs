using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;


namespace PZone.Activities
{
    /// <summary>
    /// Расширение стандартного функционала класса <see cref="CodeActivityContext"/>.
    /// </summary>
    // ReSharper disable once CheckNamespace
    public static class CodeActivityContextExtension
    {
        /// <summary>
        /// Получение экземплпра CRM-сервиса.
        /// </summary>
        /// <param name="executionContext">Экземпляр класса <see cref="CodeActivityContext"/>.</param>
        /// <returns>
        /// Метод возвращает ссылку на экземпляр CRM-сервиса, запусщенного от имени пользователя, инициировавшего запуск действия процесса.
        /// </returns>
        public static IOrganizationService GetService(this CodeActivityContext executionContext)
        {
            var context = executionContext.GetContext();
            return executionContext.GetService(context.UserId);
        }


        /// <summary>
        /// Получение экземпляра CRM-сервиса.
        /// </summary>
        /// <param name="executionContext">Экземпляр класса <see cref="CodeActivityContext"/>.</param>
        /// <param name="userId">Идентификатор пользователя, от имени которого будет выполняться сервис.</param>
        /// <returns>
        /// Метод возвращает ссылку на экземпляр CRM-сервиса, запусщенного от имени указанного пользователя.
        /// </returns>
        public static IOrganizationService GetService(this CodeActivityContext executionContext, Guid userId)
        {
            return executionContext.GetExtension<IOrganizationServiceFactory>().CreateOrganizationService(userId);
        }




        /// <summary>
        /// Получение экземпляра сервиса трассировки.
        /// </summary>
        /// <param name="context">Экземпляр класса <see cref="CodeActivityContext"/>.</param>
        /// <returns>
        /// Метод возвращает ссылку на экземпляр сервиса трассировки.
        /// </returns>
        /// <example>
        /// <code>
        /// protected override void Execute(CodeActivityContext context)
        /// {
        ///     ...
        ///     var tracingService = context.GetTracingService();
        ///     ...
        /// }
        /// </code>
        /// </example>
        public static ITracingService GetTracingService(this CodeActivityContext context)
        {
            return context.GetExtension<ITracingService>();
        }


        /// <summary>
        /// Получение контекста действия процесса.
        /// </summary>
        /// <param name="context">Экземпляр класса <see cref="CodeActivityContext"/>.</param>
        /// <returns>
        /// Метод возвращает объект, представляющий собой контекст выполнени¤ действия процесса.
        /// </returns>
        /// <example>
        /// <code>
        /// protected override void Execute(CodeActivityContext context)
        /// {
        ///     ...
        ///     var context = context.GetContext();
        ///     ...
        /// }
        /// </code>
        /// </example>
        public static IWorkflowContext GetContext(this CodeActivityContext context)
        {
            return context.GetExtension<IWorkflowContext>();
        }


        ///// <summary>
        ///// Данные класса в формате JSON.
        ///// </summary>
        ///// <param name="context">Экземпляр класса <see cref="CodeActivityContext"/>.</param>
        ///// <returns>
        ///// Метод возвращет строку, содержащую все данные класса в формате JSON.
        ///// </returns>
        //public static string ToJsonString(this CodeActivityContext context)
        //{
        //    return (new JavaScriptSerializer()).Serialize(context);
        //}
    }
}
