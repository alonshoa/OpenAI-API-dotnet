using Newtonsoft.Json;
using NUnit.Framework;
using OpenAI_API;
using OpenAI_API.Edit;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenAI_Tests
{
	class EditEndpointTest
	{
		[SetUp]
		public void Setup()
		{
			OpenAI_API.APIAuthentication.Default = new OpenAI_API.APIAuthentication(Environment.GetEnvironmentVariable("TEST_OPENAI_SECRET_KEY"));
		}

		[Test]
		public void TestBasicEdit()
		{
			var api = new OpenAI_API.OpenAIAPI();

			//string instruction = "create a function that prints all names in a list";
			//string input_text = "names = ['Jon','Bob','Alice']\n\ndef print_names(names):";
			Assert.IsNotNull(api.Edit);
			EditRequest request = new EditRequest();
			var result = api.Edit.GenerateEditAsync(request).Result;
			Console.WriteLine(result);
			Assert.IsNotNull(result);
			EditResponse response = JsonConvert.DeserializeObject<EditResponse>(result);
			Assert.IsNotNull(response);
			Assert.IsNotNull(response.choices);
			Assert.IsNotNull(response.choices[0]);
			//Assert.AreEqual("USA", result);
		}
		// TODO: More tests needed but this covers basic functionality at least
	}
}