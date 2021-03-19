using System.Collections.Generic;

namespace ECS
{
  // TODO: Doc
  public class Chunk
  {
    // TODO: Doc
    private IComponent sampleComponent;

    // TODO: Doc
    public List<byte> components;

    // TODO: Doc
    public readonly int componentSize;

    // TODO: Doc
    public Chunk(IComponent component)
    {
      this.sampleComponent = component;
      this.components = new List<byte>(component.ToBytes());
      this.componentSize = component.Size;
    }

    public IComponent FromBytes(byte[] bytes)
    {
      return this.sampleComponent.FromBytes(bytes);
    }
  }
}
