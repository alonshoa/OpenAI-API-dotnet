using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using log4net;
using OpenAI_API.Edit;

namespace OpenAI_API
{
	/// <summary>
	/// The API endpoint for querying available Engines/models
	/// </summary>
	public class EditEndpoint
	{
		private const string RequestUri = @"https://api.openai.com/v1/edits";
		OpenAIAPI Api;
		private static readonly ILog _logger;



		/// <summary>
		/// Constructor of the api endpoint.  Rather than instantiating this yourself, access it through an instance of <see cref="OpenAIAPI"/> as <see cref="OpenAIAPI.Engines"/>.
		/// </summary>
		/// <param name="api"></param>
		/// 
		static EditEndpoint()
		{
			_logger = LogManager.GetLogger("EditEndpoint");
		}
		internal EditEndpoint(OpenAIAPI api)
		{
			this.Api = api;
		}
		public async Task<string> GenerateEditAsync(EditRequest request)
		{
			_logger.Info("Calling OpenAI API to generate edit");
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Api.Auth.ThisOrDefault().ApiKey);
			client.DefaultRequestHeaders.Add("User-Agent", "okgodoit/dotnet_openai_api");
			string json_obj = JsonConvert.SerializeObject(request);
			//var response = await client.PostAsync(RequestUri, new StringContent("{\"instruction\":\"" + instruction + "\",\"input\":\"" + input_text + "\", \"model\":\"code-davinci-edit-001\"}", Encoding.UTF8, "application/json"));
			var response = await client.PostAsync(RequestUri, new StringContent(json_obj, Encoding.UTF8, "application/json"));
			string resultAsString = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				_logger.Info("Successfully generated edit");
				return resultAsString;
			}
			else
			{
				_logger.Error("Error calling OpenAI API to generate edit.  HTTP status code: " + response.StatusCode.ToString() + ". Content: " + resultAsString);
				throw new HttpRequestException("Error calling OpenAi API to generate edit.  HTTP status code: " + response.StatusCode.ToString() + ". Content: " + resultAsString);
			}
		}


		/// <summary>
		/// A helper class to deserialize the JSON API responses.  This should not be used directly.
		/// </summary>
		private class JsonHelperRoot
		{
			[JsonProperty("data")]
			public List<Engine> data { get; set; }
			[JsonProperty("object")]
			public string obj { get; set; }

		}
	}
}

