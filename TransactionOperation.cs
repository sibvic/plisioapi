namespace Sibvic.plisioapi;

public class TransactionOperation
{
    public string? Txn_Id { get; set; }
    public string? Invoice_Url { get; set; }
    public decimal? Invoice_Total_Sum { get; set; }
    public int User_Id { get; set; }
    public string? Shop_Id { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string[] Tx_Url { get; set; }
    public string? Id { get; set; }
}

