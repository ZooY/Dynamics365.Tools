namespace PZone.Common.Plugins
{
    /// <summary>
    /// Событие подключаемого модуля.
    /// </summary>
    public enum Message
    {
        /// <summary>
        /// Создание записи.
        /// </summary>
        Create,


        /// <summary>
        /// Обновление записи.
        /// </summary>
        Update,


        /// <summary>
        /// Удаление записи.
        /// </summary>
        Delete,


        /// <summary>
        /// Получение одной записи.
        /// </summary>
        Retrieve,


        /// <summary>
        /// Получение набора записей.
        /// </summary>
        RetrieveMultiple
    }
}