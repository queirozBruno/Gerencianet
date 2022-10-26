using Gerencianet.Classes;
using System;
using System.Web.UI;

namespace Gerencianet
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            EndpointsGerencianet endpoints = new EndpointsGerencianet();

            var response = endpoints.CreateBilletPayment(txtProductName.Value, txtCustomerName.Value, "bruno.queiroz@legalcontrol.com.br", "923.462.880-20", "(35)99955-7348", 50000, DateTime.Now.AddDays(7));
            lnkBillet.Visible = true;
            lnkBillet.HRef = response["link"];
        }
    }
}