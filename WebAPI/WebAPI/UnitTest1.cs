using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using RestSharp;
using System.Net;
using WebAPI.Models;

namespace WebAPI
{
    [TestFixture]
    public class Tests
    {
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/");
        }

        [Test]
        public void CheckResponse_GetBookingIds()
        {
            // arrange
            RestRequest request = new RestRequest("booking", Method.GET);

            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckResponse_CreateBooking()
        {
            // arrange
            RestRequest request = new RestRequest("booking", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new Content()
            {
                firstname = "Sarah",
                lastname = "Maas",
                totalprice = 412,
                depositpaid = true,
                bookingdates = new Dates()
                {
                    checkin = "2016-03-05",
                    checkout = "2023-12-01"
                },
                additionalneeds = ""
            });

            // act
            IRestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckResponse_UpdateBooking()
        {
            // arrange
            RestRequest request = new RestRequest("booking", Method.GET);
            IRestResponse response = client.Execute(request);
            var books = new JsonDeserializer().Deserialize<List<ID>>(response);
            request = new RestRequest("booking/" + books[1].bookingid, Method.PUT);
            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new Content()
            {
                firstname = "Sarah",
                lastname = "Maas",
                totalprice = 400,
                depositpaid = true,
                bookingdates = new Dates()
                {
                    checkin = "2016-03-05",
                    checkout = "2023-12-01"
                },
                additionalneeds = ""
            });

            // act
            response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var result = new JsonDeserializer().Deserialize<Dictionary<string, string>>(response);
            Assert.That(result["totalprice"], Is.EqualTo("400"));
        }

        [Test]
        public void CheckResponse_DeleteBooking()
        {
            // arrange
            RestRequest request = new RestRequest("booking", Method.GET);
            IRestResponse response = client.Execute(request);
            var books = new JsonDeserializer().Deserialize<List<ID>>(response);
            request = new RestRequest("booking/" + books[1].bookingid, Method.DELETE);
            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");
            request.AddHeader("Content-Type", "application/json");

            // act
            response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}