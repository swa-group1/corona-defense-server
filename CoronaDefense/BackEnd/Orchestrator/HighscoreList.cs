// <copyright file="HighscoreList.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System.Collections.Generic;

namespace BackEnd.Orchestrator
{
  /// <summary>
  /// A single instance of a highscore list.
  /// </summary>
  internal class HighscoreList
  {
    private const int HighscoreListSize = 10;

    /// <summary>
    /// Gets entries of this <see cref="HighscoreList"/>, in order.
    /// </summary>
    public IList<Entry> Entries { get; init; }

    /// <summary>
    /// Create a new <see cref="HighscoreList"/> object.
    /// </summary>
    /// <param name="jsonContent">JSON text to parse into <see cref="HighscoreList"/>.</param>
    /// <returns>The parsed <see cref="HighscoreList"/>.</returns>
    public static HighscoreList Parse(string jsonContent)
    {
      return JsonConvert.DeserializeObject<HighscoreList>(jsonContent);
    }

    /// <summary>
    /// Register the score of a lobby to this <see cref="HighscoreList"/>.
    /// </summary>
    /// <param name="name">Name of lobby.</param>
    /// <param name="score">Score the lobby achieved.</param>
    /// <returns>The placement the score achieved on the highscore list; <see langword="0"/> if the score did not make the highscore list.</returns>
    public int RegisterScore(string name, int score)
    {
      int oldCount = this.Entries.Count;
      int insertPlace = oldCount + 1;
      for (int place = 1; place <= oldCount; place++)
      {
        if (this.Entries[place - 1].Score < score)
        {
          // Score should be inserted at current place.
          insertPlace = place;
          break;
        }
      }

      this.Entries.Insert(insertPlace - 1, new Entry() { Name = name, Score = score, });
      while (HighscoreListSize < this.Entries.Count)
      {
        this.Entries.RemoveAt(this.Entries.Count - 1);
      }

      return insertPlace % (HighscoreListSize + 1);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }

    /// <summary>
    /// The entry of one lobby on a <see cref="HighscoreList"/>.
    /// </summary>
    public class Entry
    {
      /// <summary>
      /// Gets name of the lobby this <see cref="Entry"/> belongs to.
      /// </summary>
      public string Name { get; init; }

      /// <summary>
      /// Gets score achieved in the game this <see cref="Entry"/> represents.
      /// </summary>
      public int Score { get; init; }
    }
  }
}
