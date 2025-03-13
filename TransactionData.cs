namespace Sibvic.plisioapi;

public class TransactionData
{
    public List<TransactionOperation> Operations { get; set; }
    public TransactionLinks _Links { get; set; }
    public TransactionMetadata _Meta { get; set; }
}

