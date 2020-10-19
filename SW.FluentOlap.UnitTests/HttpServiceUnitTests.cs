using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SW.FluentOlap.Models;

namespace UtilityUnitTests
{
    [TestClass]
    public class HttpServiceUnitTests
    {
        [TestMethod]
        public void CreateServiceTest()
        {
            HttpService service = new HttpService("PostsService", "https://jsonplaceholder.typicode.com/posts/{PostId}");
            Assert.AreEqual("PostsService", service.ServiceName);
        }

        [TestMethod]
        public void GetRequiredParameters()
        {
            HttpService service1 = new HttpService("PostsService", "https://jsonplaceholder.typicode.com/posts/{PostId}");
            
            
            Assert.IsTrue(service1.GetRequiredParameters().Contains("PostId"));
            Assert.IsTrue(service1.GetRequiredParameters().Count() == 1);
            
            HttpService service2 = new HttpService("CommentsService", "https://jsonplaceholder.typicode.com/posts/{PostId}/comments/{CommentId}");
            Assert.IsTrue(service2.GetRequiredParameters().Contains("PostId") && service2.GetRequiredParameters().Contains("CommentId") );
            Assert.IsTrue(service2.GetRequiredParameters().Count() == 2);
        }

        [TestMethod]
        public async Task FormatUriTest()
        {
            
            HttpService service1 = new HttpService("PostsService", "https://jsonplaceholder.typicode.com/posts/{PostId}");

            HttpResponse invokation = await service1.InvokeAsync(new HttpServiceOptions
            {
                Parameters = new
                {
                    PostId = 1
                }
            });
            
            
            Assert.AreEqual(invokation.FormattedUrlCalled, "https://jsonplaceholder.typicode.com/posts/1" );
        }

        [TestMethod]
        public async Task CallApiTest()
        {
            HttpService service1 = new HttpService("PostsService", "https://jsonplaceholder.typicode.com/posts/{PostId}");

            HttpResponse invokation = await service1.InvokeAsync(new HttpServiceOptions
            {
                Parameters = new
                {
                    PostId = 1
                }
            });

            JToken rs = JToken.Parse(invokation.Content);
            Assert.AreEqual(rs["id"].ToString(), "1" );
            
        }
    }
}