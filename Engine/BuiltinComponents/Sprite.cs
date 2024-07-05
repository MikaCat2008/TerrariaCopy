using Microsoft.Xna.Framework.Graphics;


namespace TerrariaCopy.Engine
{
    public interface IRenderScript
    {
        public Texture2D Render();
    }

    public class Sprite2DRenderScript : IRenderScript
    {
        public Texture2D texture;

        public Sprite2DRenderScript(Texture2D texture) 
        {
            this.texture = texture;
        }

        public Texture2D Render() 
        {
            return this.texture;
        }
    }

    public class Sprite : BaseComponent
    {
        public Texture2D rendered;
        public IRenderScript? renderScript;

        public static GraphicsDevice graphicsDevice;

        public void Render()
        {
            if (this.renderScript == null) 
            {
                this.renderScript = new Sprite2DRenderScript(
                    new Texture2D(Sprite.graphicsDevice, 100, 100)
                );
            }

            this.rendered = this.renderScript.Render();
        }
    }
}
