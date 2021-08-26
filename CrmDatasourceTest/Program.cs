using System;
using System.Threading.Tasks;
using MyCrm.PersonalData.Grpc;
using MyCrm.PersonalData.Grpc.Contracts;
using MyJetWallet.Sdk.Grpc;

namespace CrmDatasourceTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var crmPersonalDataFactory = new MyGrpcClientFactory("http://crm-datasource.crm.svc.cluster.local:80");
            var client = crmPersonalDataFactory.CreateGrpcService<IMyCrmPersonalDataGrpcService>();

            await GetData(client, "alexey.n+2@smplt.net");
            await GetData(client, "b22e9a05e050467ba9a32488e1c28f49");

        }

        private static async Task GetData(IMyCrmPersonalDataGrpcService client, string phase)
        {
            var resp = client.GetByPhraseAsync(new GetByPhraseGrpcRequest()
            {
                Phrase = phase,
                UserId = "test-user"
            });

            Console.WriteLine("Print:");

            await foreach (var item in resp)
            {
                Console.WriteLine($"{item.Email}  :  {item.Id}");
            }

            Console.WriteLine("End");
        }
    }
}
