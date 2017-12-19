using System;
using PZone.Xrm.Workflow;


namespace PZone.StringTools.Workflow
{
    /// <summary>
    /// Сходство Джаро между двумя строками.
    /// </summary>
    /// <remarks>
    /// <para>Расстояние Джаро между двумя последовательностями символов.</para>
    /// <para>Чем меньше расстояние, тем больше сходства имеют эти строки друг с другом.</para>
    /// <para>Результат нормируется, так что 0 означает отсутствия сходства, а 100 - точное совпадение.</para>
    /// </remarks>
    public class JaroDistance : ComparisonsBase
    {
        /// <inheritdoc />
        protected override void Execute(Context context)
        {
            var string1 = String1.Get(context) ?? "";
            var string2 = String2.Get(context) ?? "";
            var result = PZone.Data.Comparisons.StringComparisons.JaroDistance(string1, string2);
            Result.Set(context, (int)Math.Round(result * 100));
        }
    }
}