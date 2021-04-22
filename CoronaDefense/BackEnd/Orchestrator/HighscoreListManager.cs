// <copyright file="HighscoreListManager.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;

namespace BackEnd.Orchestrator
{
  /// <summary>
  /// List to add highscores to.
  /// </summary>
  internal sealed class HighscoreListManager
  {
    private const string Filename = "HighscoreList.json";

    private static readonly Lazy<HighscoreListManager>
        Lazy = new Lazy<HighscoreListManager>(() =>
        {
          return new HighscoreListManager();
        });

    /// <summary>
    /// Gets singleton instance of <see cref="HighscoreListManager"/>.
    /// </summary>
    public static HighscoreListManager Instance
    {
      get { return Lazy.Value; }
    }

    private HighscoreListManager()
    {
    }

    /// <summary>
    /// Register the score of a lobby to this <see cref="HighscoreListManager"/>.
    /// </summary>
    /// <param name="name">Name of lobby.</param>
    /// <param name="score">Score the lobby achieved.</param>
    /// <returns>The placement the score achieved on the highscore list; <see langword="0"/> if the score did not make the highscore list.</returns>
    public int RegisterScore(string name, int score)
    {
      lock (this)
      {
        // Parse current highscore list
        HighscoreList highscoreList;
        if (File.Exists(Filename))
        {
          string fileContent = File.ReadAllText(Filename);
          highscoreList = HighscoreList.Parse(fileContent);
        }
        else
        {
          highscoreList = new HighscoreList() { Entries = new List<HighscoreList.Entry>(), };
        }

        // Find placement in highscore list and save
        int placement = highscoreList.RegisterScore(name, score);
        if (placement != 0)
        {
          File.WriteAllText(Filename, highscoreList.ToString());
        }

        return placement;
      }
    }
  }
}
