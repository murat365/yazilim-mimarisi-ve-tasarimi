static void Main(string[] args)
{

   ICardReaderAdapter reader = new XBankCardReaderAdapter();
   var result = reader.ReadCardData();
   Consle.WriteLine(result);
   
}