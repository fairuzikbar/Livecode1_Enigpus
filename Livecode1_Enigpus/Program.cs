using System.Data.SqlClient;
using Dapper;
using Livecode1_Enigpus.Models;

namespace Livecode1_Enigpus;

public class Program
{
    public static void Main(string[] args)
    {
        using var connection = new SqlConnection("server=localhost,1433;user=sa;password=Password12345;database=enigpus");
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        Console.WriteLine("==============================================");
        Console.WriteLine("======== WELCOME TO ENIGPUS INVENTORY ========");
        Console.WriteLine("==============================================");
        Console.WriteLine("Pilih menu di bawah ini:");
        Console.WriteLine("1. Tambahkan buku");
        Console.WriteLine("2. Lihat list buku");
        Console.WriteLine("3. Cari buku berdasarkan judul");
        Console.WriteLine("4. Hapus buku berdasarkan code");
        Console.WriteLine("5. Update buku berdasarkan code");
        Console.WriteLine("6. Keluar dari aplikasi");
        Console.WriteLine("==============================================");
        var menu = Console.ReadLine();
        var chooseMenu = new MenuProgram();
        chooseMenu.Menu(menu);
        
        // Untuk create table
        // var sql = @"
        //     CREATE TABLE inventory
        //     (
        //         id int identity,
        //         Type VARCHAR(255),
        //         Code VARCHAR(255) primary key,
        //         Title VARCHAR(255),
        //         Publisher VARCHAR(255),
        //         Year int,
        //         Author VARCHAR(255)
        //     )";
        // connection.Execute(sql);
    }
}

public class MenuProgram
{
    InventoryService inventoryService = new InventoryService();
    public void Menu(string? menu)
    {
        switch (menu)
        {
            case "1":
                Console.WriteLine("===================");
                Console.WriteLine("Inputkan jenis buku");
                Console.WriteLine("===================");
                Console.WriteLine("1. Novel");
                Console.WriteLine("2. Majalah");
                Console.WriteLine("===================");
                var choose = Console.ReadLine();
                AddMenu(choose);
                break;
            case "2":
                Console.WriteLine("===============================");
                Console.WriteLine("====== DAFTAR SEMUA BUKU ======");
                Console.WriteLine("===============================");
                inventoryService.GetAllBooks();
                break;
            case "3":
                Console.WriteLine("=========================================");
                Console.WriteLine("====== CARI BUKU BERDASARKAN JUDUL ======");
                Console.WriteLine("=========================================");
                Console.Write("Tulis judul buku yang Anda cari: ");
                var title = Console.ReadLine();
                inventoryService.SearchBook(title);
                break;
            case "4":
                Console.WriteLine("=========================================");
                Console.WriteLine("====== HAPUS BUKU BERDASARKAN CODE ======");
                Console.WriteLine("=========================================");
                Console.Write("Tulis code buku: ");
                var codeDelete = Console.ReadLine();
                inventoryService.DeleteBook(codeDelete);
                break;
            case "5":
                Console.Write("Masukkan code buku: ");
                var codeUpdate = Console.ReadLine();
                inventoryService.UpdateBook(codeUpdate);
                break;
            case "6":
                Console.WriteLine("Terima kasih telah menggunakan aplikasi ini");
                break;
            default:
                Console.WriteLine("Menu tidak tersedia");
                break;
        }
    }

    public void AddMenu(string? choose)
    {
        InventoryService inventoryService = new InventoryService();
        switch (choose)
        {
            case "1":
                Console.WriteLine("=========================");
                Console.WriteLine("TAMBAHKAN INFORMASI NOVEL");
                Console.WriteLine("=========================");
                Console.WriteLine("Inputkan code buku:");
                var codeNovel = Console.ReadLine();
                Console.WriteLine("Inputkan judul buku:");
                var titleNovel = Console.ReadLine();
                Console.WriteLine("Inputkan penerbit buku:");
                var publisherNovel = Console.ReadLine();
                Console.WriteLine("Inputkan tahun terbit buku:");
                var yearNovel = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Inputkan penulis buku:");
                var authorNovel = Console.ReadLine();
                Books novel = new Books
                {
                    Type = "Novel",
                    Code = codeNovel,
                    Title = titleNovel,
                    Publisher = publisherNovel,
                    Year = yearNovel,
                    Author = authorNovel
                };
                inventoryService.AddBook(novel);
                break;
            
            case "2":
                Console.WriteLine("===========================");
                Console.WriteLine("TAMBAHKAN INFORMASI MAJALAH");
                Console.WriteLine("===========================");
                Console.WriteLine("Inputkan code buku:");
                var codeMagz = Console.ReadLine();
                Console.WriteLine("Inputkan judul buku:");
                var titleMagz = Console.ReadLine();
                Console.WriteLine("Inputkan penerbit buku:");
                var publisherMagz = Console.ReadLine();
                Console.WriteLine("Inputkan tahun terbit buku:");
                var yearMagz = int.Parse(Console.ReadLine()!);
                
                Books magazine = new Books
                {
                    Type = "Majalah",
                    Code = codeMagz,
                    Title = titleMagz,
                    Publisher = publisherMagz,
                    Year = yearMagz,
                    Author = "-"
                };
                inventoryService.AddBook(magazine);
                break;
            default:
                Console.WriteLine("Menu tidak tersedia");
                break;
        }
    }
}