using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Net.Http;


namespace ApiPagos.Infrastruture.CircuitBreaker
{
    public static class PollyRegistry
    {
        private const int Exceptions_Allowed = 3;
        private const int Duration_Of_Break = 30;
        private static ILogger _logger;

        public static void Init(ILogger logger)
        {
            _logger = logger;
        }
        public static AsyncCircuitBreakerPolicy GetCircuitBreaker(string service)
        {
            return Policy
                .Handle<HttpRequestException>()
                .CircuitBreakerAsync(Exceptions_Allowed, TimeSpan.FromSeconds(Duration_Of_Break),
                (ex, Delay) =>
                {
                    _logger.LogError($".Registro de interrupción: En el servico { service } durante {Delay.TotalMilliseconds} ms. Debido a {ex.Message}");
                }, 
                () => _logger.LogWarning($".Registro de interrupción:¡Llama ok la servicio! { service }. Cerrado el ciucuit!"),
                () => _logger.LogWarning($".Registro de interrupción: Half-open: Next call is a trial!")
                );
        }
    }
}
