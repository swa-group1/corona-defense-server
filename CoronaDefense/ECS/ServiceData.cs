using System.Collections.Generic;

namespace ECS
{
  internal class ServiceData<T>
    where T : IComponent
  {
    public T[] components;

    public void Load(List<Chunk> chunks)
    {

    }
    
    public void Store()
    {

    }
  }
}
