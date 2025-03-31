using BookManagementSystem.Data;
using BookManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApiContext _context;

        public BookController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("BookList")]
        public IActionResult BookList() 
        {
           var bookList = _context.Books.ToList();
            if (bookList == null)
            {
                return Ok("Listelenecek Kitap Bulunamadı.");
            }   
            return Ok(bookList);
        }

        [HttpPost("CreateBook")]
        public IActionResult CreateBook([FromBody] Book book) // Bu durumda Postman'de Body -> raw -> JSON (application/json) olarak veriyi göndermeliyim.
        {
            var addBook = _context.Books.Add(book);
            if (addBook == null)
            {
                return Ok("Hata ! Kitap Eklenemedi. Tekrar deneyiniz.");
            }
            _context.SaveChanges();
            return Ok("Kitap Ekleme İşlemi Başarıyla Gerçekleştirildi.");
        }

        /* Postman'de Body -> raw -> JSON (application/json) olarak veriyi göndermeliyim. Veriyi Göndericeksem böyle göndericeksin.
           {
            "Title": "TestYazisi",
            "Author": "MertTesti",
            "PublishedYear": "2025-03-31T03:16:43.856Z"
           }

          Eğer Params Kısmından verileri girip'de göndermek istersen de bu sefer aşağıdaki gibi yapmak zorundasın 


            [HttpPost("CreateBook")]
            public IActionResult CreateBook([FromQuery] string Title, [FromQuery] string Author, [FromQuery] DateTime PublishedYear) 
            {
                var book = new Book { Title = Title, Author = Author, PublishedYear = PublishedYear };
    
                _context.Books.Add(book);
                _context.SaveChanges();

                return Ok("Kitap Ekleme İşlemi Başarıyla Gerçekleştirildi.");
            }

         Bu durumda postman'da bu URL'yi kullanmalısın : 
          POST https://localhost:7148/api/Book/CreateBook?Title=TestYazisi&Author=MertTesti&PublishedYear=2025-03-31T03:16:43.856Z
        */

        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook([FromBody] Book book) 
        {
            var updateBook = _context.Books.Update(book);
            if (updateBook == null)
            {
                return Ok("Hata ! Kitap Güncellenemedi. Tekrar deneyiniz.");
            }
            _context.SaveChanges();
            return Ok("Güncelleme İşlemi Başarıyla Gerçekleştirildi.");
        }

        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int Id) 
        {
             var selectedBook = _context.Books.SingleOrDefault(x => x.Id == Id);
            if (selectedBook == null)
            {
                return Ok("Hata ! Kitap Silinemedi. Silinecek Kitap bulunamadı..");
            }
          var deleteBook =_context.Books.Remove(selectedBook);
            if (deleteBook == null)
            {
                return Ok("Hata! Kitap Silinemedi. Kitap bulundu ama silme işleminde bir hata meydana geldi.");
            }
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarıyla Gerçekleştirildi.");
        }

        [HttpGet("SelectBook")]
        public IActionResult SelectBook(int Id) 
        {
             var selectedBook = _context.Books.SingleOrDefault(x => x.Id == Id);
            if (selectedBook == null)
            {
                return Ok("Hata ! Kitap Seçilemedi. Seçilecek Kitap bulunamadı..");
            }
            return Ok(selectedBook);
        }

    }
}
