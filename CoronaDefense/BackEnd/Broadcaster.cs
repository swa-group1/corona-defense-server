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
  /// Broadcaster for a <see cref="ModelInstance"/>. Functionality to send game events to all connected clients.
  /// </summary>
  internal class Broadcaster
  {
    private static byte[] PingBuffer { get; } = new byte[4]
    {
      0x10, // Byte code
      0x02, // Length
      0x00, // Major version
      0x00, // Minor version
    };

    private byte[] FightRoundBuffer { get; } = new byte[4]
    {
      0x20, // Byte code
      0x02, // Length
      0x00, // Round number
      0x00,
    };

    private byte[] GameModeBuffer { get; } = new byte[4]
    {
      0x21, // Byte code
      0x02, // Length
      0x00, // Stage number
      0x00,
    };

    private byte[] InputRoundBuffer { get; } = new byte[4]
    {
      0x22, // Byte code
      0x02, // Length
      0x00, // Round number
      0x00,
    };

    private static byte[] LobbyModeBuffer { get; } = new byte[2]
    {
      0x23, // Byte code
      0x00, // Length
    };

    private byte[] HealthUpdateBuffer { get; } = new byte[4]
    {
      0x30, // Byte code
      0x02, // Length
      0x00, // New value
      0x00,
    };

    private byte[] MoneyUpdateBuffer { get; } = new byte[6]
    {
      0x31, // Byte code
      0x04, // Length
      0x00, // New value
      0x00,
      0x00,
      0x00,
    };

    private byte[] PlayerCountUpdateBuffer { get; } = new byte[3]
    {
      0x32, // Byte code
      0x01, // Length
      0x00, // Player count
    };

    private byte[] TurretPositionBuffer { get; } = new byte[7]
    {
      0x33, // Byte code
      0x05, // Length
      0x00, // Tower ID
      0x00,
      0x00, // Type number
      0x00, // X position
      0x00, // Y position
    };

    private byte[] TurretRemovedBuffer { get; } = new byte[4]
    {
      0x40, // Byte code
      0x02, // Length
      0x00, // Tower ID
      0x00,
    };

    private byte[] AnimationConfirmationBuffer { get; } = new byte[4]
    {
      0x50, // Byte code
      0x02, // Length
      0x00, // Time
      0x00,
    };

    private byte[] BoardToPathAnimationBuffer { get; } = new byte[12]
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

    private byte[] PathToPathAnimationBuffer { get; } = new byte[12]
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

    private byte[] TowerAnimationBuffer { get; } = new byte[6]
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
    private Dictionary<long, Socket> sockets = new Dictionary<long, Socket>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Broadcaster"/> class.
    /// </summary>
    internal Broadcaster()
    {
    }

    private void Broadcast(byte[] buffer)
    {
      foreach (Socket socket in this.sockets.Values)
      {
        socket.Send(buffer);
      }
    }

    internal void ConnectTo(long accessToken, IPAddress address, int port)
    {
      if (this.sockets.ContainsKey(accessToken))
      {
        throw new ArgumentException($"Access token already registered in this {nameof(Broadcaster)}.");
      }

      Socket socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
      socket.Connect(address, port);
      this.sockets.Add(accessToken, socket);
    }
 
    internal void Ping()
    {
      this.Broadcast(PingBuffer);
    }

    internal void FightRound(short roundNumber)
    {
      this.FightRoundBuffer[3] = (byte)roundNumber;
      roundNumber >>= 8;
      this.FightRoundBuffer[2] = (byte)roundNumber;
      this.Broadcast(this.FightRoundBuffer);
    }

    internal void GameMode(byte stageNumber)
    {
      this.GameModeBuffer[2] = stageNumber;
      this.Broadcast(this.GameModeBuffer);
    }
    
    internal void InputRound(short roundNumber)
    {
      this.InputRoundBuffer[3] = (byte)roundNumber;
      roundNumber >>= 8;
      this.InputRoundBuffer[2] = (byte)roundNumber;
      this.Broadcast(this.FightRoundBuffer);
    }

    internal void LobbyMode()
    {
      this.Broadcast(LobbyModeBuffer);
    }

    internal void HealthUpdate(short newValue)
    {
      this.HealthUpdateBuffer[3] = (byte)newValue;
      newValue >>= 8;
      this.HealthUpdateBuffer[2] = (byte)newValue;
      this.Broadcast(this.HealthUpdateBuffer);
    }

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
    
    internal void PlayerCountUpdate(byte playerCount)
    {
      this.PlayerCountUpdateBuffer[2] = playerCount;
      this.Broadcast(this.PlayerCountUpdateBuffer);
    }

    internal void TurretPosition(short id, byte type, byte x, byte y)
    {
      this.TurretPositionBuffer[3] = (byte)id;
      id >>= 8;
      this.TurretPositionBuffer[2] = (byte)id;
      this.TurretPositionBuffer[4] = type;
      this.TurretPositionBuffer[5] = x;
      this.TurretPositionBuffer[6] = y;
      this.Broadcast(this.TurretPositionBuffer);
    }

    internal void TurretRemoved(short towerId)
    {
      this.TurretRemovedBuffer[3] = (byte)towerId;
      towerId >>= 8;
      this.TurretRemovedBuffer[2] = (byte)towerId;
      this.Broadcast(this.TurretRemovedBuffer);
    }


    internal void AnimationConfirmation(short tickNumber)
    {
      this.AnimationConfirmationBuffer[3] = (byte)tickNumber;
      tickNumber >>= 8;
      this.AnimationConfirmationBuffer[2] = (byte)tickNumber;
      this.Broadcast(this.AnimationConfirmationBuffer);
    }

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

    internal void TowerAnimation(short towerId, byte animation, byte rotation)
    {
      this.TowerAnimationBuffer[3] = (byte)towerId;
      towerId >>= 8;
      this.TowerAnimationBuffer[2] = (byte) towerId;
      this.Broadcast(TowerAnimationBuffer);
    }
  }
}
