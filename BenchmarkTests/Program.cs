using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;
using ElasticSearch.Client.Config;
using ElasticSearch.Client.Domain;

namespace BenchmarkTests
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new ElasticSearchClient("localhost",9200,TransportType.Http);
			int failure = 0;

            var res = client.CreateIndex("benchmark_test", new IndexSetting(1, 0));
            
			var data = "{\"book\": \"The Hitchhiker's Guide to the Galaxy\"" +
                ",\"id\":"+1+
			           ",\"chapter\": \"Chapter 11\",\"text1\": \"All the doors in this spaceship have a cheerful and sunny disposition. It is their pleasure to open for you, and their satisfaction to close again with the knowledge of a job well done.\"}";
			res = client.Index("benchmark_test", "default", Guid.NewGuid().ToString(), data);

            //int count = int.Parse(args[0]);

		    int count = 10;

			var begin = DateTime.Now;
			for (var i = 0; i < count; i++)
			{
                data = "{\"book\": \"The Hitchhiker's Guide to the Galaxy\"" +
                ",\"id\":" + i +
                       ",\"chapter\": \"Chapter 11\",\"text1\": \"All the doors in this spaceship have a cheerful and sunny disposition. It is their pleasure to open for you, and their satisfaction to close again with the knowledge of a job well done.\"}";
                client.Index("benchmark_test", "default", Guid.NewGuid().ToString(), data);

				var response = client.Index("benchmark_test", "default", Guid.NewGuid().ToString(), data);
				if (!response.Success)
				{
					failure++;
				}
			}
			var end = DateTime.Now;

			var timespan = end - begin;

			var avg = timespan.TotalMilliseconds / count;
			Console.WriteLine("iteration:{0},failure:{3},elapsed time:{1},avg time:{2}ms", count, timespan, avg, failure);

		    var result = client.Search("benchmark_test", "default", "text1:doors");

			client.DeleteIndex("benchmark_test");

			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}
