using ElasticSearch.Client;
using ElasticSearch.Client.Config;
using ElasticSearch.Client.Mapping;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MappingTest3
    {
        [Test]
        public void TestTypeExist()
        {
            var index = "1-2147483644";

            var type = "Order";

            ElasticSearchClient client = new ElasticSearchClient("127.0.0.1", 9200, TransportType.Http);

            Assert.IsTrue(client.TypeExist(index, type));

            var type2 = "Order2";

            Assert.IsFalse(client.TypeExist(index, type2));
        }

        [Test]
        public void TestCreateParentChildType()
        {
            var index = "index_test_parent_child_type";

			

            var parentType = new TypeSetting("blog");
            parentType.AddStringField("title");

            var client = new ElasticSearchClient("localhost");
        	
			client.CreateIndex(index);

            var op= client.PutMapping(index, parentType);

            Assert.AreEqual(true,op.Acknowledged);

            var childType = new TypeSetting("comment",parentType);
            childType.AddStringField("comments");

            op=client.PutMapping(index,childType);
            Assert.AreEqual(true, op.Acknowledged);

            var mapping=client.GetMapping(index, "comment");

            Assert.True(mapping.IndexOf("_parent")>0);

            client.DeleteIndex(index);
        }
    }
}