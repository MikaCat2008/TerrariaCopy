using System;
using Microsoft.Xna.Framework.Graphics;


namespace TerrariaCopy.Engine
{
    public abstract class IRenderScript
    {
        public Sprite sprite;

        public virtual void Initialize(Sprite sprite) 
        {
            this.sprite = sprite;
        }

        public abstract Texture2D Render();
    }

    public class Sprite2DRenderScript : IRenderScript
    {
        public Texture2D texture;

        public Sprite2DRenderScript(Texture2D texture) 
        {
            this.texture = texture;
        }

        public override Texture2D Render() 
        {
            return this.texture;
        }
    }

    public class Sprite : BaseComponent
    {
        public Texture2D rendered;
        public IRenderScript renderScript;

        public static GraphicsDevice graphicsDevice;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            this.renderScript.Initialize(this);
        }

        public void Render()
        {
            this.rendered = this.renderScript.Render();
        }
    }
}
