using System.ComponentModel;


namespace PZone.Common.Plugins
{
    /// <summary>
    /// ���������� ����������� ������������ <see cref="Stage"/>.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once CheckNamespace
    public static class StageExtension
    {
        /// <summary>
        /// ��������� ������������� ����� �������� ������������.
        /// </summary>
        /// <param name="stage">�������� ������������.</param>
        /// <returns>
        /// ����� ���������� �������� �����������, ������� ����� ���������� �������� �������������.
        /// </returns>
        public static string GetDisplayName(this Stage stage)
        {
            var memInfo = (typeof(Stage)).GetMember(stage.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}