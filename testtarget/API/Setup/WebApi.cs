/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using APITests.Utils;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit.Abstractions;

namespace APITests.Setup
{
	class WebApi
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public Dictionary<string, string> CommonHeaders { get; } = new Dictionary<string, string>();

		public WebApi(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		public void SetDefaultHeaders()
		{
			CommonHeaders["Content-Type"] = "application/json";
			CommonHeaders["Accept"] = "application/json, text/html, */*";
		}

		public void ConfigureAuthenticationHeaders(string userName = null, string password = null)
		{
			userName ??= _configure.SuperUsername;
			password ??= _configure.SuperPassword;

			var loginToken = new LoginToken(_configure.BaseUrl, userName, password);

			CommonHeaders["Authorization"] = $"{loginToken.TokenType} {loginToken.AccessToken}";
		}

		public IRestResponse Get(string url, Dictionary<string,string> param = null, Dictionary<string, string> headers = null)
		{
			return RequestEndpoint(Method.GET, url, param, headers);
		}

		public IRestResponse Post(string url, JsonObject body = null, Dictionary<string, string> headers = null)
		{
			return RequestEndpoint(Method.POST, url, body, headers);
		}

		public IRestResponse Post(string url, JsonArray body = null, Dictionary<string, string> headers = null)
		{
			return RequestEndpoint(Method.POST, url, body, headers);
		}

		public IRestResponse Put(string url, JsonObject body = null, Dictionary<string, string> headers = null)
		{
			return RequestEndpoint(Method.PUT, url, body, headers);
		}

		public IRestResponse Put(string url, JsonArray body = null, Dictionary<string, string> headers = null)
		{
			return RequestEndpoint(Method.PUT, url, body, headers);
		}

		public IRestResponse Delete(string url, Dictionary<string,string> param = null, Dictionary<string, string> headers = null)
		{
			return RequestEndpoint(Method.DELETE, url, param, headers);
		}

		public IRestResponse RequestEndpoint(Method method, string url, object param = null, Dictionary<string, string> headers = null, DataFormat dataFormat= DataFormat.Json)
		{
			// Setup the rest client
			var client = new RestClient
			{
				BaseUrl = new Uri(_configure.BaseUrl + url)
			};

			// Setup the request
			var request = new RestRequest
			{
				Method = method,
			};

			// Merge the two dictionaries together
			var requestHeaders = new Dictionary<string, string>();
			if (headers != null)
			{
				foreach (var (key, value) in headers)
				{
					requestHeaders.TryAdd(key, value);
				}
			}
			if (CommonHeaders != null)
			{
				foreach (var (key, value) in CommonHeaders)
				{
					requestHeaders.TryAdd(key, value);
				}
			}

			// Set all the headers to the request
			if (requestHeaders.Count > 0)
			{
				foreach (var (key, value) in requestHeaders)
				{
					request.AddHeader(key, value);
				}
			}

			if (param != null)
			{
				request.RequestFormat = dataFormat;

				switch (dataFormat)
				{
					case DataFormat.Json:
						request.AddJsonBody(param);
						break;
					case DataFormat.Xml:
						request.AddXmlBody(param);
						break;
					default:
						request.AddJsonBody(param);
						break;
				}
			}
			// Execute the request
			var response = client.Execute(request);

			ApiOutputHelper.WriteRequestResponseOutput(request, response, _output);

			return response;
		}
	}
}