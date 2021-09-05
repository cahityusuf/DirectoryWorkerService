# DirectoryWorkerService
DirectoryWorkerService mikroservisi ReportingService'ten gelen mesajı işlemektedir.
  1. Mesaj aracılığı ile gönderilen RaporId 'yi mesajdan okumaktadır.
  2. Okuduğu RaporId yi, oluşturduğu rapora ekleyerek raporun hangi rapor talebi ile olduğunu, ReportinServis mikroservisinin anlamasını sağlayacaktır.
  3. Oluşan rapora, Reporting Servisin rapor talep Id side eklendikten sonra, bu model HttpPost isteği ile ReportinService de yazığımız "ReportCapture" endpoindine göndermektedir.
  4. Rapor talebinde bulunan ve bu manada bir mesaj oluşturan Reporting Service artık gelen HttpPost requestini yakalayarak, ReportStatus durumunu "Hazırlanıyordan" => "Tamamnlandı" durumuna çeker ve zamanı şimdi ile gündeller.
  5. Aynı modelde gelen sorgu sonucu ise istek Id'si ile ReportinDetail tablosuna kaydedilir.
  6. Böylelikler tüm rapor taleplerinin detaylarıda ReportinService mikroservisi ile kayıt altına alınmış olur.
  7. Tüm geçmiş raporlara ulaşabilmemizi de sağlamış oluruz. 
