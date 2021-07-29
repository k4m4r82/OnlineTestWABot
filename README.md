# WhatsApp Bot - Online Test

Ini adalah contoh aplikasi WhatsApp Bot dengan tema **Tes Online Bahasa Inggris**. Project ini dibuat menggunakan bahasa C# dan menggunakan library [WhatsApp NET Client](http://wa-net.coding4ever.net/) untuk mempermudah komunikasi ke aplikasi WhatsApp web.

## Library Pendukung

* [Dapper](https://www.nuget.org/packages/Dapper/)
* [Dapper.Contrib](https://www.nuget.org/packages/Dapper.Contrib/)
* [System.Data.SQLite.Core](https://www.nuget.org/packages/System.Data.SQLite.Core/)
* [WhatsApp NET Client](https://www.nuget.org/packages/WhatsAppNETAPI)

## Persyaratan Sistem

* Windows 7, 8, 10 dan windows versi terbaru
* .NET Framework 4.5 dan .NET versi terbaru
* Node.js versi 13.14.0 (khusus Windows 7)
* Node.js versi 14.16.x atau versi terbaru (untuk windows 8, 10 atau windows terbaru)
* Software git (version control)

## Instalasi

* Buka project WhatsApp Bot, kemudian klik kanan solution `OnlineTestWABot` -> `Rebuild Solution`. Langkah ini akan merestore library-library [Nuget](https://www.nuget.org/) yang digunakan project ini.
* Copykan folder **database** ke folder **bin\Debug** atau **bin\Release**.
* Setelah itu Anda bisa langsung menjalankan project dengan menekan tombol F5, kemudian mengeset lokasi direktori `WhatsAppNETAPINodeJs` dan menekan tombol `Start` untuk mengaktifkan bot.

Untuk mendapatkan direktori `WhatsAppNETAPINodeJs`, langkah-langkahnya bisa Anda baca di [sini](http://wa-net.coding4ever.net/).