namespace BackEnd
{
  /// <summary>
  /// A class describing a back-end orchestrator that handles global client-requests not connected to <see cref="GameInstance"/>s. This includes creating new <see cref="GameInstance"/>s.
  /// </summary>
  internal class Orchestrator : ApiEndpoint.Observer
  {
    /// <inheritdoc/>
    internal override void GetNotified()
    {
      throw new System.NotImplementedException();
    }
  }
}
