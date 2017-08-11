using System;
using System.Activities;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.pdf;
using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using PZone.Xrm.Workflow;


namespace PZone.PdfTools.Workflow
{
    /// <summary>
    /// Заполнение шаблона PDF.
    /// </summary>
    public class FillTemplate : WorkflowBase
    {
        /// <summary>
        /// PDF-шаблон в виде строки в кодировке BASE64.
        /// </summary>
        [Input("Template PDF File (BASE64 String)")]
        [RequiredArgument]
        public InArgument<string> TemplateFileString { get; set; }


        /// <summary>
        /// Параметры для заполнения шаблона в формате строки JSON.
        /// </summary>
        [Input("Parameters (JSON String)")]
        [RequiredArgument]
        public InArgument<string> ParametersString { get; set; }


        /// <summary>
        /// Результирующий PDF-файл.
        /// </summary>
        [Output("Result PDF File (BASE64 String)")]
        public OutArgument<string> ResultFileString { get; set; }


        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var templateFileString = TemplateFileString.Get(context);
            var parametersString = ParametersString.Get(context);

            var parameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(parametersString);

            using (var templateStream = new MemoryStream(Convert.FromBase64String(templateFileString)))
            using (var templateReader = new PdfReader(templateStream))
            using (var resultStream = new MemoryStream())
            {
                using (var resultStamper = new PdfStamper(templateReader, resultStream))
                {
                    // Получаем ссылку на форму с полями.
                    var form = resultStamper.AcroFields;
                    // Получаем все шрифты формы.
                    var fonts = templateReader.GetFormFonts();
                    // Устновка значения полей
                    foreach (var parameter in parameters)
                    {
                        if (form.GetFieldType(parameter.Key) == AcroFields.FIELD_TYPE_TEXT)
                            form.SetFieldWithFont(templateReader, fonts, parameter.Key, parameter.Value.ToString());
                        else
                            form.SetField(parameter.Key, parameter.Value.ToString());
                    }
                    // Установка запрета на редактирование полей.
                    resultStamper.FormFlattening = true;
                }
                var resultArray = resultStream.ToArray();
                ResultFileString.Set(context, Convert.ToBase64String(resultArray));
            }
        }
    }
}