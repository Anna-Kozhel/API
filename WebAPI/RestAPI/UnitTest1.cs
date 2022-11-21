using RestAPI.Models;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System.Net;

namespace RestAPI
{
    public class Tests
    {
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://api.github.com/");
        }

        [Test]
        public void CheckResponse_GET()
        {
            // arrange
            RestRequest request = new RestRequest("gists", Method.GET);

            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckResponse_POST()
        {
            // arrange
            RestRequest request = new RestRequest("gists", Method.GET);
            IRestResponse response = client.Execute(request);
            var gists = new JsonDeserializer().Deserialize<List<Gist>>(response);
            request = new RestRequest($"gists/{gists[0].Id}/comments", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/vnd.github+json");
            request.AddHeader("Authorization", "Bearer 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");
            request.AddJsonBody(new
            {
                body = "Please work"
            });

            // act
            response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

    } 
}