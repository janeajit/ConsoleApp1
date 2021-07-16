//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web;

//namespace ConsoleApp1
//{
//    class http
//    {
//            public List<Cookie> ResponseCookies { get; private set; } = new List<Cookie>();

//            protected string Controller
//            {
//                get => _controller;
//                set
//                {
//                    if (string.IsNullOrEmpty(value))
//                        throw new Exception("Empty string passed into controller property");

//                    _controller = value.EndsWith("/") ? value : $"{value}/";
//                }
//            }

//            protected HttpMethod HttpMethod { get; private set; } = HttpMethod.Post;

//            private HttpHeaders ResponseHeaders { get; set; }
//            private int ResponseStatusCode { get; set; }

//            private readonly CookieContainer _cookieContainer = new CookieContainer();
//            private readonly bool _logHeaders;
//            private string _controller;

//            private readonly IDictionary<string, string> _requestHeaders = new Dictionary<string, string>
//        {
//            {HttpRequestHeaders.ServicesUserAgent.Name, HttpRequestHeaders.ServicesUserAgent.Value}
//        };

//            public HttpClientTool(string controller, bool logHeaders = false)
//            {
//                Controller = controller;
//                _logHeaders = logHeaders;
//            }

//            public async Task<T> SendAsync<T>(dynamic request, string apiMethodName, string queryString = null)
//            {
//                var serialized = request is string
//                    ? request
//                    : SerializeObject(() => JsonConvert.SerializeObject(request, new JsonSerializerSettings
//                    {
//                        ContractResolver = new DefaultContractResolver(),
//                    }), apiMethodName);

//                var content = new ByteArrayContent(Encoding.ASCII.GetBytes(serialized))
//                {
//                    Headers =
//                {
//                    ContentType = new MediaTypeWithQualityHeaderValue(RequestHeaders.JsonContentType.Value)
//                }
//                };

//                if (HttpMethod == HttpMethod.Get || HttpMethod == HttpMethod.Head)
//                    content = null;

//                var requestUri = new UriBuilder(Controller + apiMethodName)
//                {
//                    Query = queryString
//                };
//                Logger.Log($"Request Uri : {requestUri.Uri}");

//                var requestMessage = new HttpRequestMessage
//                {
//                    RequestUri = requestUri.Uri,
//                    Method = HttpMethod,
//                    Content = content,
//                    Headers =
//                {
//                    Accept =
//                    {
//                        new MediaTypeWithQualityHeaderValue(RequestHeaders.JsonAccept.Value)
//                    }
//                }
//                };

//                AddCookiesToRequest(request);
//                AddCustomHeadersToRequest(request);

//                return await ProcessWebRequestAsync<T>(requestMessage);
//            }

//            public NameValueCollection NewQueryString(string uri = "")
//            {
//                var query = HttpUtility.ParseQueryString(uri);
//                return query;
//            }

//            [Obsolete]
//            public async Task<T> GetRequestAsync<T>(string controller)
//            {
//                HttpMethod = HttpMethod.Get;
//                return await SendAsync<T>("", controller);
//            }



//            /// <summary>
//            /// New implementation to send POST requests
//            /// </summary>
//            /// <typeparam name="T">Response object type</typeparam>
//            /// <param name="request">Deserialized Request object</param>
//            /// <param name="uriPath">Path e.g. api/something/</param>
//            /// <param name="queryString">query parameters string</param>
//            /// <returns>Api Response with Data and Status Code</returns>
//            public async Task<ApiResponse<T>> SendPostRequestAsync<T>(dynamic request, string uriPath, string queryString)
//            {
//                return await SendRequestAsync<T>(HttpMethod.Post, request, uriPath, queryString);
//            }

//            /// <summary>
//            /// New implementation to send POST requests
//            /// </summary>
//            /// <typeparam name="T">Response object type</typeparam>
//            /// <param name="request">Deserialized Request object</param>
//            /// <param name="uriPath">Path e.g. api/something/</param>
//            /// <param name="queryParameters">Optional query parameter names and values</param>
//            /// <returns>Api Response with Data and Status Code</returns>
//            public async Task<ApiResponse<T>> SendPostRequestAsync<T>(dynamic request, string uriPath, params (string name, string value)[] queryParameters)
//            {
//                return await SendPostRequestAsync<T>(request, uriPath, BuildNewQueryString(queryParameters));
//            }

