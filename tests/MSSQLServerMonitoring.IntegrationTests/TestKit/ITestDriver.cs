using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.IntegrationTests.TestKit
{
    public interface ITestDriver : IDisposable
    {
        IServiceProvider Services();

        HttpClient HttpClient();

        Task<TResponse> HttpClientGetAsync<TResponse>(
            string requestUri,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            IReadOnlyDictionary<string, string> headers = null );

        Task<TResponse> HttpClientAuthenticatedGetAsync<TResponse>(
            string requestUri,
            string username,
            HttpStatusCode statusCode = HttpStatusCode.OK );

        Task<TResponse> HttpClientPostAsync<TResponse>(
            string requestUri,
            object body,
            HttpStatusCode statusCode = HttpStatusCode.OK );

        Task HttpClientGetAsync(
            string requestUri,
            HttpStatusCode statusCode = HttpStatusCode.OK );

        Task HttpClientPostAsync(
            string requestUri,
            object body,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            IReadOnlyDictionary<string, string> headers = null );

        Task HttpClientUploadAsync(
            string requestUri,
            MultipartFormDataContent formData,
            HttpStatusCode statusCode = HttpStatusCode.OK );

        Task<TResponse> HttpClientSendAsync<TResponse>(
            string requestUri,
            object body,
            HttpMethod method,
            HttpStatusCode statusCode = HttpStatusCode.OK );

        Task SeedDatabase();
    }
}
