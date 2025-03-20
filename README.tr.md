# Windows Session Manager

*For the English version, please see [README.md](README.md).*

Windows Session Manager, C# ile geliştirilmiş bir Windows Servis uygulamasıdır. Bu uygulama, arka planda çalışan bir zamanlayıcı (Timer) sayesinde, oturum açılmış fakat bağlantısı kesilmiş ve belirli bir süre (varsayılan olarak 60 dakika) aşım yaşamış kullanıcı oturumlarını otomatik olarak kapatır.

## Özellikler

- **Otomatik Oturum Kapatma:** Bağlantısı kesilmiş ve 60 dakikadan uzun süre aşım yaşayan kullanıcı oturumlarını kapatır.
- **Beyaz Liste Desteği:** Belirli kullanıcıların oturumlarının kapatılmasını engellemek için beyaz liste özelliği sunar.
- **Kolay Konfigürasyon:** `app.config` dosyası üzerinden zaman aşımı süresi ve beyaz liste ayarları kolaylıkla yapılandırılabilir.

## Gereksinimler

- .NET Framework (4.7.2)

## Kurulum

1. **Projeyi İndirin veya Klonlayın:**

   ```bash
   git clone https://github.com/kemalincekara/WindowsSessionManager.git
   ```

2. **Projeyi Açın:**
   
   Visual Studio 2022 ile projeyi açın.

3. **Projeyi Build Edin:**

   Visual Studio 2022'de, Menüden **Build > Build Solution** (Ctrl+Shift+B) seçeneğini kullanarak projeyi derleyin.

4. **Servis Yükleme İşlemi:**

   Proje içerisinde yer alan komut satırı argümanları ile servis işlemleri gerçekleştirilir:
   
   - **Yükleme:** `-i` veya `--install`
   - **Kaldırma:** `-u` veya `--uninstall`
   - **Başlatma:** `-s` veya `--start`
   - **Durdurma:** `-t` veya `--stop`

   Örnek kullanım:

   ```bash
   WindowsSessionManager.exe -install
   ```

## Yapılandırma

`app.config` dosyasında aşağıdaki ayarları yapabilirsiniz:

- **TimerIntervalMinutes:** Zamanlayıcının (Timer) çalıştırılma sıklığı (dakika). Varsayılan: 5 dakika.
- **SessionTimeoutMinutes:** Oturumun kapatılması için gereken süre (dakika). Varsayılan: 60 dakika.
- **WhiteList:** Oturum kapatma dışında tutulacak kullanıcı adları (virgülle ayrılmış).

Örnek `app.config` ayarları:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="TimerIntervalMinutes" value="5" />
		<add key="SessionTimeoutMinutes" value="60" />
		<add key="WhiteList" value="Administrator" />
	</appSettings>
	<startup>
		<supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
	</startup>
</configuration>
```

## Kullanım

1. **Servisi Yükleyin:** Konsol üzerinden `--install` argümanını kullanın.
2. **Servisi Başlatın:** `--start` argümanını kullanın.
3. **Servisi Durdurun:** `--stop` argümanını kullanın.
4. **Servisi Kaldırın:** `--uninstall` argümanını kullanın.

## Katkıda Bulunma

Geliştirme önerileri, hata bildirimleri veya kod katkılarınız için [Issues](https://github.com/kemalincekara/WindowsSessionManager/issues) bölümünü kullanın.

## Lisans

Bu proje, MIT Lisansı kapsamında lisanslanmıştır. Lisans detayları için [LICENSE](LICENSE.txt) dosyasını inceleyin.

---

*Bu proje, oturum yönetim işlemlerini otomatikleştirmek amacıyla geliştirilmiştir. Geri bildirimleriniz değerlidir!*