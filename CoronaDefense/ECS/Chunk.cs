using System.Collections.Generic;

namespace ECS
{
    // TODO: Doc
    public class Chunk
    {
        // TODO: Doc
        public readonly int componentSize;

        // TODO: Doc
        public List<byte> components;

        // TODO: Doc
        public Chunk(IComponent component)
        {
            this.componentSize = component.Size;
            this.components = new List<byte>(component.ToBytes());
        }
    }
}
