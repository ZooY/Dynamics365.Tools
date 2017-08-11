using PZone.Xrm.Workflow;


namespace PZone.WebTools.Workflow
{
    /// <summary>
    /// Выполнение PUT-запроса к удаленному веб-сервису и отправка данных в формате JSON.
    /// </summary>
    public class PutJson : PostJson
    {
        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var url = Url.Get(context);
            var json = Json.Get(context);
            using (var client = new ExtendedWebClient())
            {
                var response = client.UploadString(url, "PUT", json ?? string.Empty);
                Response.Set(context, response);
            }
        }
    }
}