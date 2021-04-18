// <copyright file="StorageAPI.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;
using System.Net;

namespace BackEnd
{
  /// <summary>
  /// Interacts with Google Firebase.
  /// </summary>
  internal class StorageAPI
  {
    /// <summary>
    /// URL of storage bucket in FireBase project.
    /// </summary>
    private const string FirebaseStorageUrl = "https://firebasestorage.googleapis.com/v0/b/coronadefense-1.appspot.com/o/";

    /// <summary>
    /// GET parameters for standard requests to JSON files in the FireBase Storage bucket.
    /// </summary>
    private const string GetParameters = "alt=media";

    /// <summary>
    /// Download the newest JSON file for enemy definitions.
    /// </summary>
    /// <returns>The content of the file.</returns>
    public static string DownloadEnemies()
    {
      using WebClient client = new WebClient();
      return client.DownloadString($"{FirebaseStorageUrl}enemies.json?{GetParameters}");
    }

    /// <summary>
    /// Download the newest JSON file for round definitions.
    /// </summary>
    /// <returns>The content of the file.</returns>
    public static string DownloadRounds()
    {
      using WebClient client = new WebClient();
      return client.DownloadString($"{FirebaseStorageUrl}rounds.json?{GetParameters}");
    }

    /// <summary>
    /// Download stage with supplied <paramref name="stageNumber"/>.
    /// </summary>
    /// <param name="stageNumber">Number of stage to load.</param>
    /// <returns>The content of the file.</returns>
    public static string DownloadStage(int stageNumber)
    {
      if (stageNumber < 0 || 999 < stageNumber)
      {
        throw new ArgumentException($"{nameof(stageNumber)} cannot be outside the range 0-999.");
      }

      using WebClient client = new WebClient();
      string stageNumberText = stageNumber.ToString().PadLeft(3, '0');
      return client.DownloadString($"{FirebaseStorageUrl}stage_{stageNumberText}.json?{GetParameters}");
    }

    /// <summary>
    /// Download the newest JSON schema file for stages.
    /// </summary>
    /// <returns>The content of the file.</returns>
    public static string DownloadStageSchema()
    {
      using WebClient client = new WebClient();
      return client.DownloadString($"{FirebaseStorageUrl}stage_schema.json?{GetParameters}");
    }

    /// <summary>
    /// Download the newest JSON file for tower types.
    /// </summary>
    /// <returns>The content of the file.</returns>
    public static string DownloadTowers()
    {
      using WebClient client = new WebClient();
      return client.DownloadString($"{FirebaseStorageUrl}towers.json?{GetParameters}");
    }
  }
}
