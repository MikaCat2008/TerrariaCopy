using System;
using Microsoft.Xna.Framework;


namespace TerrariaCopy.Engine
{
    public class Movement : BaseComponent
    {
        public int x;
        public int y;
        public Transition transition;

        public override void Initialize(Entity entity)
        {
            this.transition = entity.GetComponent<Transition>();

            this.x = (int)this.transition.position.X;
            this.y = (int)this.transition.position.Y;
        }

        public override void Update(GameTime gameTime)
        {        
            float frames = BaseComponent.app.frames;
            int x = (int)(Math.Sin(frames / 10) * 100) + this.x;
            int y = (int)(Math.Cos(frames / 10) * 100) + this.y;

            this.transition.position = new Vector2(x, y);
        }
    }
}
