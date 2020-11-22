# WhatsApp Bot - Online Test

Ini adalah contoh aplikasi WhatsApp Bot dengan tema **Tes Online Bahasa Inggris**. Project ini dibuat menggunakan bahasa C# dan menggunakan library [WhatsApp NET Client](http://wa-net.coding4ever.net/) untuk mempermudah komunikasi ke aplikasi WhatsApp web.

## Library Pendukung

Untuk membuat project ini saya menggunakan library pendukung sebagai berikut:

* [Dapper](https://www.nuget.org/packages/Dapper/)
* [Dapper.Contrib](https://www.nuget.org/packages/Dapper.Contrib/)
* [System.Data.SQLite.Core](https://www.nuget.org/packages/System.Data.SQLite.Core/)
* [WhatsApp NET Client](http://wa-net.coding4ever.net/)

## Persyaratan Sistem

* .NET Framework 4.0, 4.5 dan .NET versi terbaru
*  Versi minimal Google Chrome harus versi 87.xx (jadi klo belum sama silahkan diupdate dulu)

## Instalasi

* Buka project WhatsApp Bot, kemudian klik kanan solution `OnlineTestWABot` -> `Rebuild Solution`. Langkah ini akan merestore library-library [Nuget](https://www.nuget.org/) yang digunakan project ini.
* Copykan file [chromedriver.exe](https://github.com/k4m4r82/OnlineTestWABot/tree/master/libs) yang ada di folder **libs** ke folder **bin\Debug** atau **bin\Release**.
* Copykan folder **database** ke folder **bin\Debug** atau **bin\Release**.
* Setelah itu Anda bisa menjalankan project kemudian klik tombol `Start`.

## Solusi Error

Error yang sering terjadi adalah ketika versi google chrome Anda tidak kompatibel dengan versi ChromeDriver (WebDriver for Chrome), sehingga Anda harus mendownload ulang file [chromedriver.exe](https://chromedriver.chromium.org/downloads) yang menyesuaikan dengan versi google chrome Anda.