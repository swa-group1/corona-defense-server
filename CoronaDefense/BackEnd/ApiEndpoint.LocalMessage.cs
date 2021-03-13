// unset

namespace BackEnd
{
  using BackEnd.ModelEvents;

  internal partial class ApiEndpoint
  {
    internal interface ILocalMessage
    {
      readonly GameAdress GameAdress;
    }
  }
}
