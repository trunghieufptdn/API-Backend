using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiLearn.Authorization;
using WebApiLearn.DbContext;
using WebApiLearn.Dtos;
using WebApiLearn.Models;

namespace WebApiLearn.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
//http://localhost/api/Book/
public class BookController: ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public BookController(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    // GET http://localhost:5000/api/book
    public IActionResult GetAll()
    {
        var allBoooks = _db.Books.ToList();
        return Ok(allBoooks);
    }

    [HttpGet("{id:int}")]
    // GET http://localhost:5000/api/book/5
    // id = 5
    public IActionResult GetById(int id)
    {
        var book = _db.Books.Find(id);
        return Ok(book);
    }
    
    [HttpPost]
    // POST http://localhost:5000/api/book
    public IActionResult Create(UpSertBook model)
    {
        var book = _mapper.Map<Book>(model);
        // var book = new Book()
        // {
        //     Name = model.Name,
        //     Author = model.Author,
        //     Price = model.Price,
        //     PublishDate = model.PublishDate
        // };

        _db.Books.Add(book);
        _db.SaveChanges();
        return Ok();
    }
    
    [HttpPut("{id:int}")]
    // POST http://localhost:5000/api/book
    public IActionResult Update(int id, [FromBody] UpSertBook model)
    {
        var bookDb = _db.Books.Find(id);
        if (bookDb == null)
        {
            return NotFound();
        }

        _mapper.Map(model, bookDb);
        
        _db.SaveChanges();
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    // POST http://localhost:5000/api/book
    public IActionResult Delete(int id)
    {
        var bookDb = _db.Books.Find(id);
        if (bookDb == null)
        {
            return NotFound();
        }

        _db.Books.Remove(bookDb);
        _db.SaveChanges();
        
        return Ok();
    }
}