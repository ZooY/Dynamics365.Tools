using iTextSharp.text.pdf;


namespace PZone.PdfTools
{
    /// <summary>
    /// Расширение класса <see cref="PdfReader"/>.
    /// </summary>
    public static class PdfReaderExtension
    {
        /// <summary>
        /// Получение всех шрифтов формы.
        /// </summary>
        /// <param name="reader">Объект чтения из шаблона PDF.</param>
        /// <returns>
        /// Метод возвращает список всех шрифтов формы.
        /// </returns>
        public static PdfDictionary GetFormFonts(this PdfReader reader)
        {
            var acroForm = (PdfDictionary)PdfReader.GetPdfObject(reader.Catalog.Get(PdfName.ACROFORM));
            var dr = acroForm.GetAsDict(PdfName.DR);
            return dr.GetAsDict(PdfName.FONT);
        }
    }
}