using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Engine
{
    public class Movement : BaseComponent
    {
        float speed;
        Entity player;
        Transition playerTransition;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            this.speed = 200;
            this.player = entity.entityManager.GetEntityByTag("Player");
            this.playerTransition = player.GetComponent<Transition>();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 move = new Vector2(0, 0);
            KeyboardState state = Keyboard.GetState();
            float speed = this.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (state.IsKeyDown(Keys.A))
            {
                move.X -= speed;
            }
            if (state.IsKeyDown(Keys.D))
            {
                move.X += speed;
            }
            if (state.IsKeyDown(Keys.S))
            {
                move.Y -= speed;
            }
            if (state.IsKeyDown(Keys.W))
            {
                move.Y += speed;
            }

            this.playerTransition.position += move;
            Console.WriteLine(this.playerTransition.position);
        }
    }
}
