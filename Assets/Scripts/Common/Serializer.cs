using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Serializer
{
    public static void Serialize<T>(string filename, T obj)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, obj);
        stream.Close();
    }

    public static T Deserialize<T>(string filename)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);

        try
        {
            return (T)formatter.Deserialize(stream);
        }
        catch (Exception)
        {
        }
        finally
        {
            stream.Close();
        }

        return default(T);
    }
}