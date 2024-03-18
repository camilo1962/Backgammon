using System.Text;

public class ArrayHelper
{
    public static string ConvertToString<T>(T[] array)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < array.Length; i++)
        {
            if (i == 0)
            {
                sb.AppendFormat("[{0}", array[i]);
            }
            else
            {
                sb.AppendFormat(", {0}", array[i]);
            }

            if (i == array.Length-1)
            {
                sb.Append("]");
            }
        }

        return sb.ToString();
    }
}