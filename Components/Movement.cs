using Engine;
using Microsoft.Xna.Framework;


namespace TerrariaCopy
{
    public class Movement : BaseComponent
    {
        Entity player;
        Transition playerTransition;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            this.player = entity.entityManager.GetEntityByTag("Player");
            this.playerTransition = player.GetComponent<Transition>();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 move = new Vector2();

            if (Input.GetPressed<Key.W>())
            {
                move.Y += 1;
            }

            this.playerTransition.position += move;
        }
    }
}
