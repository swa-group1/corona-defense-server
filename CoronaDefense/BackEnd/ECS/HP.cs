// <copyright file="IComponent.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.ECS
{
  internal readonly struct HP
  {
    /// <summary>
    /// Int value of current HP.
    /// </summary>
    public readonly int Hp;

    /// <summary>
    /// Initializes a new instance of the <see cref="HP"/> struct.
    /// </summary>
    /// <param name="hp">Initial int value of HP. </param>
    public HP(int hp)
    {
      this.Hp = hp;
    }

    /// <summary>
    /// Get a new HP based on a change in the current HP.
    /// </summary>
    /// <param name="hp">Change HP by this value. Use negative to decrease. </param>
    /// <returns> New HP with updated value. </returns>
    public HP ChangeBy(int hp)
    {
      return new HP(this.Hp + hp);
    }
  }
}
