using System.Text;
using Microsoft.Xrm.Sdk;


namespace PZone.Common.Testing
{
    public class FakseTracingService : ITracingService
    {
        public void Trace(string format, params object[] args)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Сообщение в Trasing Service ===");
            if (args == null || args.Length < 1)
                sb.AppendLine(format);
            else
                sb.AppendLine(string.Format(format, args));
            System.Diagnostics.Trace.WriteLine(sb.ToString());
        }
    }
}