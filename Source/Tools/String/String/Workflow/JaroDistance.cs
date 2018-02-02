using System;
using PZone.Data.Comparisons;
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
            var settings = new StringComparisonSettings
            {
                CaseSensitive = CaseSensitive.Get(context),
                AccentSensitive = AccentSensitive.Get(context)
            };
            var result = StringComparisons.JaroDistance(String1.Get(context), String2.Get(context), settings);
            Result.Set(context, (int)Math.Round(result * 100));
        }
    }
}