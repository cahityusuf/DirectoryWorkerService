# DirectoryWorkerService

# KULLANILAN TEKNOLOJİLER
1.  RabbitMQ
2.  Postgresql
3.  .Net 5.0
4.  WorkerService

# DATABASE CONNECT ISLEMLERİ
1.  Appsettings.Json dosyasında, "ConnectionStrings:DirectoryDbContext" dizininde bulunan Connectionstring'te bulunan "Database" ve "Password" bilgileri önemlidir.
bağlandığı database name DirectoryDb olarak belirlenmiştir.
2.  Password => localinizde kurulu olan postgresql databasine bağlandığınız şifredir. Her kişide farklılık gösterdiğinden boş bırakıyorum.Lütfen kendi şifrenizi giriniz.

# AÇIKLAMALAR
1.  DirectoryWorkerService mikroservisi ReportingService'ten gelen mesajı işlemektedir.
2.  Mesaj aracılığı ile gönderilen RaporId 'yi mesajdan okumaktadır.
3.  Okuduğu RaporId yi, oluşturduğu rapora ekleyerek raporun hangi rapor talebi ile olduğunu, ReportinServis mikroservisinin anlamasını sağlayacaktır.
4.  Oluşan rapora, Reporting Servisin rapor talep Id side eklendikten sonra, bu model HttpPost isteği ile ReportinService de yazığımız "ReportCapture" endpoindine göndermektedir.
5.  Rapor talebinde bulunan ve bu manada bir mesaj oluşturan Reporting Service artık gelen HttpPost requestini yakalayarak, ReportStatus durumunu "Hazırlanıyordan" => "Tamamnlandı" durumuna çeker ve zamanı şimdi ile gündeller.
6.  Aynı modelde gelen sorgu sonucu ise istek Id'si ile ReportinDetail tablosuna kaydedilir.
7.  Böylelikler tüm rapor taleplerinin detaylarıda ReportinService mikroservisi ile kayıt altına alınmış olur.
8.  Tüm geçmiş raporlara ulaşabilmemizi de sağlamış oluruz.
