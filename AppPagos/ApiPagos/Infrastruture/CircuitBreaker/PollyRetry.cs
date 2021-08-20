using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Net.Http;

namespace ApiPagos.Infrastruture.CircuitBreaker
{
    public static class PollyRetry
    {
        private static ILogger _logger;

        public static void Init(ILogger logger)
        {
            _logger = logger;
        }

        public static AsyncPolicy GetPolicyAsync(int numRetries, int retryAttempt, string log)
        {
            return Policy
                .Handle<Exception>(e => !(e is BrokenCircuitException))
                .WaitAndRetryAsync(numRetries, attempt => TimeSpan.FromSeconds(retryAttempt), (ex, waitDuration) =>
                {
                    _logger.LogInformation(log, ex);
                });
        }
    }
}
