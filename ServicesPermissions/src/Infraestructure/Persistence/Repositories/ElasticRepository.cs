using Application.Data;
using Confluent.Kafka;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    internal class ElasticRepository<T> : IElasticRepository<T> where T : class
    {

        private readonly ElasticsearchClient client;

        public ElasticRepository(ElasticsearchClient client)
        {
            this.client = client;
        }

        public async Task Delete(string id) => await client.DeleteAsync<T>(id);

        public async Task<T> GetById(string id) => (await client.GetAsync<T>(id)).Source;

        public async Task<IEnumerable<string>> Index(IEnumerable<T> documents)
        {
            var indexName = typeof(T).Name.ToLower();
            var indexResponse = await client.Indices.ExistsAsync(indexName);

            if (!indexResponse.Exists)
                await client.Indices.CreateAsync(indexName, c => c
                    .Mappings(m => m
                        .AllField(f => f.Enabled())
                    )
                );

            var response = await client.IndexManyAsync(documents);
            return response.Items.Select(x => x.Id);
        }

        public async Task Update(T document, string id)
        {
            var response = await client.UpdateAsync<T,T>(id, u => u.Doc(document));
        }
    }
}
