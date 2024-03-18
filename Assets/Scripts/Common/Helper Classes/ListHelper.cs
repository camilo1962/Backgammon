using System.Collections.Generic;
using System.Text;

public class ListHelper
{
    public static string ConvertToString<T>(List<T> list)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < list.Count; i++)
        {
            if (i == 0)
            {
                sb.AppendFormat("[{0}", list[i]);
            }
            else
            {
                sb.AppendFormat(", {0}", list[i]);
            }

            if (i == list.Count - 1)
            {
                sb.Append("]");
            }
        }

        return sb.ToString();
    }
}