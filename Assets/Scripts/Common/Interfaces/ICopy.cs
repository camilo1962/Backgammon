
namespace Common
{
    public interface ICopy<T>
    {
        /// <summary>
        /// Returns a deep copy of this instance.
        /// </summary>
        /// <returns></returns>
        T Copy();
    }
}