using System;
using Microsoft.Xna.Framework;


namespace TerrariaCopy.Engine
{
    public class Tools 
    {
        public static EntityManager? currentManager;

        public static Entity Instantiate(Prefab prefab, Vector2 position)
        {
            EntityManager? currentManager = Tools.currentManager;

            if (currentManager == null) 
            {
                throw new Exception("Current manager not specified");
            }

            Entity entity = currentManager.Create(
                prefab.tag, prefab.GetComponents()
            );

            Transition? transition = entity.GetComponent<Transition>();

            if (transition == null)
            {
                transition = entity.AddComponent<Transition>();
            }

            transition.position = position;

            return entity;
        }
    }
}
