using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MSSQLServerMonitoring.IntegrationTests.TestKit
{
    public class TestDriver : ITestDriver, IDisposable
    {
        private readonly IWebHost _webHost;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TestDriver( Type startupType, string environment = "Development" )
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath( Path.GetDirectoryName( System.Reflection.Assembly.GetAssembly( startupType ).Location ) )
                .AddJsonFile( "appsettings.test.json", true, true )
                .AddEnvironmentVariables()
                .Build();

            _server = new TestServer(
                new WebHostBuilder()
                    .UseStartup( startupType )
                    .UseEnvironment( environment )
                    .UseConfiguration( configuration ) );
            _webHost = _server.Host;
            _client = _server.CreateClient();
        }

        public IServiceProvider Services()
        {
            return _webHost.Services;
        }

        public HttpClient HttpClient()
        {
            return _client;
        }

        public async Task<TResponse> HttpClientGetAsync<TResponse>(
            string requestUri,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            IReadOnlyDictionary<string, string> headers = null )
        {
            SetRequestHeaders( headers );
            var response = await _client.GetAsync( requestUri );
            if ( response.StatusCode != statusCode )
            {
                throw new HttpRequestException( $"Status code: {response.StatusCode}" );
            }

            string responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>( responseString );
        }

        public async Task<TResponse> HttpClientAuthenticatedGetAsync<TResponse>(
            string requestUri,
            string username,
            HttpStatusCode statusCode = HttpStatusCode.OK )
        {
            RequestBuilder requestBuilder = _server.CreateRequest( requestUri );
            requestBuilder.AddHeader( "Cookie", $"_TLTICKET={username};" );

            var response = await requestBuilder.GetAsync();
            if ( response.StatusCode != statusCode )
            {
                throw new HttpRequestException( $"Status code: {response.StatusCode}" );
            }

            string responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>( responseString );
        }

        public async Task<TResponse> HttpClientPostAsync<TResponse>(
            string requestUri,
            object body,
            HttpStatusCode statusCode = HttpStatusCode.OK )
        {
            var contents = new StringContent(
                JsonConvert.SerializeObject( body ),
                Encoding.UTF8,
                "application/json" );
            var response = await _client.PostAsync( requestUri, contents );
            if ( response.StatusCode != statusCode )
            {
                throw new HttpRequestException( $"Status code: {response.StatusCode}" );
            }

            string responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>( responseString );
        }

        public async Task HttpClientGetAsync(
            string requestUri,
            HttpStatusCode statusCode = HttpStatusCode.OK )
        {
            var response = await _client.GetAsync( requestUri );
            if ( response.StatusCode != statusCode )
            {
                throw new HttpRequestException( $"Status code: {response.StatusCode}" );
            }
        }

        public async Task HttpClientPostAsync(
            string requestUri,
            object body,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            IReadOnlyDictionary<string, string> headers = null )
        {
            RequestBuilder requestBuilder = _server.CreateRequest( requestUri );
            SetRequestHeaders( requestBuilder, headers );
            var contents = new StringContent(
                JsonConvert.SerializeObject( body ),
                Encoding.UTF8,
                "application/json" );
            requestBuilder.And( x => x.Content = contents );

            var response = await requestBuilder.PostAsync();
            if ( response.StatusCode != statusCode )
            {
                throw new HttpRequestException( $"Status code: {response.StatusCode}" );
            }
        }

        public async Task HttpClientUploadAsync(
            string requestUri,
            MultipartFormDataContent formData,
            HttpStatusCode statusCode = HttpStatusCode.OK )
        {
            RequestBuilder requestBuilder = _server.CreateRequest( requestUri );
            requestBuilder.And( x => x.Content = formData );

            var response = await requestBuilder.PostAsync();

            if ( response.StatusCode != statusCode )
            {
                throw new HttpRequestException( $"Status code: {response.StatusCode}" );
            }
        }

        public async Task<TResponse> HttpClientSendAsync<TResponse>(
            string requestUri,
            object body,
            HttpMethod method,
            HttpStatusCode statusCode = HttpStatusCode.OK )
        {
            var request = new HttpRequestMessage( method, requestUri )
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject( body ),
                    Encoding.UTF8,
                    "application/json" )
            };

            HttpResponseMessage response = await _client.SendAsync( request );

            if ( response.StatusCode != statusCode )
            {
                throw new HttpRequestException( $"Status code: {response.StatusCode}" );
            }

            string responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>( responseString );
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        public async Task SeedDatabase()
        {
            IDbSeeder dbSeeder = Services().GetService<IDbSeeder>();
            if ( dbSeeder == null )
            {
                return;
            }

            await dbSeeder.Seed();
        }

        private void SetRequestHeaders( IReadOnlyDictionary<string, string> headers )
        {
            if ( headers == null )
            {
                return;
            }

            foreach ( string headerName in headers.Keys )
            {
                _client.DefaultRequestHeaders.Add( headerName, headers[ headerName ] );
            }
        }

        private void SetRequestHeaders( RequestBuilder requestBuilder, IReadOnlyDictionary<string, string> headers )
        {
            if ( headers != null )
            {
                foreach ( string headerName in headers.Keys )
                {
                    requestBuilder.AddHeader( headerName, headers[ headerName ] );
                }
            }
        }
    }
}
