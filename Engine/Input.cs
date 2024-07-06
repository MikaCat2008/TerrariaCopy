
namespace Engine
{
    public enum Key
    {
        W, A, S, D
    }
    public class Input
    {
        public static bool GetPressed<K>() where K : Key 
        {
            return false;
        }
    }   
}
