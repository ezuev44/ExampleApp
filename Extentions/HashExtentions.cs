using System.Text.Json;
using System.Text;

namespace Extentions
{
    public static class HashExtentions
    {
        public static int GetStableHashCode(this object obj)
        {
            unchecked
            {
                byte[] bytes = ObjectToByteArray(obj);
                int result = 17;
                foreach (byte b in bytes)
                    result = result * 23 + b;
                return result;
            }
        }

        private static byte[] ObjectToByteArray(object obj)
        {
            string jsonString = JsonSerializer.Serialize(obj);
            return Encoding.UTF8.GetBytes(jsonString);
        }
    }
}
