// <copyright file="Broadcaster.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace BackEnd
{
  /// <summary>
  /// Broadcaster for a <see cref="Lobby"/>. Functionality to send game events to all connected clients.
  /// </summary>
  internal class Broadcaster
  {
    private static byte[] PingBuffer { get; } = new byte[]
    {
      0x10, // Byte code
      0x02, // Length
      0x00, // Major version
      0x00, // Minor version
    };

    private byte[] FightRoundBuffer { get; } = new byte[]
    {
      0x20, // Byte code
      0x02, // Length
      0x00, // Round number
      0x00,
    };

    private byte[] GameModeBuffer { get; } = new byte[]
    {
      0x21, // Byte code
      0x02, // Length
      0x00, // Stage number
      0x00,
    };

    private byte[] InputRoundBuffer { get; } = new byte[]
    {
      0x22, // Byte code
      0x02, // Length
      0x00, // Round number
      0x00,
    };

    private static byte[] LobbyModeBuffer { get; } = new byte[]
    {
      0x23, // Byte code
      0x00, // Length
    };

    private byte[] HealthUpdateBuffer { get; } = new byte[]
    {
      0x30, // Byte code
      0x02, // Length
      0x00, // New value
      0x00,
    };

    private byte[] MoneyUpdateBuffer { get; } = new byte[]
    {
      0x31, // Byte code
      0x04, // Length
      0x00, // New value
      0x00,
      0x00,
      0x00,
    };

    private byte[] PlayerCountUpdateBuffer { get; } = new byte[]
    {
      0x32, // Byte code
      0x01, // Length
      0x00, // Player count
    };

    private byte[] TowerPositionBuffer { get; } = new byte[]
    {
      0x33, // Byte code
      0x05, // Length
      0x00, // Tower ID
      0x00,
      0x00, // Type number
      0x00, // X position
      0x00, // Y position
    };

    private byte[] TowerRemovedBuffer { get; } = new byte[]
    {
      0x40, // Byte code
      0x02, // Length
      0x00, // Tower ID
      0x00,
    };

    private byte[] AnimationConfirmationBuffer { get; } = new byte[]
    {
      0x50, // Byte code
      0x02, // Length
      0x00, // Time
      0x00,
    };

    private byte[] BoardToPathAnimationBuffer { get; } = new byte[]
    {
      0x51, // Byte code
      0x0A, // Length
      0x00, // Sprite number
      0x00, // Start position x
      0x00, // Start position y
      0x00, // End position
      0x00,
      0x00, // Start time
      0x00,
      0x00, // End time
      0x00,
      0x00, // Result animation
    };

    private byte[] PathToPathAnimationBuffer { get; } = new byte[]
    {
      0x52, // Byte code
      0x0A, // Length
      0x00, // Sprite number
      0x00, // Start position
      0x00,
      0x00, // End position
      0x00,
      0x00, // Start time
      0x00,
      0x00, // End time
      0x00,
      0x00, // Result animation
    };

    private byte[] TowerAnimationBuffer { get; } = new byte[]
    {
      0x53, // Byte code
      0x04, // Length
      0x00, // Tower ID
      0x00,
      0x00, // Animation number
      0x00, // Rotation
    };

    /// <summary>
    /// Dictionary connecting access tokens to sockets.
    /// </summary>
    private readonly Dictionary<long, Socket> sockets = new Dictionary<long, Socket>();

    /// <summary>
    /// Send supplied <paramref name="buffer"/> to all <see cref="sockets"/>.
    /// </summary>
    /// <param name="buffer">Buffer to send to <see cref="sockets"/>.</param>
    private void Broadcast(byte[] buffer)
    {
      foreach (Socket socket in this.sockets.Values)
      {
        socket.Send(buffer);
      }
    }

    /// <summary>
    /// Initializes a new socket and connects to client if the client is not already connected.
    /// </summary>
    /// <param name="accessToken">Access token to associate with the new <see cref="Socket"/>.</param>
    /// <param name="address">Address to connect to.</param>
    internal void ConnectTo(long accessToken, EndPoint address)
    {
      if (this.sockets.ContainsKey(accessToken))
      {
        throw new ArgumentException($"Access token already registered in this {nameof(Broadcaster)}.");
      }

      Socket socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
      socket.Connect(address);
      this.sockets.Add(accessToken, socket);
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
      this.FightRoundBuffer[3] = (byte)roundNumber;
      roundNumber >>= 8;
      this.FightRoundBuffer[2] = (byte)roundNumber;
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
      this.InputRoundBuffer[3] = (byte)roundNumber;
      roundNumber >>= 8;
      this.InputRoundBuffer[2] = (byte)roundNumber;
      this.Broadcast(this.FightRoundBuffer);
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
      this.HealthUpdateBuffer[3] = (byte)newValue;
      newValue >>= 8;
      this.HealthUpdateBuffer[2] = (byte)newValue;
      this.Broadcast(this.HealthUpdateBuffer);
    }

    /// <summary>
    /// Broadcast that the value of the players money has changed.
    /// </summary>
    /// <param name="newValue">The new value of the players money.</param>
    internal void MoneyUpdate(int newValue)
    {
      this.InputRoundBuffer[5] = (byte)newValue;
      newValue >>= 8;
      this.InputRoundBuffer[4] = (byte)newValue;
      newValue >>= 8;
      this.InputRoundBuffer[3] = (byte)newValue;
      newValue >>= 8;
      this.InputRoundBuffer[2] = (byte)newValue;
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
      this.TowerPositionBuffer[3] = (byte)id;
      id >>= 8;
      this.TowerPositionBuffer[2] = (byte)id;
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
      this.TowerRemovedBuffer[3] = (byte)towerId;
      towerId >>= 8;
      this.TowerRemovedBuffer[2] = (byte)towerId;
      this.Broadcast(this.TowerRemovedBuffer);
    }

    /// <summary>
    /// <para>Broadcast a confirmation that the server has notified the players about all animations that take place before a certain time.</para>
    /// <para>The clients are therefore safe to render the game up to that point in time.</para>
    /// </summary>
    /// <param name="tickNumber">The tick up to which all animations have been sent.</param>
    internal void AnimationConfirmation(short tickNumber)
    {
      this.AnimationConfirmationBuffer[3] = (byte)tickNumber;
      tickNumber >>= 8;
      this.AnimationConfirmationBuffer[2] = (byte)tickNumber;
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
      short endPosition,
      short startTime,
      short endTime,
      byte resultAnimation
    )
    {
      this.BoardToPathAnimationBuffer[2] = spriteNumber;
      this.BoardToPathAnimationBuffer[3] = startX;
      this.BoardToPathAnimationBuffer[4] = startY;
      this.BoardToPathAnimationBuffer[6] = (byte)endPosition;
      endPosition >>= 8;
      this.BoardToPathAnimationBuffer[5] = (byte)endPosition;
      this.BoardToPathAnimationBuffer[8] = (byte)startTime;
      startTime >>= 8;
      this.BoardToPathAnimationBuffer[7] = (byte)startTime;
      this.BoardToPathAnimationBuffer[10] = (byte)endTime;
      endTime >>= 8;
      this.BoardToPathAnimationBuffer[9] = (byte)endTime;
      this.BoardToPathAnimationBuffer[11] = resultAnimation;
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
      short startPosition,
      short endPosition,
      short startTime,
      short endTime,
      byte resultAnimation
    )
    {
      this.PathToPathAnimationBuffer[2] = spriteNumber;
      this.PathToPathAnimationBuffer[4] = (byte)startPosition;
      startPosition >>= 8;
      this.PathToPathAnimationBuffer[3] = (byte)startPosition;
      this.PathToPathAnimationBuffer[6] = (byte)endPosition;
      endPosition >>= 8;
      this.PathToPathAnimationBuffer[5] = (byte)endPosition;
      this.PathToPathAnimationBuffer[8] = (byte)startTime;
      startTime >>= 8;
      this.PathToPathAnimationBuffer[7] = (byte)startTime;
      this.PathToPathAnimationBuffer[10] = (byte)endTime;
      endTime >>= 8;
      this.PathToPathAnimationBuffer[9] = (byte)endTime;
      this.PathToPathAnimationBuffer[11] = resultAnimation;
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
      this.TowerAnimationBuffer[3] = (byte)towerId;
      towerId >>= 8;
      this.TowerAnimationBuffer[2] = (byte)towerId;
      this.Broadcast(this.TowerAnimationBuffer);
    }
  }
}
