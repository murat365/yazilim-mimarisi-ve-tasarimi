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