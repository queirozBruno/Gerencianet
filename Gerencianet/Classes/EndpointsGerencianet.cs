using Gerencianet.SDK;
using System;
using System.Collections.Generic;

namespace Gerencianet.Classes
{
    public class EndpointsGerencianet
    {
        // Chaves da aplicação criada no site
        private string client_id_homologation = "Client_Id_67b68de248e8a381f2490209534878f213184aa7";
        private string client_secret_homologation = "Client_Secret_ac6df7d967b4429404e0be5633b4cd0849341168";
        private string client_id_production = "Client_Id_8e9f539f124c6af2b440d7cab97ea56800c392fb";
        private string client_secret_production = "Client_Secret_9c00c40d4f9f4749f906a706cb1f2bdfca4433cf";

        private bool ambiente = false; // altere conforme o ambiente (true = Homologação e false = producao)

        private string client_id = "";
        private string client_secret = "";

        public EndpointsGerencianet()
        {
            if (ambiente == false)
            {
                client_id = client_id_production;
                client_secret = client_secret_production;
            }
            else
            {
                client_id = client_id_homologation;
                client_secret = client_secret_homologation;
            }
        }

        /// <summary>
        /// Endpoint de geração de boleto
        /// </summary>
        /// <param name="nomeProduto"></param>
        /// <param name="nomeCliente"></param>
        /// <param name="emailCliente"></param>
        /// <param name="documento"></param>
        /// <param name="telefone"></param>
        /// <param name="valor"></param>
        /// <param name="dataExpiracao"></param>
        /// <returns></returns>
        public Dictionary<string, string> CreateBilletPayment(string nomeProduto, string nomeCliente, string emailCliente, string documento, string telefone, int valor, DateTime dataExpiracao)
        {
            Dictionary<string, string> dictionaryResponse = new Dictionary<string, string>();
            object body = null;

            //Formata documento
            documento = documento.Replace(".", "").Replace("/", "").Replace("-", "");

            //Formata telefone
            telefone = telefone.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");

            dynamic endpoints = new Endpoints(client_id, client_secret, ambiente);

            if (documento.Length > 11) //CNPJ
            {
                body = new
                {
                    items = new[]
                    {
                        new
                        {
                            name = nomeProduto,
                            value = valor,
                            amount = 1
                        }
                    },
                    //metadata = new
                    //{
                    //    notification_url = endereco_notificacao
                    //},
                    payment = new
                    {
                        banking_billet = new
                        {
                            expire_at = dataExpiracao.ToString("yyyy-MM-dd"),
                            customer = new
                            {
                                name = nomeCliente,
                                email = emailCliente,
                                phone_number = telefone,

                                juridical_person = new
                                {
                                    corporate_name = nomeCliente,
                                    cnpj = documento
                                }
                            }
                        }
                    }
                };
            }
            else
            {
                body = new
                {
                    items = new[]
                    {
                        new
                        {
                            name = nomeProduto,
                            value = valor,
                            amount = 1
                        }
                    },
                    //metadata = new
                    //{
                    //    notification_url = legalcontrol.com.br/pagamentos
                    //},
                    payment = new
                    {
                        banking_billet = new
                        {
                            expire_at = dataExpiracao.ToString("yyyy-MM-dd"),
                            customer = new
                            {
                                name = nomeCliente,
                                email = emailCliente,
                                cpf = documento,
                                phone_number = telefone
                            }
                        }
                    }
                };
            }
            var response = endpoints;
            try
            {
                response = endpoints.OneStep(null, body);

                dictionaryResponse.Add("barcode", (response.data.barcode).ToString());
                dictionaryResponse.Add("link", (response.data.link).ToString());
                dictionaryResponse.Add("pdf", (response.data.pdf).ToString());
                dictionaryResponse.Add("expire_at", (response.data.expire_at).ToString());
                dictionaryResponse.Add("charge_id", (response.data.charge_id).ToString());
                dictionaryResponse.Add("status", (response.data.status).ToString());
                dictionaryResponse.Add("payment", (response.data.payment).ToString());
                dictionaryResponse.Add("total", (response.data.total).ToString());
            }
            catch (GnException e)
            {
                dictionaryResponse.Add("error", e.Message);
                Console.WriteLine(e.ErrorType);
                Console.WriteLine(e.Message);
            }
            return dictionaryResponse;
        }
    }
}