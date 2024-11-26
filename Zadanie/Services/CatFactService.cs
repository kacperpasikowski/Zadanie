using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Zadanie.Models;

namespace Zadanie.Services
{
	public class CatFactService
	{
		private readonly HttpClient _httpClient;

		public CatFactService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}


		public async Task<CatFactResponseModel> GetFactAsync(string endpoint)
		{
			var response = await _httpClient.GetAsync(endpoint);
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();


				SaveToFile(json);

				return JsonSerializer.Deserialize<CatFactResponseModel>(json, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
			}

			return null;
		}

		private void SaveToFile(string data)
		{
			string filePath = "CatFact.txt";

			var deserializedData = JsonSerializer.Deserialize<object>(data, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			var formattedResponse = JsonSerializer.Serialize(deserializedData, new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			});


			File.AppendAllText(filePath, $"{formattedResponse}\n", System.Text.Encoding.UTF8);


		}

	}
}