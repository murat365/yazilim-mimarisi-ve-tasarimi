# yazilim-mimarisi-ve-tasarimi
## Memento mimari tasarımı 
Memento tasarım kalıbındaki esas amaç bir nesnenin önceki halinin veya hallerinin saklanması ve istenildiğinde tekrardan elde edilebilmesidir.  
En basit örnekle nesneye Ctrl-Z, Ctrl-Y yapabildiğinizi düşünün. Nesne üzerinde yaptığınız değişiklikleri geri alıp ilk haline dönebilmenizi sağlar.  
Bu deseni nesnelere, dahili durumları için geri alma işlemi(Undo) yeteneğininin kazandırılması olarak da düşünebiliriz.  
              ![Image of Class](https://github.com/murat365/yazilim-mimarisi-ve-tasarimi/blob/master/memento.png)  


```c#

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string IDNo { get; set; }

        public UserMemento Backup()
        {
            return new UserMemento
            {
                UserId = this.UserId,
                Name = this.Name,
                IDNo = this.IDNo
            };
        }

        public void Restore(UserMemento memento)
        {
            this.UserId = memento.UserId;
            this.Name = memento.Name;
            this.IDNo = memento.IDNo;
        }

        public override string ToString()
        {
            return String.Format("User ID: {0} Name: {1} ID NO: {2}\n", UserId, Name, IDNo);
        }
    }
```  
Originator  Class: Bu sınıf üzerinde user ile ilgili alanlar tutuluyor. Bunlara ek olarakta nesneyi Backup ve Restore etmemize yarayan metodlar bu sınıfa yazılıyor.  
```c#

    public class UserMemento
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string IDNo { get; set; }
    }
    
```  
Memento Class: Bu sınıf user nesnesinin alanlarının aynısını tutuyor.  

``` c#

    public class Memory
    {
        public UserMemento Memento { get; set; }
    }
```  
Caretaker Class: Bu sınıf memento nesnesini ya da geri dönüş n adım geriye olacaksa memento nesnelerini tutacak. Bizim örneğimiz sadece tek adım geriye gidilecek şekilde tasarlandığı için sadece bir memento nesnesi tutuyor.  
```c#

    class Program
    {
        static void Main(string[] args)
        {
            Memory memory = new Memory();
            User user = new User { UserId = 123123, Name = "Ahmet", IDNo = "1232134353" };

            //User nesnesinin ilk hali yazdırılıyor
            Console.WriteLine(user.ToString());

            //Memento nesnesine backup alınıyor
            memory.Memento = user.Backup();

            //User nesnesi üzerinde değişiklikler yapılıyor
            user.Name = "Mehmet";
            user.IDNo = "2423552342";

            //Değişiklikler sonrası user nesnesi yazdırılıyor
            Console.WriteLine(user.ToString());

            //User nesnesi memento üzerinden restore ediliyor
            user.Restore(memory.Memento);

            //Restore sonrası user nesnesi
            Console.WriteLine(user.ToString());

            Console.ReadLine();


        }
    }
```
## Adapter tasarım deseni
Adapter tasarım deseni, structural tasarım desenlerinden biridir. Bu tasarım deseni, birbiriyle ilişkili olmayan interface'lerin birlikte çalışmasını sağlar. Bu işlemi ise, bir sınıfın interface'ini diğer bir interface'e dönüştürerek yapar.  

Adapter tasarım deseni ismini gerçek hayattaki adaptörlerden almıştır. Örneğin, telefon şarz cihazı bir adaptördür. 240V'luk gerilimi 5V'a dönüştürür. Mobil şarz cihazı, mobil şarz soketi ile duvar soketi arasında bir adaptör görevi görür.  

Öncelikle, geliştirdiğimiz istemcinin beklediği arayüzü tasarlamamız gerekiyor (fişinizi nereye takabiliyorsunuz ?) . Sonra da adapte edilecek nesneye erişen bir sınıfa ihtiyacımız olacak (duvardaki prizin neye ihtiyacı var ?).Kodları bir senaryo yardımıyla yazıyoruz.Diyelim ki, POS cihazından gelen veriyi okumak üzere bir uygulama yazıyorsunuz. Cihazın API’si tarafından nesnemiz de aşağıdaki gibi olsun:
```c#
public class XBankPOSReader
{

   public string ReadFromCard()
 
   {
       
      return "X bank Pos cihazı çalışıyor";
    
    }

} 
```
Daha önce de belirttiğimiz gibi, bu örnekte de düşünmemiz gereken şeylerden biri şu olmalı: uygulama sadece bir POS cihazına (ve onun API’sine) bağlı olmamalı.Öyleyse, tüm POS cihazları ile çalışabilecek bir adaptör arayüzü tasarlamamız gerekiyor:
```c#
public interface ICardAdapter

{

   public string ReadCardData();

}
```
İstemci, işte bu arayüz üzerinden cihaz API’si ile iletişim kurabilecek. POS cihazı ile çalışacak olan her sınıfı bu interface’den implemente etmemiz gerekiyor. Şu anda elimizde bulunan X Bankın POS cihazı için gereken sınıfımızı aşağıdaki gibi yazıyoruz:  
```c#
public class XBankCardReaderAdapter:ICardReaderAdapter
{

    public string ReadCardData()
    {

      XBankPOSReader posReader = new XBankPOSReader();
      return posReaderFromCard();
 
      }
}
  ``` 
Bu sınıfa dikkat etmeliyiz. Tanımladığımız interface’den gelen ReadCardData() metodu, adapte edilecek nesnenin (XBankPOSReader) ilgili metodunu (ReadFromCard()) çağırıyor.  

Yani istemci, XBankCardReaderAdapter adaptör nesnesine erişecek (şarj aletini adaptöre takacak). Adaptör de, asıl nesne üzerindeki ihtiyacımız olan metodu çağıracak (adaptörü duvardaki prize takacak).İstemcinin kodlarını aşağıda belirtiyoruz:  
```c#
static void Main(string[] args)
{

   ICardReaderAdapter reader = new XBankCardReaderAdapter();
   var result = reader.ReadCardData();
   Console.WriteLine(result);
   
}
```
![Image of Class](https://github.com/murat365/yazilim-mimarisi-ve-tasarimi/blob/master/Adapter.png)



