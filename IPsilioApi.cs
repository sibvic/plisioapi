
namespace Sibvic.plisioapi
{
    public interface IPsilioApi
    {
        Task<PsilioResponse<CreateNewInvoiceResponseData>?> CreateNewInvoice(string? currency, string orderName, long orderNumber, decimal? amount = null, string? sourceCurrency = null, decimal? sourceAmount = null, string[]? allowedPsysCids = null, string? description = null, string? callbackUrl = null, string? successCallbackUrl = null, string? failCallbackUrl = null, string? successInvoiceUrl = null, string? failInvoiceUrl = null, string? email = null, string? redirectToInvoice = null, int? expireMin = null, bool? returnExisting = null);
    }
}