using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client.Utils;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ElasticSearch.Tests
{
    [TestFixture]
    public class DateTimeSerializeTest
    {
        [Test]
        public void DateTimeJsonSerializeTest()
        {
            var str = JsonBuilder.strValueOf(DateTime.Now, false);

            var result = JsonConvert.DeserializeObject(str, typeof(DateTime), new JsonSerializerSettings()
            {
                //			ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = new List<JsonConverter> { new IsoDateTimeConverter() }
            });
        }
    }
}
