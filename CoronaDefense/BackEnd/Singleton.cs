// <copyright file="Singleton.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  using System;

  /// <summary>
  /// <para>Universal <see langword="abstract"/> base <see langword="class"/> for singleton objects.</para>
  /// <para>Handles the management and access of Singleton instances.</para>
  /// </summary>
  /// <typeparam name="T">Type of singleton.</typeparam>
  public abstract class Singleton<T>
    where T : class
  {
    private static readonly Lazy<T> InstanceValue = new Lazy<T>(CreateInstanceOfT);

    /// <summary>
    /// Gets currently active Singleton instance.
    /// </summary>
    public static T Instance
    {
      get { return InstanceValue.Value; }
    }

    /// <summary>
    /// Creates a new instance of <typeparamref name="T"/> via reflection, because in normal usage the constructor of <typeparamref name="T"/> is expected to be <see langword="private"/>.
    /// </summary>
    /// <returns>Newly created instance of <typeparamref name="T"/>.</returns>
    private static T CreateInstanceOfT()
    {
      return Activator.CreateInstance(typeof(T), true) as T;
    }
  }
}
