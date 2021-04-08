using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Schemas
{
  /// <summary>
  /// List of the available lobbies.
  /// </summary>
  public class LobbyList
  {
    /// <summary>
    /// Gets or sets list of available lobbies.
    /// </summary>
    [Required]
    public List<Lobby> Lobbies { get; set; } = new List<Lobby>();
  }
}
