using System.Collections.Generic;


namespace Engine
{
    public class Prefab
    {
        public string tag;
        public List<BaseComponent> components;

        public Prefab()
        { 
            this.tag = "Entity";
            this.components = new List<BaseComponent>();
        }

        public Prefab(string tag)
        {
            this.tag = tag;
            this.components = new List<BaseComponent>();
        }

        public Prefab(string tag, List<BaseComponent> components) 
        {
            this.tag = tag;
            this.components = components;
        }

        public List<BaseComponent> GetComponents()
        {
            List<BaseComponent> componentsCopy = new List<BaseComponent>(this.components);
            
            for (int i = 0; i < this.components.Count; i++)
            {
                componentsCopy[i] = (BaseComponent) this.components[i].Clone();
            }

            return componentsCopy;
        }
    }
}