//            /// <summary>
//            /// New implementation to send GET requests
//            /// </summary>
//            /// <typeparam name="T">Response object type</typeparam>
//            /// <param name="uriPath">Path e.g. api/something/</param>
//            /// <param name="queryString">query parameters string</param>
//            /// <returns>Api Response with Data and Status Code</returns>
//            public async Task<ApiResponse<T>> SendGetRequestAsync<T>(string uriPath, string queryString)
//            {
//                return await SendRequestAsync<T>(HttpMethod.Get, "", uriPath, queryString);
//            }

//            /// <summary>
//            /// New implementation to send GET requests
//            /// </summary>
//            /// <typeparam name="T">Response object type</typeparam>
//            /// <param name="uriPath">Path e.g. api/something/</param>
//            /// <param name="queryParameters">Optional query parameter names and values</param>
//            /// <returns>Api Response with Data and Status Code</returns>
//            public async Task<ApiResponse<T>> SendGetRequestAsync<T>(string uriPath, params (string name, string value)[] queryParameters)
//            {
//                return await SendGetRequestAsync<T>(uriPath, BuildNewQueryString(queryParameters));
//            }

//            /// <summary>
//            /// New implementation to send DELETE requests
//            /// </summary>
//            /// <typeparam name="T">Response object type</typeparam>
//            /// <param name="uriPath">Path e.g. api/something/</param>
//            /// <param name="queryString">query parameters string</param>
//            /// <returns>Api Response with Data and Status Code</returns>
//            public async Task<ApiResponse<T>> SendDeleteRequestAsync<T>(string uriPath, string queryString)
//            {
//                return await SendRequestAsync<T>(HttpMethod.Delete, "", uriPath, queryString);
//            }

//            /// <summary>
//            /// New implementation to send DELETE requests
//            /// </summary>
//            /// <typeparam name="T">Response object type</typeparam>
//            /// <param name="uriPath">Path e.g. api/something/</param>
//            /// <param name="queryParameters">Optional query parameter names and values</param>
//            /// <returns>Api Response with Data and Status Code</returns>
//            public async Task<ApiResponse<T>> SendDeleteRequestAsync<T>(string uriPath, params (string name, string value)[] queryParameters)
//            {
//                return await SendDeleteRequestAsync<T>(uriPath, BuildNewQueryString(queryParameters));
//            }

//            public async Task<object> SendOptionRequestAsync(string methodName)
//            {
//                HttpMethod = HttpMethod.Options;
//                return await SendAsync<object>(new JObject(), methodName);
//            }

//            public async Task<object> SendDeleteRequestAsync(string methodName)
//            {
//                HttpMethod = HttpMethod.Delete;
//                return await SendAsync<object>(new JObject(), methodName);
//            }

//            public async Task<object> SendHeadRequestAsync(string methodName)
//            {
//                HttpMethod = HttpMethod.Head;
//                return await SendAsync<object>(new JObject(), methodName);
//            }

//            public string GetResponseHeaderValue(string headerName)
//            {
//                ResponseHeaders.TryGetValues(headerName, out var headers);
//                return headers?.FirstOrDefault();
//            }

//            public void AddRequestHeader(string headerName, string headerValue) =>
//                _requestHeaders.Add(headerName, headerValue);

//            public void RemoveRequestHeaders() => _requestHeaders.Clear();

//            public void RemoveRequestHeader(string headerName)
//            {
//                if (_requestHeaders.ContainsKey(headerName))
//                    _requestHeaders.Remove(headerName);
//            }

//            internal string BuildNewQueryString(params (string name, string value)[] queryParameters)
//            {
//                var nvc = NewQueryString();

//                foreach (var (name, value) in queryParameters ?? new (string, string)[0])
//                {
//                    if (value != null) nvc.Add(name, value);
//                }

//                var sb = new StringBuilder();

//                var first = true;

//                foreach (var key in nvc.AllKeys)
//                {
//                    foreach (var value in nvc.GetValues(key))
//                    {
//                        if (!first)
//                        {
//                            sb.Append("&");
//                        }

//                        sb.AppendFormat("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));

//                        first = false;
//                    }
//                }

//                return sb.ToString();
//            }

//            private async Task<ApiResponse<T>> SendRequestAsync<T>(HttpMethod method, dynamic request, string uriPath, string queryString)
//            {
//                try
//                {
//                    HttpMethod = method;
//                    var response = await SendAsync<T>(request, uriPath, queryString);
//                    return new ApiResponse<T>(response, ResponseStatusCode);
//                }
//                catch (CoreHttpToolException)
//                {
//                    return new ApiResponse<T>(ResponseStatusCode);
//                }
//                catch (HttpRequestException httpRequestException)
//                {
//                    return new ApiResponse<T>(httpRequestException);
//                }
//            }

