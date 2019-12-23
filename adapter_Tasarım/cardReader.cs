public class XBankCardReaderAdapter:ICardReaderAdapter
{

    public string ReadCardData()
    {

      XBankPOSReader posReader = new XBankPOSReader();
      return posReaderFromCard();
 
      }
}
    