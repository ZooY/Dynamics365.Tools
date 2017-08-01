using System.Linq;
using iTextSharp.text.pdf;


namespace PZone.PdfTools
{
    /// <summary>
    /// Расширение класса <see cref="AcroFields"/>.
    /// </summary>
    public static class AcroFieldsExtension
    {
        /// <summary>
        /// Установка значения текстового поля с использованием шрифта, определенного для этого поля или первого подходящего шрифта, при невозможности использования шрифта поля..
        /// </summary>
        /// <param name="form">Объект формы.</param>
        /// <param name="reader">Объект чтения из шаблона PDF.</param>
        /// <param name="fonts">Список всех шрифтов формы.</param>
        /// <param name="fieldName">Имя поля формы.</param>
        /// <param name="value">Значение поля формы.</param>
        public static void SetFieldWithFont(this AcroFields form, PdfReader reader, PdfDictionary fonts, string fieldName, string value)
        {
            var fontName = FindFieldFontName(reader, fieldName);
            if (!string.IsNullOrWhiteSpace(fontName))
            {
                var font = FindFontInFontDict(fonts, fontName);
                if (font != null)
                    form.SetFieldProperty(fieldName, "textfont", font, null);
            }
            form.SetField(fieldName, value);
        }


        private static string FindFieldFontName(PdfReader reader, string fieldName)
        {
            var merged = reader.AcroFields.GetFieldItem(fieldName).GetMerged(0);
            var da = merged.GetAsString(PdfName.DA);
            return da?.ToString();
        }


        private static BaseFont FindFontInFontDict(PdfDictionary fontDict, string sourceFieldFontName)
        {
            var fieldFontName = fontDict.Keys.FirstOrDefault(f => sourceFieldFontName.StartsWith(f.ToString()));
            var iRef = (PRIndirectReference)fontDict.GetAsIndirectObject(fieldFontName);
            var font = iRef != null ? BaseFont.CreateFont(iRef) : null;
            if (!string.IsNullOrWhiteSpace(font?.Encoding))
                return font;
            foreach (var fontName in fontDict.Keys)
            {
                iRef = (PRIndirectReference)fontDict.GetAsIndirectObject(fontName);
                font = iRef != null ? BaseFont.CreateFont(iRef) : null;
                if (!string.IsNullOrWhiteSpace(font?.Encoding))
                    return font;
            }
            return null;
        }
    }
}