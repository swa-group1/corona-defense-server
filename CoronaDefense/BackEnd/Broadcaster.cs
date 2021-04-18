// <copyright file="Broadcaster.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace BackEnd
{
  /// <summary>
  /// Broadcaster for a <see cref="Lobby"/>. Functionality to send game events to all connected clients.
  /// </summary>
  internal class Broadcaster
  {
    /// <summary>
    /// Gets <see cref="ConnectionBroker"/> that this <see cref="Broadcaster"> should get <see cref="Socket"/>s from.
    /// </summary>.
    private ConnectionBroker ConnectionBroker { get; }

    /// <summary>
    /// Gets dictionary connecting access tokens to sockets.
    /// </summary>
    private Dictionary<long, Socket> Sockets { get; } = new Dictionary<long, Socket>();

    private static byte[] PingBuffer { get; } = new byte[]
    {
      0x10, // Byte code (UByte)
      0x02, // Length (UByte)
      0x00, // Major version (UByte)
      0x01, // Minor version (UByte)
    };

    private byte[] FightRoundBuffer { get; } = new byte[]
    {
      0x20, // Byte code (UByte)
      0x02, // Length (UByte)
      0x00, // Round number (UShort)
      0x00,
    };

    private byte[] GameModeBuffer { get; } = new byte[]
    {
      0x21, // Byte code (UByte)
      0x01, // Length (UByte)
      0x00, // Stage number (UByte)
    };

    private byte[] InputRoundBuffer { get; } = new byte[]
    {
      0x22, // Byte code (UByte)
      0x02, // Length (UByte)
      0x00, // Round number (UShort)
      0x00,
    };

    private static byte[] LobbyModeBuffer { get; } = new byte[]
    {
      0x23, // Byte code (UByte)
      0x00, // Length (UByte)
    };

    private byte[] HealthUpdateBuffer { get; } = new byte[]
    {
      0x30, // Byte code (UByte)
      0x02, // Length (UByte)
      0x00, // New value (UShort)
      0x00,
    };

    private byte[] MoneyUpdateBuffer { get; } = new byte[]
    {
      0x31, // Byte code (UByte)
      0x04, // Length (UByte)
      0x00, // New value (UInt)
      0x00,
      0x00,
      0x00,
    };

    private byte[] PlayerCountUpdateBuffer { get; } = new byte[]
    {
      0x32, // Byte code (UByte)
      0x01, // Length (UByte)
      0x00, // Player count (UByte)
    };

    private byte[] TowerPositionBuffer { get; } = new byte[]
    {
      0x33, // Byte code (UByte)
      0x05, // Length (UByte)
      0x00, // Tower ID (UShort)
      0x00,
      0x00, // Type number (UByte)
      0x00, // X position (UByte)
      0x00, // Y position (UByte)
    };

    private byte[] TowerRemovedBuffer { get; } = new byte[]
    {
      0x40, // Byte code (UByte)
      0x02, // Length (UByte)
      0x00, // Tower ID (UShort)
      0x00,
    };

    private byte[] AnimationConfirmationBuffer { get; } = new byte[]
    {
      0x50, // Byte code (UByte)
      0x04, // Length (UByte)
      0x00, // Time (Float)
      0x00,
      0x00,
      0x00,
    };

    private byte[] BoardToPathAnimationBuffer { get; } = new byte[]
    {
      0x51, // Byte code (UByte)
      0x10, // Length (UByte)
      0x00, // Sprite number (UByte)
      0x00, // Start position x (UByte)
      0x00, // Start position y (UByte)
      0x00, // End position (Float)
      0x00,
      0x00,
      0x00,
      0x00, // Start time (Float)
      0x00,
      0x00,
      0x00,
      0x00, // End time (Float)
      0x00,
      0x00,
      0x00,
      0x00, // Result animation (UByte)
    };

    private byte[] PathToPathAnimationBuffer { get; } = new byte[]
    {
      0x52, // Byte code (UByte)
      0x12, // Length (UByte)
      0x00, // Sprite number (UByte)
      0x00, // Start position (Float)
      0x00,
      0x00,
      0x00,
      0x00, // End position (Float)
      0x00,
      0x00,
      0x00,
      0x00, // Start time (Float)
      0x00,
      0x00,
      0x00,
      0x00, // End time (Float)
      0x00,
      0x00,
      0x00,
      0x00, // Result animation (UByte)
    };

    private byte[] TowerAnimationBuffer { get; } = new byte[]
    {
      0x53, // Byte code (UByte)
      0x04, // Length (UByte)
      0x00, // Tower ID (UShort)
      0x00,
      0x00, // Animation number (UByte)
      0x00, // Rotation (Byte)
    };

    private byte[] HealthAnimationBuffer { get; } = new byte[]
    {
      0x54, // Byte code (UByte)
      0x06, // Length (UByte)
      0x00, // New Value (UShort)
      0x00,
      0x00, // Time (Float)
      0x00,
      0x00,
      0x00,
    };

    private byte[] MoneyAnimationBuffer { get; } = new byte[]
    {
      0x55, // Byte code (UByte)
      0x08, // Length (UByte)
      0x00, // New Value (UInt)
      0x00,
      0x00,
      0x00,
      0x00, // Time (Float)
      0x00,
      0x00,
      0x00,
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="Broadcaster"/> class.
    /// </summary>
    /// <param name="connectionBroker"><see cref="ConnectionBroker"/> that the new <see cref="Broadcaster"> should get <see cref="Socket"/>s from.</param>.
    public Broadcaster(ConnectionBroker connectionBroker)
    {
      this.ConnectionBroker = connectionBroker;
    }

    /// <summary>
    /// Send supplied <paramref name="buffer"/> to all <see cref="sockets"/>.
    /// </summary>
    /// <param name="buffer">Buffer to send to <see cref="sockets"/>.</param>
    private void Broadcast(byte[] buffer)
    {
      foreach (KeyValuePair<long, Socket> pair in this.Sockets)
      {
        try
        {
          _ = pair.Value.Send(buffer);
        }
        catch (Exception)
        {
          pair.Value.Dispose();
          _ = this.Sockets.Remove(pair.Key);
        }
      }
    }

    /// <summary>
    /// Disconnect socket associated with supplied <paramref name="accessToken"/>.
    /// </summary>
    /// <param name="accessToken">Access token of connection to close.</param>
    internal void DisconnectClient(long accessToken)
    {
      if (!this.Sockets.TryGetValue(accessToken, out Socket clientSocket))
      {
        return;
      }

      clientSocket.Close();
      _ = this.Sockets.Remove(accessToken);
    }

    /// <summary>
    /// Write a <see cref="float"> to supplied <paramref name="buffer"/> starting at a specific index.
    /// </summary>
    /// <param name="buffer">Buffer to write to.</param>
    /// <param name="startIndex">Index of first byte in buffer to write to.</param>
    /// <param name="data">Data to write.</param>
    private void SetFloat(byte[] buffer, int startIndex, float data)
    {
      byte[] bytes = BitConverter.GetBytes(data);
      bytes.CopyTo(buffer, startIndex);
    }

    /// <summary>
    /// Write a <see cref="int"> to supplied <paramref name="buffer"/> starting at a specific index.
    /// </summary>
    /// <param name="buffer">Buffer to write to.</param>
    /// <param name="startIndex">Index of first byte in buffer to write to.</param>
    /// <param name="data">Data to write.</param>
    private void SetInt(byte[] buffer, int startIndex, int data)
    {
      byte[] bytes = BitConverter.GetBytes(data);

      if (BitConverter.IsLittleEndian)
      {
        Array.Reverse(bytes);
      }

      bytes.CopyTo(buffer, startIndex);
    }

    /// <summary>
    /// Write a <see cref="short"> to supplied <paramref name="buffer"/> starting at a specific index.
    /// </summary>
    /// <param name="buffer">Buffer to write to.</param>
    /// <param name="startIndex">Index of first byte in buffer to write to.</param>
    /// <param name="data">Data to write.</param>
    private void SetShort(byte[] buffer, int startIndex, short data)
    {
      byte[] bytes = BitConverter.GetBytes(data);

      if (BitConverter.IsLittleEndian)
      {
        Array.Reverse(bytes);
      }

      bytes.CopyTo(buffer, startIndex);
    }

    /// <summary>
    /// Attempt to associate client with specific <paramref name="accessToken"/> to connection with supplied <paramref name="connectionNumebr"/>.
    /// </summary>
    /// <param name="accessToken">Access token to associate with the retrieved <see cref="Socket"/>.</param>
    /// <param name="connectionNumber">Connection number of connection to claim from <see cref="ConnectionBroker"/>.</param>
    /// <returns><see langword="true"/> if connection was associated with <paramref name="accessToken"/>.</returns>
    internal bool TryAssociateWithConnection(long accessToken, long connectionNumber)
    {
      if (!this.ConnectionBroker.TryClaimConnection(connectionNumber, out Socket clientSocket))
      {
        return false;
      }

      this.Sockets.Add(accessToken, clientSocket);
      return true;
    }

    /// <summary>
    /// Simple broadcast method used to test connection to clients.
    /// </summary>
    internal void Ping()
    {
      this.Broadcast(PingBuffer);
    }

    /// <summary>
    /// Broadcast that the game is in a fighting round.
    /// </summary>
    /// <param name="roundNumber">Number of current round.</param>
    internal void FightRound(short roundNumber)
    {
      this.SetShort(this.FightRoundBuffer, 2, roundNumber);
      this.Broadcast(this.FightRoundBuffer);
    }

    /// <summary>
    /// Broadcast signaling that the game is playing.
    /// </summary>
    /// <param name="stageNumber">Number of the stage that is being played.</param>
    internal void GameMode(byte stageNumber)
    {
      this.GameModeBuffer[2] = stageNumber;
      this.Broadcast(this.GameModeBuffer);
    }

    /// <summary>
    /// Broadcast that the game is in input mode.
    /// </summary>
    /// <param name="roundNumber">Number of current round.</param>
    internal void InputRound(short roundNumber)
    {
      this.SetShort(this.InputRoundBuffer, 2, roundNumber);
      this.Broadcast(this.InputRoundBuffer);
    }

    /// <summary>
    /// Broadcast that the game is currently in lobby mode.
    /// </summary>
    internal void LobbyMode()
    {
      this.Broadcast(LobbyModeBuffer);
    }

    /// <summary>
    /// Broadcast that the value of the player health has changed.
    /// </summary>
    /// <param name="newValue">The new value of the player health.</param>
    internal void HealthUpdate(short newValue)
    {
      this.SetShort(this.HealthUpdateBuffer, 2, newValue);
      this.Broadcast(this.HealthUpdateBuffer);
    }

    /// <summary>
    /// Broadcast that the value of the players money has changed.
    /// </summary>
    /// <param name="newValue">The new value of the players money.</param>
    internal void MoneyUpdate(int newValue)
    {
      this.SetInt(this.InputRoundBuffer, 2, newValue);
      this.Broadcast(this.MoneyUpdateBuffer);
    }

    /// <summary>
    /// Broadcast that the number of players in the game has changed.
    /// </summary>
    /// <param name="playerCount">The new number of players in the game.</param>
    internal void PlayerCountUpdate(byte playerCount)
    {
      this.PlayerCountUpdateBuffer[2] = playerCount;
      this.Broadcast(this.PlayerCountUpdateBuffer);
    }

    /// <summary>
    /// Broadcast the ID, type and position of a tower.
    /// </summary>
    /// <param name="id">ID of the tower.</param>
    /// <param name="type">Type number of tower.</param>
    /// <param name="x">X position of tower.</param>
    /// <param name="y">Y position of tower.</param>
    internal void TowerPosition(short id, byte type, byte x, byte y)
    {
      this.SetShort(this.TowerPositionBuffer, 2, id);
      this.TowerPositionBuffer[4] = type;
      this.TowerPositionBuffer[5] = x;
      this.TowerPositionBuffer[6] = y;
      this.Broadcast(this.TowerPositionBuffer);
    }

    /// <summary>
    /// Broadcast that a tower has been removed.
    /// </summary>
    /// <param name="towerId">The ID of the tower that has been removed.</param>
    internal void TowerRemoved(short towerId)
    {
      this.SetShort(this.TowerRemovedBuffer, 2, towerId);
      this.Broadcast(this.TowerRemovedBuffer);
    }

    /// <summary>
    /// <para>Broadcast a confirmation that the server has notified the players about all animations that take place before a certain time.</para>
    /// <para>The clients are therefore safe to render the game up to that point in time.</para>
    /// </summary>
    /// <param name="tickNumber">The tick up to which all animations have been sent.</param>
    internal void AnimationConfirmation(short tickNumber)
    {
      this.SetShort(this.AnimationConfirmationBuffer, 2, tickNumber);
      this.Broadcast(this.AnimationConfirmationBuffer);
    }

    /// <summary>
    /// Broadcast an animation that travels from the board to the path.
    /// </summary>
    /// <param name="spriteNumber">The ID of the sprite used in the animation.</param>
    /// <param name="startX">The X coordinate of the board position where the animation starts.</param>
    /// <param name="startY">The Y coordinate of the board position where the animation starts.</param>
    /// <param name="endPosition">The position on the path that the animation travels to.</param>
    /// <param name="startTime">The tick when the animation should begin.</param>
    /// <param name="endTime">The tick when the animation should end.</param>
    /// <param name="resultAnimation">
    /// <para>The ID of the animation that should be played once the animation is completed.</para>
    /// <para>The value 0x00 means that the sprite should disappear without an animation.</para>
    /// </param>
    internal void BoardToPathAnimation(
      byte spriteNumber,
      byte startX,
      byte startY,
      float endPosition,
      float startTime,
      float endTime,
      byte resultAnimation
    )
    {
      this.BoardToPathAnimationBuffer[2] = spriteNumber;
      this.BoardToPathAnimationBuffer[3] = startX;
      this.BoardToPathAnimationBuffer[4] = startY;
      this.SetFloat(this.BoardToPathAnimationBuffer, 5, endPosition);
      this.SetFloat(this.BoardToPathAnimationBuffer, 9, startTime);
      this.SetFloat(this.BoardToPathAnimationBuffer, 13, endTime);
      this.BoardToPathAnimationBuffer[17] = resultAnimation;
      this.Broadcast(this.BoardToPathAnimationBuffer);
    }

    /// <summary>
    /// An animation that should travel along the path.
    /// </summary>
    /// <param name="spriteNumber">The ID of the sprite used in the animation.</param>
    /// <param name="startPosition">The position on the path where the animation begins.</param>
    /// <param name="endPosition">The position on the path that the animation travels to.</param>
    /// <param name="startTime">The tick when the animation should begin.</param>
    /// <param name="endTime">The tick when the animation should end.</param>
    /// <param name="resultAnimation">
    /// <para>The ID of the animation that should be played once the animation is completed.</para>
    /// <para>The value 0x00 means that the sprite should disappear without an animation.</para>
    /// </param>
    internal void PathToPathAnimation(
      byte spriteNumber,
      float startPosition,
      float endPosition,
      float startTime,
      float endTime,
      byte resultAnimation
    )
    {
      this.PathToPathAnimationBuffer[2] = spriteNumber;
      this.SetFloat(this.PathToPathAnimationBuffer, 3, startPosition);
      this.SetFloat(this.PathToPathAnimationBuffer, 7, endPosition);
      this.SetFloat(this.PathToPathAnimationBuffer, 11, startTime);
      this.SetFloat(this.PathToPathAnimationBuffer, 15, endTime);
      this.PathToPathAnimationBuffer[19] = resultAnimation;
      this.Broadcast(this.PathToPathAnimationBuffer);
    }

    /// <summary>
    /// Broadcast that a tower should start an animation.
    /// </summary>
    /// <param name="towerId">ID of tower to animate.</param>
    /// <param name="animation">
    /// <para>Animation that tower should start.</para>
    /// <para>A value of 0x00 means the tower should not be animated.</para>
    /// </param>
    /// <param name="rotation">Rotation tower should assume during the animation and afterwards.</param>
    internal void TowerAnimation(short towerId, byte animation, byte rotation)
    {
      this.SetShort(this.TowerAnimationBuffer, 2, towerId);
      this.TowerAnimationBuffer[4] = animation;
      this.TowerAnimationBuffer[5] = rotation;
      this.Broadcast(this.TowerAnimationBuffer);
    }

    /// <summary>
    /// Broadcast that health should change at a specific time.
    /// </summary>
    /// <param name="newValue">New health value.</param>
    /// <param name="time">The time the new vallue should be applied.</param>
    internal void HealthAnimation(short newValue, float time)
    {
      this.SetShort(this.HealthAnimationBuffer, 2, newValue);
      this.SetFloat(this.HealthAnimationBuffer, 4, time);
      this.Broadcast(this.HealthAnimationBuffer);
    }

    /// <summary>
    /// Broadcast that money should change at a specific time.
    /// </summary>
    /// <param name="newValue">New money value.</param>
    /// <param name="time">The time the new vallue should be applied.</param>
    internal void MoneyAnimation(int newValue, float time)
    {
      this.SetInt(this.MoneyAnimationBuffer, 2, newValue);
      this.SetFloat(this.MoneyAnimationBuffer, 6, time);
      this.Broadcast(this.MoneyAnimationBuffer);
    }
  }
}
