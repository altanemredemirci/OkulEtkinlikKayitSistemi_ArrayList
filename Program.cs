using System.Collections;
using System.Text;

namespace OkulEtkinlikKayitSistemi_ArrayList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Okul Etkinlik Kayıt Sistemi - ArrayList";

            /*
             * Bir etkinliğe ait bütün bilgiler aynı indekste tutulur.
             * Örneğin etkinlikAdlari[0] ile kontenjanlar[0] aynı etkinliğe aittir.
             */
            ArrayList etkinlikAdlari = new ArrayList();
            ArrayList etkinlikTarihleri = new ArrayList();
            ArrayList kontenjanlar = new ArrayList();
            ArrayList kayitSayilari = new ArrayList();
            ArrayList ucretler = new ArrayList();
            ArrayList etkinlikAktifMi = new ArrayList();

            // Başlangıç etkinlikleri
            etkinlikAdlari.Add("Yazılım Semineri");
            etkinlikTarihleri.Add(DateTime.Today.AddDays(10));
            kontenjanlar.Add(40);
            kayitSayilari.Add(0);
            ucretler.Add(0m);
            etkinlikAktifMi.Add(true);

            etkinlikAdlari.Add("Müze Gezisi");
            etkinlikTarihleri.Add(DateTime.Today.AddDays(18));
            kontenjanlar.Add(25);
            kayitSayilari.Add(0);
            ucretler.Add(150m);
            etkinlikAktifMi.Add(true);

            etkinlikAdlari.Add("Satranç Turnuvası");
            etkinlikTarihleri.Add(DateTime.Today.AddDays(25));
            kontenjanlar.Add(32);
            kayitSayilari.Add(0);
            ucretler.Add(50m);
            etkinlikAktifMi.Add(true);

            etkinlikAdlari.Add("Bahar Konseri");
            etkinlikTarihleri.Add(DateTime.Today.AddDays(35));
            kontenjanlar.Add(80);
            kayitSayilari.Add(0);
            ucretler.Add(200m);
            etkinlikAktifMi.Add(true);

            // Öğrenci kayıtları da kendi aralarında aynı indeksi kullanır.
            ArrayList ogrenciNumaralari = new ArrayList();
            ArrayList ogrenciAdlari = new ArrayList();
            ArrayList kayitliEtkinlikIndexleri = new ArrayList();
            ArrayList kayitTarihleri = new ArrayList();

            const string yoneticiSifresi = "1234";
            bool programCalisiyor = true;

            while (programCalisiyor)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================================");
                Console.WriteLine("       OKUL ETKİNLİK KAYIT SİSTEMİ");
                Console.WriteLine("              ARRAYLIST SÜRÜMÜ");
                Console.WriteLine("================================================");
                Console.ResetColor();
                Console.WriteLine("1 - Etkinlikleri Listele");
                Console.WriteLine("2 - Etkinlik Ara");
                Console.WriteLine("3 - Etkinliğe Kayıt Ol");
                Console.WriteLine("4 - Kaydı İptal Et");
                Console.WriteLine("5 - Öğrencinin Kayıtlarını Göster");
                Console.WriteLine("6 - Yönetici Paneli");
                Console.WriteLine("7 - Raporlar");
                Console.WriteLine("0 - Çıkış");
                Console.Write("Seçiminiz: ");
                string anaMenuSecimi = Console.ReadLine();

                try
                {
                    switch (anaMenuSecimi)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("ETKİNLİK LİSTESİ");
                            Console.WriteLine(new string('-', 75));

                            if (etkinlikAdlari.Count == 0)
                            {
                                Console.WriteLine("Sistemde etkinlik bulunmamaktadır.");
                            }

                            for (int i = 0; i < etkinlikAdlari.Count; i++)
                            {
                                // ArrayList değerleri object olduğu için uygun tipe dönüştürülür.
                                string ad = (string)etkinlikAdlari[i];
                                DateTime tarih = (DateTime)etkinlikTarihleri[i];
                                int kontenjan = (int)kontenjanlar[i];
                                int kayit = (int)kayitSayilari[i];
                                decimal ucret = (decimal)ucretler[i];
                                bool aktifMi = (bool)etkinlikAktifMi[i];

                                string durum = aktifMi ? "Aktif" : "İptal edildi";
                                if (aktifMi && tarih.Date < DateTime.Today)
                                {
                                    durum = "Tarihi geçti";
                                }

                                Console.WriteLine($"Etkinlik No     : {i + 1}");
                                Console.WriteLine($"Etkinlik Adı    : {ad}");
                                Console.WriteLine($"Tarih           : {tarih:dd.MM.yyyy}");
                                Console.WriteLine($"Ücret           : {ucret:0.00} TL");
                                Console.WriteLine($"Kontenjan       : {kontenjan}");
                                Console.WriteLine($"Kalan Kontenjan : {kontenjan - kayit}");
                                Console.WriteLine($"Durum           : {durum}");
                                Console.WriteLine(new string('-', 75));
                            }

                            Console.WriteLine("Devam etmek için bir tuşa basınız...");
                            Console.ReadKey();
                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("ETKİNLİK ARAMA");
                            Console.Write("Aranacak etkinlik adı: ");
                            string arananKelime = (Console.ReadLine() ?? "").Trim().ToLower();

                            if (arananKelime.Length == 0)
                            {
                                Console.WriteLine("Arama metni boş bırakılamaz.");
                            }
                            else
                            {
                                bool bulundu = false;

                                for (int i = 0; i < etkinlikAdlari.Count; i++)
                                {
                                    string etkinlikAdi = (string)etkinlikAdlari[i];

                                    if (etkinlikAdi.ToLower().Contains(arananKelime))
                                    {
                                        bulundu = true;
                                        DateTime tarih = (DateTime)etkinlikTarihleri[i];
                                        int kalan = (int)kontenjanlar[i] - (int)kayitSayilari[i];
                                        decimal ucret = (decimal)ucretler[i];
                                        bool aktifMi = (bool)etkinlikAktifMi[i];

                                        Console.WriteLine();
                                        Console.WriteLine($"{i + 1} - {etkinlikAdi}");
                                        Console.WriteLine($"Tarih: {tarih:dd.MM.yyyy}");
                                        Console.WriteLine($"Ücret: {ucret:0.00} TL");
                                        Console.WriteLine($"Kalan kontenjan: {kalan}");
                                        Console.WriteLine($"Durum: {(aktifMi ? "Aktif" : "İptal")}");
                                    }
                                }

                                if (!bulundu)
                                {
                                    Console.WriteLine("Arama ölçütüne uygun etkinlik bulunamadı.");
                                }
                            }

                            Console.WriteLine("\nDevam etmek için bir tuşa basınız...");
                            Console.ReadKey();
                            break;

                        case "3":
                            Console.Clear();
                            Console.WriteLine("ETKİNLİĞE KAYIT OL");

                            bool uygunEtkinlikVar = false;
                            for (int i = 0; i < etkinlikAdlari.Count; i++)
                            {
                                DateTime tarih = (DateTime)etkinlikTarihleri[i];
                                int kontenjan = (int)kontenjanlar[i];
                                int kayit = (int)kayitSayilari[i];
                                bool aktifMi = (bool)etkinlikAktifMi[i];

                                if (aktifMi && tarih.Date >= DateTime.Today && kayit < kontenjan)
                                {
                                    uygunEtkinlikVar = true;
                                    Console.WriteLine($"{i + 1} - {etkinlikAdlari[i]} | {tarih:dd.MM.yyyy} | {(decimal)ucretler[i]:0.00} TL | Kalan: {kontenjan - kayit}");
                                }
                            }

                            if (!uygunEtkinlikVar)
                            {
                                Console.WriteLine("Kayıt yapılabilecek etkinlik bulunmamaktadır.");
                                Console.ReadKey();
                                break;
                            }

                            Console.Write("\nÖğrenci numarası: ");
                            string ogrenciNo = (Console.ReadLine() ?? "").Trim();
                            Console.Write("Öğrencinin adı ve soyadı: ");
                            string ogrenciAdi = (Console.ReadLine() ?? "").Trim();
                            Console.Write("Etkinlik numarası: ");
                            bool etkinlikNoGecerli = int.TryParse(Console.ReadLine(), out int etkinlikNo);

                            if (ogrenciNo.Length == 0 || ogrenciAdi.Length == 0)
                            {
                                Console.WriteLine("Öğrenci numarası ve adı boş bırakılamaz.");
                            }
                            else if (!etkinlikNoGecerli || etkinlikNo < 1 || etkinlikNo > etkinlikAdlari.Count)
                            {
                                Console.WriteLine("Geçerli bir etkinlik numarası giriniz.");
                            }
                            else
                            {
                                int etkinlikIndex = etkinlikNo - 1;
                                bool aktifMi = (bool)etkinlikAktifMi[etkinlikIndex];
                                DateTime tarih = (DateTime)etkinlikTarihleri[etkinlikIndex];
                                int kontenjan = (int)kontenjanlar[etkinlikIndex];
                                int kayit = (int)kayitSayilari[etkinlikIndex];

                                if (!aktifMi)
                                {
                                    Console.WriteLine("Bu etkinlik iptal edilmiştir.");
                                }
                                else if (tarih.Date < DateTime.Today)
                                {
                                    Console.WriteLine("Tarihi geçmiş etkinliğe kayıt yapılamaz.");
                                }
                                else if (kayit >= kontenjan)
                                {
                                    Console.WriteLine("Etkinliğin kontenjanı dolmuştur.");
                                }
                                else
                                {
                                    bool tekrarKayit = false;

                                    for (int i = 0; i < ogrenciNumaralari.Count; i++)
                                    {
                                        string kayitliOgrenciNo = (string)ogrenciNumaralari[i];
                                        int kayitliEtkinlik = (int)kayitliEtkinlikIndexleri[i];

                                        if (kayitliOgrenciNo.Equals(ogrenciNo, StringComparison.OrdinalIgnoreCase) &&
                                            kayitliEtkinlik == etkinlikIndex)
                                        {
                                            tekrarKayit = true;
                                            break;
                                        }
                                    }

                                    if (tekrarKayit)
                                    {
                                        Console.WriteLine("Öğrenci bu etkinliğe zaten kayıtlıdır.");
                                    }
                                    else
                                    {
                                        // Dört koleksiyona da aynı sırayla veri eklenir.
                                        ogrenciNumaralari.Add(ogrenciNo);
                                        ogrenciAdlari.Add(ogrenciAdi);
                                        kayitliEtkinlikIndexleri.Add(etkinlikIndex);
                                        kayitTarihleri.Add(DateTime.Now);

                                        kayitSayilari[etkinlikIndex] = kayit + 1;

                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Kayıt başarıyla tamamlandı.");
                                        Console.ResetColor();
                                        Console.WriteLine($"Etkinlik: {etkinlikAdlari[etkinlikIndex]}");
                                        Console.WriteLine($"Ücret: {(decimal)ucretler[etkinlikIndex]:0.00} TL");
                                        Console.WriteLine($"Kalan kontenjan: {kontenjan - (kayit + 1)}");
                                    }
                                }
                            }

                            Console.WriteLine("\nDevam etmek için bir tuşa basınız...");
                            Console.ReadKey();
                            break;

                        case "4":
                            Console.Clear();
                            Console.WriteLine("KAYIT İPTALİ");
                            Console.Write("Öğrenci numarası: ");
                            string iptalOgrenciNo = (Console.ReadLine() ?? "").Trim();
                            bool ogrencininKaydiVar = false;

                            for (int i = 0; i < ogrenciNumaralari.Count; i++)
                            {
                                string kayitliNo = (string)ogrenciNumaralari[i];
                                if (kayitliNo.Equals(iptalOgrenciNo, StringComparison.OrdinalIgnoreCase))
                                {
                                    ogrencininKaydiVar = true;
                                    int etkinlikIndex = (int)kayitliEtkinlikIndexleri[i];
                                    Console.WriteLine($"Kayıt No: {i + 1} | {etkinlikAdlari[etkinlikIndex]} | {(DateTime)etkinlikTarihleri[etkinlikIndex]:dd.MM.yyyy}");
                                }
                            }

                            if (!ogrencininKaydiVar)
                            {
                                Console.WriteLine("Öğrenciye ait kayıt bulunamadı.");
                            }
                            else
                            {
                                Console.Write("İptal edilecek kayıt numarası: ");
                                bool kayitNoGecerli = int.TryParse(Console.ReadLine(), out int kayitNo);
                                int kayitIndex = kayitNo - 1;

                                if (!kayitNoGecerli || kayitIndex < 0 || kayitIndex >= ogrenciNumaralari.Count)
                                {
                                    Console.WriteLine("Geçersiz kayıt numarası.");
                                }
                                else if (!((string)ogrenciNumaralari[kayitIndex]).Equals(iptalOgrenciNo, StringComparison.OrdinalIgnoreCase))
                                {
                                    Console.WriteLine("Seçilen kayıt bu öğrenciye ait değildir.");
                                }
                                else
                                {
                                    int etkinlikIndex = (int)kayitliEtkinlikIndexleri[kayitIndex];
                                    int etkinlikKayitSayisi = (int)kayitSayilari[etkinlikIndex];

                                    // Aynı kayıtla ilgili bütün ArrayList elemanları silinir.
                                    ogrenciNumaralari.RemoveAt(kayitIndex);
                                    ogrenciAdlari.RemoveAt(kayitIndex);
                                    kayitliEtkinlikIndexleri.RemoveAt(kayitIndex);
                                    kayitTarihleri.RemoveAt(kayitIndex);

                                    if (etkinlikKayitSayisi > 0)
                                    {
                                        kayitSayilari[etkinlikIndex] = etkinlikKayitSayisi - 1;
                                    }

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Kayıt başarıyla iptal edildi.");
                                    Console.ResetColor();
                                }
                            }

                            Console.WriteLine("\nDevam etmek için bir tuşa basınız...");
                            Console.ReadKey();
                            break;

                        case "5":
                            Console.Clear();
                            Console.WriteLine("ÖĞRENCİNİN KAYITLARI");
                            Console.Write("Öğrenci numarası: ");
                            string arananOgrenciNo = (Console.ReadLine() ?? "").Trim();
                            bool ogrenciBulundu = false;
                            decimal toplamUcret = 0;

                            for (int i = 0; i < ogrenciNumaralari.Count; i++)
                            {
                                string kayitliNo = (string)ogrenciNumaralari[i];
                                if (kayitliNo.Equals(arananOgrenciNo, StringComparison.OrdinalIgnoreCase))
                                {
                                    ogrenciBulundu = true;
                                    int etkinlikIndex = (int)kayitliEtkinlikIndexleri[i];
                                    decimal ucret = (decimal)ucretler[etkinlikIndex];
                                    toplamUcret += ucret;

                                    Console.WriteLine(new string('-', 60));
                                    Console.WriteLine($"Öğrenci         : {ogrenciAdlari[i]}");
                                    Console.WriteLine($"Etkinlik        : {etkinlikAdlari[etkinlikIndex]}");
                                    Console.WriteLine($"Etkinlik Tarihi : {(DateTime)etkinlikTarihleri[etkinlikIndex]:dd.MM.yyyy}");
                                    Console.WriteLine($"Kayıt Tarihi    : {(DateTime)kayitTarihleri[i]:dd.MM.yyyy HH:mm}");
                                    Console.WriteLine($"Ücret           : {ucret:0.00} TL");
                                }
                            }

                            if (!ogrenciBulundu)
                            {
                                Console.WriteLine("Öğrenciye ait kayıt bulunamadı.");
                            }
                            else
                            {
                                Console.WriteLine(new string('-', 60));
                                Console.WriteLine($"Toplam ücret: {toplamUcret:0.00} TL");
                            }

                            Console.WriteLine("\nDevam etmek için bir tuşa basınız...");
                            Console.ReadKey();
                            break;

                        case "6":
                            Console.Clear();
                            Console.WriteLine("YÖNETİCİ GİRİŞİ");
                            bool girisBasarili = false;

                            for (int hak = 3; hak > 0; hak--)
                            {
                                Console.Write("Şifre: ");
                                if (Console.ReadLine() == yoneticiSifresi)
                                {
                                    girisBasarili = true;
                                    break;
                                }
                                Console.WriteLine($"Şifre hatalı. Kalan hak: {hak - 1}");
                            }

                            if (!girisBasarili)
                            {
                                Console.WriteLine("Giriş başarısız. Ana menüye dönülüyor.");
                                Console.ReadKey();
                                break;
                            }

                            bool yoneticiPaneliAcik = true;
                            while (yoneticiPaneliAcik)
                            {
                                Console.Clear();
                                Console.WriteLine("YÖNETİCİ PANELİ");
                                Console.WriteLine("1 - Yeni Etkinlik Ekle");
                                Console.WriteLine("2 - Etkinlik Güncelle");
                                Console.WriteLine("3 - Etkinlik İptal Et");
                                Console.WriteLine("4 - Etkinlikleri Listele");
                                Console.WriteLine("0 - Ana Menüye Dön");
                                Console.Write("Seçiminiz: ");
                                string yoneticiSecimi = Console.ReadLine();

                                if (yoneticiSecimi == "1")
                                {
                                    Console.Clear();
                                    Console.WriteLine("YENİ ETKİNLİK EKLE");
                                    Console.Write("Etkinlik adı: ");
                                    string yeniAd = (Console.ReadLine() ?? "").Trim();

                                    DateTime yeniTarih;
                                    while (true)
                                    {
                                        Console.Write("Etkinlik tarihi (gg.aa.yyyy): ");
                                        if (DateTime.TryParse(Console.ReadLine(), out yeniTarih) && yeniTarih.Date >= DateTime.Today)
                                        {
                                            yeniTarih = yeniTarih.Date;
                                            break;
                                        }
                                        Console.WriteLine("Bugün veya daha ileri bir tarih giriniz.");
                                    }

                                    int yeniKontenjan;
                                    while (true)
                                    {
                                        Console.Write("Kontenjan: ");
                                        if (int.TryParse(Console.ReadLine(), out yeniKontenjan) && yeniKontenjan > 0)
                                        {
                                            break;
                                        }
                                        Console.WriteLine("Sıfırdan büyük bir tam sayı giriniz.");
                                    }

                                    decimal yeniUcret;
                                    while (true)
                                    {
                                        Console.Write("Ücret: ");
                                        if (decimal.TryParse(Console.ReadLine(), out yeniUcret) && yeniUcret >= 0)
                                        {
                                            break;
                                        }
                                        Console.WriteLine("Sıfır veya daha büyük bir ücret giriniz.");
                                    }

                                    if (yeniAd.Length == 0)
                                    {
                                        Console.WriteLine("Etkinlik adı boş bırakılamaz.");
                                    }
                                    else
                                    {
                                        // Bütün etkinlik koleksiyonlarına aynı sırayla veri eklenir.
                                        etkinlikAdlari.Add(yeniAd);
                                        etkinlikTarihleri.Add(yeniTarih);
                                        kontenjanlar.Add(yeniKontenjan);
                                        kayitSayilari.Add(0);
                                        ucretler.Add(yeniUcret);
                                        etkinlikAktifMi.Add(true);

                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Etkinlik başarıyla eklendi.");
                                        Console.ResetColor();
                                    }
                                    Console.ReadKey();
                                }
                                else if (yoneticiSecimi == "2")
                                {
                                    Console.Clear();
                                    Console.WriteLine("ETKİNLİK GÜNCELLE");

                                    for (int i = 0; i < etkinlikAdlari.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1} - {etkinlikAdlari[i]} | {(DateTime)etkinlikTarihleri[i]:dd.MM.yyyy} | Kayıt: {kayitSayilari[i]}/{kontenjanlar[i]}");
                                    }

                                    Console.Write("Güncellenecek etkinlik numarası: ");
                                    bool noGecerli = int.TryParse(Console.ReadLine(), out int guncellenecekNo);
                                    int etkinlikIndex = guncellenecekNo - 1;

                                    if (!noGecerli || etkinlikIndex < 0 || etkinlikIndex >= etkinlikAdlari.Count)
                                    {
                                        Console.WriteLine("Geçersiz etkinlik numarası.");
                                    }
                                    else
                                    {
                                        Console.Write("Yeni etkinlik adı: ");
                                        string guncelAd = (Console.ReadLine() ?? "").Trim();

                                        DateTime guncelTarih;
                                        while (true)
                                        {
                                            Console.Write("Yeni tarih (gg.aa.yyyy): ");
                                            if (DateTime.TryParse(Console.ReadLine(), out guncelTarih) && guncelTarih.Date >= DateTime.Today)
                                            {
                                                guncelTarih = guncelTarih.Date;
                                                break;
                                            }
                                            Console.WriteLine("Geçerli bir tarih giriniz.");
                                        }

                                        int mevcutKayit = (int)kayitSayilari[etkinlikIndex];
                                        int guncelKontenjan;
                                        while (true)
                                        {
                                            Console.Write("Yeni kontenjan: ");
                                            if (int.TryParse(Console.ReadLine(), out guncelKontenjan) && guncelKontenjan >= mevcutKayit && guncelKontenjan > 0)
                                            {
                                                break;
                                            }
                                            Console.WriteLine($"Kontenjan mevcut kayıt sayısından ({mevcutKayit}) az olamaz.");
                                        }

                                        decimal guncelUcret;
                                        while (true)
                                        {
                                            Console.Write("Yeni ücret: ");
                                            if (decimal.TryParse(Console.ReadLine(), out guncelUcret) && guncelUcret >= 0)
                                            {
                                                break;
                                            }
                                            Console.WriteLine("Geçerli bir ücret giriniz.");
                                        }

                                        if (guncelAd.Length == 0)
                                        {
                                            Console.WriteLine("Etkinlik adı boş bırakılamaz.");
                                        }
                                        else
                                        {
                                            etkinlikAdlari[etkinlikIndex] = guncelAd;
                                            etkinlikTarihleri[etkinlikIndex] = guncelTarih;
                                            kontenjanlar[etkinlikIndex] = guncelKontenjan;
                                            ucretler[etkinlikIndex] = guncelUcret;
                                            Console.WriteLine("Etkinlik başarıyla güncellendi.");
                                        }
                                    }
                                    Console.ReadKey();
                                }
                                else if (yoneticiSecimi == "3")
                                {
                                    Console.Clear();
                                    Console.WriteLine("ETKİNLİK İPTALİ");

                                    for (int i = 0; i < etkinlikAdlari.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1} - {etkinlikAdlari[i]} | {((bool)etkinlikAktifMi[i] ? "Aktif" : "İptal")}");
                                    }

                                    Console.Write("İptal edilecek etkinlik numarası: ");
                                    bool noGecerli = int.TryParse(Console.ReadLine(), out int iptalNo);
                                    int etkinlikIndex = iptalNo - 1;

                                    if (!noGecerli || etkinlikIndex < 0 || etkinlikIndex >= etkinlikAdlari.Count)
                                    {
                                        Console.WriteLine("Geçersiz etkinlik numarası.");
                                    }
                                    else if (!(bool)etkinlikAktifMi[etkinlikIndex])
                                    {
                                        Console.WriteLine("Etkinlik daha önce iptal edilmiştir.");
                                    }
                                    else
                                    {
                                        etkinlikAktifMi[etkinlikIndex] = false;

                                        // İptal edilen etkinliğe ait kayıtlar RemoveAt ile silinir.
                                        int i = 0;
                                        while (i < kayitliEtkinlikIndexleri.Count)
                                        {
                                            if ((int)kayitliEtkinlikIndexleri[i] == etkinlikIndex)
                                            {
                                                ogrenciNumaralari.RemoveAt(i);
                                                ogrenciAdlari.RemoveAt(i);
                                                kayitliEtkinlikIndexleri.RemoveAt(i);
                                                kayitTarihleri.RemoveAt(i);
                                            }
                                            else
                                            {
                                                i++;
                                            }
                                        }

                                        kayitSayilari[etkinlikIndex] = 0;
                                        Console.WriteLine("Etkinlik ve ilgili öğrenci kayıtları iptal edildi.");
                                    }
                                    Console.ReadKey();
                                }
                                else if (yoneticiSecimi == "4")
                                {
                                    Console.Clear();
                                    for (int i = 0; i < etkinlikAdlari.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1} - {etkinlikAdlari[i]} | {(DateTime)etkinlikTarihleri[i]:dd.MM.yyyy} | {ucretler[i]:0.00} TL | {kayitSayilari[i]}/{kontenjanlar[i]} | {((bool)etkinlikAktifMi[i] ? "Aktif" : "İptal")}");
                                    }
                                    Console.ReadKey();
                                }
                                else if (yoneticiSecimi == "0")
                                {
                                    yoneticiPaneliAcik = false;
                                }
                                else
                                {
                                    Console.WriteLine("Geçersiz seçim.");
                                    Console.ReadKey();
                                }
                            }
                            break;

                        case "7":
                            Console.Clear();
                            Console.WriteLine("RAPORLAR");
                            Console.WriteLine(new string('=', 60));

                            decimal tahminiGelir = 0;
                            for (int i = 0; i < kayitliEtkinlikIndexleri.Count; i++)
                            {
                                int etkinlikIndex = (int)kayitliEtkinlikIndexleri[i];
                                tahminiGelir += (decimal)ucretler[etkinlikIndex];
                            }

                            Console.WriteLine($"Etkinlik sayısı       : {etkinlikAdlari.Count}");
                            Console.WriteLine($"Toplam öğrenci kaydı : {ogrenciNumaralari.Count}");
                            Console.WriteLine($"Tahmini toplam gelir : {tahminiGelir:0.00} TL");

                            int enFazlaIndex = -1;
                            int enAzIndex = -1;

                            for (int i = 0; i < etkinlikAdlari.Count; i++)
                            {
                                if ((bool)etkinlikAktifMi[i])
                                {
                                    if (enFazlaIndex == -1 || (int)kayitSayilari[i] > (int)kayitSayilari[enFazlaIndex])
                                    {
                                        enFazlaIndex = i;
                                    }

                                    if (enAzIndex == -1 || (int)kayitSayilari[i] < (int)kayitSayilari[enAzIndex])
                                    {
                                        enAzIndex = i;
                                    }
                                }
                            }

                            if (enFazlaIndex != -1)
                            {
                                Console.WriteLine($"En fazla kayıt alan  : {etkinlikAdlari[enFazlaIndex]} ({kayitSayilari[enFazlaIndex]})");
                                Console.WriteLine($"En az kayıt alan     : {etkinlikAdlari[enAzIndex]} ({kayitSayilari[enAzIndex]})");
                            }

                            Console.WriteLine("\nKONTENJANI DOLAN ETKİNLİKLER");
                            bool doluVar = false;
                            for (int i = 0; i < etkinlikAdlari.Count; i++)
                            {
                                if ((bool)etkinlikAktifMi[i] && (int)kayitSayilari[i] >= (int)kontenjanlar[i])
                                {
                                    doluVar = true;
                                    Console.WriteLine("- " + etkinlikAdlari[i]);
                                }
                            }
                            if (!doluVar) Console.WriteLine("Kontenjanı dolan etkinlik yoktur.");

                            Console.WriteLine("\nHİÇ KAYIT ALMAYAN ETKİNLİKLER");
                            bool kayitsizVar = false;
                            for (int i = 0; i < etkinlikAdlari.Count; i++)
                            {
                                if ((bool)etkinlikAktifMi[i] && (int)kayitSayilari[i] == 0)
                                {
                                    kayitsizVar = true;
                                    Console.WriteLine("- " + etkinlikAdlari[i]);
                                }
                            }
                            if (!kayitsizVar) Console.WriteLine("Bütün etkinlikler kayıt almıştır.");

                            Console.WriteLine("\nYAKLAŞAN ETKİNLİKLER");
                            bool yaklasanVar = false;
                            for (int i = 0; i < etkinlikAdlari.Count; i++)
                            {
                                DateTime tarih = (DateTime)etkinlikTarihleri[i];
                                double kalanGun = (tarih.Date - DateTime.Today).TotalDays;

                                if ((bool)etkinlikAktifMi[i] && kalanGun >= 0 && kalanGun <= 30)
                                {
                                    yaklasanVar = true;
                                    Console.WriteLine($"- {etkinlikAdlari[i]} | {tarih:dd.MM.yyyy} | {kalanGun:0} gün kaldı");
                                }
                            }
                            if (!yaklasanVar) Console.WriteLine("Önümüzdeki 30 günde etkinlik yoktur.");

                            Console.WriteLine("\nDevam etmek için bir tuşa basınız...");
                            Console.ReadKey();
                            break;

                        case "0":
                            Console.Write("Programdan çıkmak istediğinize emin misiniz? (E/H): ");
                            string cevap = (Console.ReadLine() ?? "").Trim().ToUpper();
                            if (cevap == "E")
                            {
                                programCalisiyor = false;
                            }
                            break;

                        default:
                            Console.WriteLine("Geçersiz seçim yaptınız.");
                            Console.WriteLine("Devam etmek için bir tuşa basınız...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception hata)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Beklenmeyen bir hata oluştu: " + hata.Message);
                    Console.ResetColor();
                    Console.WriteLine("Ana menüye dönmek için bir tuşa basınız...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Program kapatıldı. İyi günler.");
        }
    }
}
