using System.Text;

namespace WebApiTemplate.SharedKernel.Helpers
{
    public static class ExceptionFormatter
    {
        /// <summary>
        /// Creates a formatted string representation of an exception and its inner exceptions (if any).
        /// </summary>
        /// <param name="ex">The exception to format.</param>
        /// <returns>A formatted string containing information about the exception and its inner exceptions.</returns>
        public static string CreateExceptionString(this Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            CreateExceptionString(sb, ex, String.Empty);

            return sb.ToString();
        }

        // <summary>
        /// Recursively creates a formatted string representation of an exception and its inner exceptions (if any).
        /// </summary>
        /// <param name="sb">The StringBuilder used to build the formatted exception string.</param>
        /// <param name="ex">The current exception being processed.</param>
        /// <param name="indent">The current level of indentation for formatting.</param>
        private static void CreateExceptionString(StringBuilder sb, Exception ex, string indent)
        {
            if (indent == null)
            {
                indent = String.Empty;
            }
            else if (indent.Length > 0)
            {
                sb.AppendFormat("{0}Inner ", indent);
            }

            sb.Append("Exception Found:");
            sb.AppendFormat("\n{0}Type: {1}", indent, ex.GetType().FullName);
            sb.AppendFormat("\n{0}Message: {1}", indent, ex.Message);
            sb.AppendFormat("\n{0}Source: {1}", indent, ex.Source);
            sb.AppendFormat("\n{0}Stacktrace: {1}", indent, ex.StackTrace);

            if (ex.InnerException != null)
            {
                sb.Append("\n");
                CreateExceptionString(sb, ex.InnerException, indent + "  ");
            }
        }
    }
}
