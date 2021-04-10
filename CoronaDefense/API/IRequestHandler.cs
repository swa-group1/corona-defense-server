// <copyright file="IRequestHandler.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace API
{
  /// <summary>
  /// Handler of request without input.
  /// </summary>
  /// <typeparam name="TResult">Type of output of request.</typeparam>
  public interface IRequestHandler<TResult>
  {
    /// <summary>
    /// Process request.
    /// </summary>
    /// <returns><typeparamref name="TResult"/> of requests.</returns>
    protected internal TResult ProcessRequest();
  }

  /// <summary>
  /// Handler of request with input.
  /// </summary>
  /// <typeparam name="TRequest">Type of input of requests.</typeparam>
  /// <typeparam name="TResult">Type of output of requests.</typeparam>
  public interface IRequestHandler<TRequest, TResult>
  {
    /// <summary>
    /// Process supplied <typeparamref name="TRequest"/>.
    /// </summary>
    /// <param name="request">Request to process.</param>
    /// <returns><typeparamref name="TResult"/> of request.</returns>
    protected internal TResult ProcessRequest(TRequest request);
  }
}
