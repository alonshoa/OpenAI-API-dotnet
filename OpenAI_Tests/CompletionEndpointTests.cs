using NUnit.Framework;
using OpenAI_API;
using System;
using System.IO;
using System.Linq;

namespace OpenAI_Tests
{
	public class CompletionEndpointTests
	{
		[SetUp]
		public void Setup()
		{
			var t = Environment.GetEnvironmentVariable("TEST_OPENAI_SECRET_KEY");
			OpenAI_API.APIAuthentication.Default = new OpenAI_API.APIAuthentication(t);
		}

		[Test]
		public void GetBasicCompletion()
		{
			var api = new OpenAI_API.OpenAIAPI(engine: Engine.Davinci);

			Assert.IsNotNull(api.Completions);

			var results = api.Completions.CreateCompletionsAsync(new CompletionRequest("One Two Three Four Five Six Seven Eight Nine One Two Three Four Five Six Seven Eight", temperature: 0.1, max_tokens: 5), 5).Result;
			Assert.IsNotNull(results);
			Assert.NotNull(results.Completions);
			Assert.NotZero(results.Completions.Count);
			Assert.That(results.Completions.Any(c => c.Text.Trim().ToLower().StartsWith("nine")));
		}

		[Test]
		public void GetFineTunedCompletion()
		{
			var api = new OpenAI_API.OpenAIAPI(engine: Engine.Davinci);

			Assert.IsNotNull(api.Completions);
			var completionRequest =
				new CompletionRequest(
					"Kaboo é uma loja de fantasias\nAs 10 melhores fantasias para o Halloween\n###\n 1.",
					temperature: 0.65,
					max_tokens: 600,
					model: "davinci:ft-infinity-copy-2022-02-09-17-59-39",
					top_p: 1.0,
					echo: true,
					presencePenalty: 0,
					frequencyPenalty: 0.0,
					stopSequences: new string[]{"\n###"});

			var results = api.Completions.CreateCompletionsAsync(completionRequest, 1).Result;
			Assert.IsNotNull(results);
			Assert.NotNull(results.Completions);
			Assert.NotZero(results.Completions.Count);
		}

		// TODO: More tests needed but this covers basic functionality at least
	}
}