//            private async Task<T> ProcessWebRequestAsync<T>(HttpRequestMessage requestMessage)
//            {
//                using (var handler = new HttpClientHandler
//                {
//                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
//                })
//                {
//                    handler.CookieContainer = _cookieContainer;

//                    using (var client = new HttpClient(handler))
//                    {
//                        AddHeaders(client);

//                        var response =
//                            await
//                                client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead,
//                                    CancellationToken.None);

//                        return await ProcessWebResponseAsync<T>(response);
//                    }
//                }
//            }

//            private async Task<T> ProcessWebResponseAsync<T>(HttpResponseMessage response)
//            {
//                try
//                {
//                    ResponseCookies = _cookieContainer.GetCookies(response.RequestMessage.RequestUri).Cast<Cookie>()
//                        .ToList();
//                    ResponseHeaders = response.Headers;

//                    if (_logHeaders)
//                    {
//                        Logger.Log("{0}", parameters: "Response headers: ");
//                        foreach (var responseHeader in ResponseHeaders)
//                            Logger.Log("{0} : {1}", responseHeader.Key, responseHeader.Value.FirstOrDefault());
//                    }

//                    response.Headers.TryGetValues("Set-Cookie", out var cookieHeaders);

//                    Logger.Log($"Http Status Code: {response.StatusCode}");
//                    ResponseStatusCode = (int)response.StatusCode;

//                    if (HttpMethod == HttpMethod.Options || HttpMethod == HttpMethod.Head)
//                    {
//                        return (T)new object();
//                    }

//                    var responseContent = new StreamReader(await response.Content.ReadAsStreamAsync()).ReadToEnd();

//                    var deserialized = DeserializeObject(() => JsonConvert.DeserializeObject<T>(responseContent),
//                        responseContent);

//                    CookieJar.AddCookiesWithHeadersToObject(deserialized,
//                        new CookiesWithHeaders { Cookies = ResponseCookies, Headers = cookieHeaders });

//                    return deserialized;
//                }
//                catch (Exception ex)
//                {
//                    Logger.Log(ex.Message);
//                    throw;
//                }
//            }

//            private static dynamic SerializeObject(Func<dynamic> expression, string apiMethodName)
//            {
//                var result = expression();
//                Logger.Log($"\n{apiMethodName.ToUpper()} REQUEST:");
//                Logger.Log(result.ToString());
//                return result;
//            }

//            private static T DeserializeObject<T>(Func<T> expression, string responseString)
//            {
//                var resultType = (typeof(T).ToString().Split('.')).Last();
//                var logMessage = $"{Environment.NewLine}{resultType.ToUpper()}:{Environment.NewLine}{responseString}";
//                Logger.Log(logMessage);
//                try
//                {
//                    var result = expression();
//                    return result;
//                }
//                catch (Exception ex)
//                {
//                    Logger.Warn($"Unable to deserialize http response to {resultType} object -> {ex.Message}");
//                    throw new CoreHttpToolException(responseString, ex);
//                }
//            }

//            private void AddCookiesToRequest(object obj)
//            {
//                var cookies = CookieJar.GetCookiesForObject(obj);
//                foreach (var cookie in cookies)
//                {
//                    Logger.Log($"Cookie found: {cookie.Name}");
//                    _cookieContainer.Add(cookie);
//                }

//                if (cookies.Count == 0)
//                    Logger.Log("No Cookies.");
//            }

//            private void AddCustomHeadersToRequest(object obj)
//            {
//                var headers = HeaderBox.GetHeadersFromRequest(obj);
//                foreach (var (name, value) in headers)
//                {
//                    if (_requestHeaders.ContainsKey(name))
//                    {
//                        Logger.Log(
//                            $"Header {name} already exists. Replacing value from {_requestHeaders[name]} to {value}");
//                        _requestHeaders[name] = value;
//                    }
//                    else
//                    {
//                        Logger.Log($"Adding custom header: {name}: {value}}}");
//                        _requestHeaders.Add(name, value);
//                    }
//                }

//                if (headers.Count == 0)
//                    Logger.Log("No custom headers.");
//            }

//            private void AddHeaders(HttpClient request)
//            {
//                if (_requestHeaders == null || !_requestHeaders.Any()) return;
//                foreach (var header in _requestHeaders)
//                {
//                    request.DefaultRequestHeaders.Add(header.Key, header.Value);
//                    if (_logHeaders)
//                        Logger.Log("{0} : {1} {2}", header.Key, header.Value, "header added to the request");
//                }
//            }
//        }
//    }


