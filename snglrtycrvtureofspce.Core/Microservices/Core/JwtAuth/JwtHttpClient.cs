using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using snglrtycrvtureofspce.Core.Base.ComplexTypes;

namespace snglrtycrvtureofspce.Core.Microservices.Core.JwtAuth;

public class JwtHttpClient : HttpClient
{
    public JwtHttpClient(string hostUrl) : base((HttpMessageHandler) 
        new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = 
                (Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>) 
                ((sender, certificate, chain, sslPolicyErrors) => true)
        })
    {
        Timeout = TimeSpan.FromMinutes(1.0);
        DefaultRequestHeaders.Add("Accept", "application/json");
        DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
            JwtTokenProvider.GenerateSystemToken(SystemUsersEnumeration.SystemMessage));
        BaseAddress = new Uri(hostUrl);
    }

    public JwtHttpClient(string hostUrl, IHttpContextAccessor accessor) : base((HttpMessageHandler) 
        new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = 
                (Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>) 
                ((sender, certificate, chain, sslPolicyErrors) => true)
        })
    {
        Timeout = TimeSpan.FromMinutes(1.0);
        DefaultRequestHeaders.Add("Accept", "application/json");
        try
        {
            DefaultRequestHeaders.Add("Authorization", (IEnumerable<string>) new string[1]
            {
                (string) accessor.HttpContext.Request.Headers["Authorization"]
            });
        }
        catch (Exception ex)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                JwtTokenProvider.GenerateSystemToken(SystemUsersEnumeration.SystemMessage));
        }
        BaseAddress = new Uri(hostUrl);
    }
}