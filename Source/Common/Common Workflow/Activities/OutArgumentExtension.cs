using System.Activities;
using PZone.Common.Workflow;


namespace PZone.Activities
{
    /// <summary>
    /// Расширение стандартного функционала класса <see cref="OutArgument"/>.
    /// </summary>
    // ReSharper disable once CheckNamespace
    public static class OutArgumentExtension
    {
        /// <summary>
        /// Установка значения выходного параметра.
        /// </summary>
        /// <typeparam name="T">Тип параметра.</typeparam>
        /// <param name="argument">Экземпляр класса <see cref="OutArgument{T}"/>.</param>
        /// <param name="context">Контекст выполенения.</param>
        /// <param name="value">Устанавливаемое значение.</param>
        public static void Set<T>(this OutArgument<T> argument, Context context, T value)
        {
            argument.Set(context.SourceActivityContext, value);
        }


        /// <summary>
        /// Получение значения выходного параметра.
        /// </summary>
        /// <typeparam name="T">Тип параметра.</typeparam>
        /// <param name="argument">Экземпляр класса <see cref="InArgument{T}"/>.</param>
        /// <param name="context">Контекст выполенения.</param>
        /// <returns>Метод возвращает значение указанного входного параметра.</returns>
        public static T Get<T>(this OutArgument<T> argument, Context context)
        {
            return argument.Get<T>(context.SourceActivityContext);
        }
    }
}