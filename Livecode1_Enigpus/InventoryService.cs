using System.Data.SqlClient;
using Livecode1_Enigpus.Models;

namespace Livecode1_Enigpus;

public class InventoryService: IInventoryService
{
    readonly SqlConnection _connection = Dbconn.GetConnection();
    
    public void AddBook(Books book)
    {
        try
        {
            _connection.Open();
            var sql = @"
                INSERT INTO inventory (Type, Code, Title, Publisher, Year, Author)
                VALUES (@Type, @Code, @Title, @Publisher, @Year, @Author)";

            Books books = new Books
            {
                Type = book.Type,
                Code = book.Code,
                Title = book.Title,
                Publisher = book.Publisher,
                Year = book.Year,
                Author = book.Author
            };

            var command = new SqlCommand(sql, _connection);
            command.Parameters.AddWithValue("@Type", books.Type);
            command.Parameters.AddWithValue("@Code", books.Code);
            command.Parameters.AddWithValue("@Title", books.Title);
            command.Parameters.AddWithValue("@Publisher", books.Publisher);
            command.Parameters.AddWithValue("@Year", books.Year);
            command.Parameters.AddWithValue("@Author", books.Author);

            command.ExecuteNonQuery();
            Console.WriteLine("Data inserted successfully");
            Console.WriteLine($"{nameof(books.Type)}: {books.Type}, " +
                              $"{nameof(books.Code)}: '{books.Code}', " +
                              $"{nameof(books.Title)}: '{books.Title}', " +
                              $"{nameof(books.Publisher)}: '{books.Publisher}', " +
                              $"{nameof(books.Year)}: {books.Year}, " +
                              $"{nameof(books.Author)}: '{books.Author}'");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }

    public void SearchBook(string? title)
    {
        try
        {
            _connection.Open();
            const string sql = "SELECT * FROM inventory WHERE Title = @Title";
            var command = new SqlCommand(sql, _connection);
            command.Parameters.AddWithValue("@Title", title);
            
            var dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                Console.WriteLine($"Hasil pencarian dengan judul: {title}");
                Console.WriteLine($"Type: {dataReader.GetString(1)}, Code: '{dataReader.GetString(2)}', Title: '{dataReader.GetString(3)}', Publisher: '{dataReader.GetString(4)}', Year: {dataReader.GetInt32(5)}, Author: '{dataReader.GetString(6)}'");
                Console.WriteLine("=======================================");
            }
            else
            {
                Console.WriteLine("Data tidak ditemukan");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }

    public void GetAllBooks()
    {
        try
        {
            _connection.Open();
            const string sql = @"SELECT * FROM inventory";
            var command = new SqlCommand(sql, _connection);
            var reader = command.ExecuteReader();
            var books = new List<Books>();

            while (reader.Read())
            {
                books.Add(new Books
                {
                    Type = Convert.ToString(reader["type"]),
                    Code = Convert.ToString(reader["code"]),
                    Title = Convert.ToString(reader["title"]),
                    Publisher = Convert.ToString(reader["publisher"]),
                    Year = Convert.ToInt32(reader["year"]),
                    Author = Convert.ToString(reader["author"])
                });
            }

            foreach (var book in books)
            {
                Console.WriteLine($"{nameof(book.Type)}: {book.Type}, {nameof(book.Code)}: '{book.Code}', {nameof(book.Title)}: '{book.Title}', {nameof(book.Publisher)}: '{book.Publisher}', {nameof(book.Year)}: {book.Year}, {nameof(book.Author)}: '{book.Author}'");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }

    public void DeleteBook(string? code)
    {
        try
        {
            _connection.Open();
            const string sql = "DELETE FROM inventory WHERE Code = @Code";
            var command = new SqlCommand(sql, _connection);
            command.Parameters.AddWithValue("@Code", code);
            command.ExecuteNonQuery();
            Console.WriteLine("Data berhasil dihapus");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }

    public void UpdateBook(string code)
    {
        try
        {
            _connection.Open();
            var select = @"SELECT * FROM inventory where Code = @Code";
            var commandSelect = new SqlCommand(select, _connection);
            commandSelect.Parameters.AddWithValue("@Code", code);
            var reader = commandSelect.ExecuteReader();
            var bookSelect = new List<Books>();

            while (reader.Read())
            {
                bookSelect.Add(new Books
                {
                    Type = Convert.ToString(reader["type"]),
                    Code = Convert.ToString(reader["code"]),
                    Title = Convert.ToString(reader["title"]),
                    Publisher = Convert.ToString(reader["publisher"]),
                    Year = Convert.ToInt32(reader["year"]),
                    Author = Convert.ToString(reader["author"])
                });
            }
            reader.Close();
            var sql = @"
                UPDATE inventory SET Type = @Type, Title = @Title, Publisher = @Publisher, Year = @Year, Author = @Author
                WHERE Code = @Code";
            
            Console.WriteLine("=====================");
            Console.WriteLine("UPDATE INFORMASI BUKU");
            Console.WriteLine("=====================");
            Console.WriteLine("Inputkan judul buku:");
            var title = Console.ReadLine();
            if (title != "")
            {
                bookSelect[0].Title = title;
            }
            else
            {
                bookSelect[0].Title = bookSelect[0].Title;
            }
            Console.WriteLine("Inputkan penerbit buku:");
            var publisher = Console.ReadLine();
            if (publisher != "")
            {
                bookSelect[0].Publisher = publisher;
            }
            else
            {
                bookSelect[0].Publisher = bookSelect[0].Publisher;
            }
            Console.WriteLine("Inputkan tahun terbit buku:");
            var yearString = Console.ReadLine();
            
            if (yearString != "")
            {
                bookSelect[0].Year = int.Parse(yearString!);
            }
            else
            {
                bookSelect[0].Year = bookSelect[0].Year;
            }
            Console.WriteLine("Inputkan penulis buku:");
            var author = Console.ReadLine();
            if (author != "")
            {
                bookSelect[0].Author = author;
            }
            else
            {
                bookSelect[0].Author = bookSelect[0].Author;
            }
            
            Books books = new Books
            {
                Type = bookSelect[0].Type,
                Code = bookSelect[0].Code,
                Title = bookSelect[0].Title,
                Publisher = bookSelect[0].Publisher,
                Year = bookSelect[0].Year,
                Author = bookSelect[0].Author
            };
            
            var command = new SqlCommand(sql, _connection);
            command.Parameters.AddWithValue("@Type", bookSelect[0].Type);
            command.Parameters.AddWithValue("@Code", bookSelect[0].Code);
            command.Parameters.AddWithValue("@Title", bookSelect[0].Title);
            command.Parameters.AddWithValue("@Publisher", bookSelect[0].Publisher);
            command.Parameters.AddWithValue("@Year", bookSelect[0].Year);
            command.Parameters.AddWithValue("@Author", bookSelect[0].Author);
            
            command.ExecuteNonQuery();
            Console.WriteLine("Data berhasil diupdate");
            Console.WriteLine($"{nameof(books.Type)}: {books.Type}, " +
                              $"{nameof(books.Code)}: '{books.Code}', " +
                              $"{nameof(books.Title)}: '{books.Title}', " +
                              $"{nameof(books.Publisher)}: '{books.Publisher}', " +
                              $"{nameof(books.Year)}: {books.Year}, " +
                              $"{nameof(books.Author)}: '{books.Author}'");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _connection.Close();
        }
    }
}