using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Json;
using System.Net;
using RA;
using System.IO;

namespace weApiTest
{
    [TestClass]
    public class UnitTest1
    {
        private string ACCESS_TOKEN = "ZojTzbJkeRMAAAAAAAAAAX5TO-xSY6s_P_oT4TPAGoAnimVdDcCepnyG9qqe8QNu";

        [TestMethod]
        public void A_uploadTest()
        {

            JsonObject json = new JsonObject
            {

            };

            byte[] data = File.ReadAllBytes("../../may.jpg");

            new RestAssured()
                .Given()
                    .Header("Dropbox-API-Arg", "{\"mode\":\"add\",\"autorename\":false,\"mute\":false,\"path\":\"/may.jpg\"}")
                    .Header("Content-Type", "application/octet-stream")
                    .Header("Authorization", "Bearer ZojTzbJkeRMAAAAAAAAAAX5TO-xSY6s_P_oT4TPAGoAnimVdDcCepnyG9qqe8QNu")
                    .Body(data)
                .When()
                    .Post("https://content.dropboxapi.com/2/files/upload")
                .Then()
                    .TestStatus("test a", x => x == (int)HttpStatusCode.OK)
                    .Assert("test a");
        }
        [TestMethod]
        public void B_getMetadataTest()
        {

            JsonObject json = new JsonObject
            {
                { "path", "/may.jpg" }
            };

            new RestAssured()
                .Given()
                .Header("Authorization", "Bearer " + ACCESS_TOKEN)
                .Header("Content-Type", "application/json")
                .Body(json.ToString())
                .When()
                .Post("https://api.dropboxapi.com/2/files/get_metadata")
                .Then()
                .TestStatus("test a", x => x == (int)HttpStatusCode.OK)
                .Assert("test a");
        }

        [TestMethod]
        public void C_deleteTest()
        {

            JsonObject json = new JsonObject
            {
                { "path", "/may.jpg" }
            };

            new RestAssured()
                .Given()
                    .Header("Authorization", "Bearer " + ACCESS_TOKEN)
                    .Header("Content-Type", "application/json")
                    .Body(json.ToString())
                .When()
                    .Post("https://api.dropboxapi.com/2/files/delete_v2")
                .Then()
                    .TestStatus("test a", x => x == (int)HttpStatusCode.OK)
                    .Assert("test a");
        }
    }
}