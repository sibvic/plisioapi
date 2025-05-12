using Newtonsoft.Json;

namespace Sibvic.plisioapi;
public class PsilioApi(PsilioOptions options) : IPsilioApi
{
    public async Task<PsilioResponse<CreateNewInvoiceResponseData>?> CreateNewInvoiceAsync(string? currency, string orderName, long orderNumber, decimal? amount = null,
        string? sourceCurrency = null, decimal? sourceAmount = null, string[]? allowedPsysCids = null, string? description = null, string? callbackUrl = null,
        string? successCallbackUrl = null, string? failCallbackUrl = null, string? successInvoiceUrl = null, string? failInvoiceUrl = null, string? email = null,
        string? redirectToInvoice = null, int? expireMin = null, bool? returnExisting = null)
    {
        if (string.IsNullOrEmpty(options.Key))
        {
            return null;
        }
        HttpClientHandler clientHandler = new()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        };
        var client = new HttpClient(clientHandler) { BaseAddress = new Uri("https://api.plisio.net") };

        List<string> parameters = [];
        if (!string.IsNullOrEmpty(currency)) { parameters.Add("currency=" + currency); }
        parameters.Add("order_name=" + orderName);
        parameters.Add("order_number=" + orderNumber);
        if (amount.HasValue) { parameters.Add("amount=" + amount); }
        if (!string.IsNullOrEmpty(sourceCurrency)) { parameters.Add("source_currency=" + sourceCurrency); }
        if (sourceAmount.HasValue) { parameters.Add("source_amount=" + sourceAmount); }
        if (allowedPsysCids != null && allowedPsysCids.Length > 0) { parameters.Add("allowed_psys_cids=" + string.Join(";", allowedPsysCids)); }
        if (!string.IsNullOrEmpty(description)) { parameters.Add("description=" + description); }
        if (!string.IsNullOrEmpty(callbackUrl)) { parameters.Add("callback_url=" + callbackUrl); }
        if (!string.IsNullOrEmpty(successCallbackUrl)) { parameters.Add("success_callback_url=" + successCallbackUrl); }
        if (!string.IsNullOrEmpty(failCallbackUrl)) { parameters.Add("fail_callback_url=" + failCallbackUrl); }
        if (!string.IsNullOrEmpty(successInvoiceUrl)) { parameters.Add("success_invoice_url=" + successInvoiceUrl); }
        if (!string.IsNullOrEmpty(failInvoiceUrl)) { parameters.Add("fail_invoice_url=" + failInvoiceUrl); }
        if (!string.IsNullOrEmpty(email)) { parameters.Add("email=" + email); }
        if (!string.IsNullOrEmpty(redirectToInvoice)) { parameters.Add("emredirect_to_invoiceail=" + redirectToInvoice); }
        parameters.Add("api_key=" + options.Key);
        if (expireMin.HasValue) { parameters.Add("expire_min=" + expireMin); }
        if (returnExisting.HasValue) { parameters.Add("return_existing=" + returnExisting); }

        var result = await client.GetAsync("/api/v1/invoices/new?" + string.Join("&", parameters));
        var res = await result.Content.ReadAsStringAsync();
        if (result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<PsilioResponse<CreateNewInvoiceResponseData>>(res);
        }
        var failData = JsonConvert.DeserializeObject<PsilioResponse<ResponseFailData>>(res);
        throw new BadRequestException(failData.Data.Message);
    }

    public async Task<PsilioResponse<TransactionData>?> GetTransactionsAsync()
    {
        if (string.IsNullOrEmpty(options.Key))
        {
            return null;
        }
        HttpClientHandler clientHandler = new()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        };
        var client = new HttpClient(clientHandler) { BaseAddress = new Uri("https://api.plisio.net") };
        //https://api.plisio.net/?api_key=SECRET_KEY
        List<string> parameters = [];
        parameters.Add("api_key=" + options.Key);
        
        var result = await client.GetAsync("/api/v1/operations?" + string.Join("&", parameters));
        var res = await result.Content.ReadAsStringAsync();
        if (result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<PsilioResponse<TransactionData>>(res);
        }
        var failData = JsonConvert.DeserializeObject<PsilioResponse<ResponseFailData>>(res);
        throw new BadRequestException(failData.Data.Message);
    }
}
