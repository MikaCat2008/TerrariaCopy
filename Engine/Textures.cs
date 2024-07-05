using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace TerrariaCopy.Engine 
{
    public class Textures
    {
        public static Texture2D Stone;

        public static void Initialize(ContentManager Content) 
        {
            Textures.Stone = Content.Load<Texture2D>("stone");
        }
    }
}
