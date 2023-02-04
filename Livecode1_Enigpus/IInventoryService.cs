using Livecode1_Enigpus.Models;

namespace Livecode1_Enigpus;

public interface IInventoryService
{
    void AddBook(Books book);
    void SearchBook(string title);
    void GetAllBooks();
    void DeleteBook(string code);
    void UpdateBook(string code);
}