using CRN.Application.DTOs;
using CRN.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRN.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }


    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }


    // GET: api/products/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }


    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto dto)
    {
        var product = await _productService.CreateAsync(dto);

        return Ok(product);
    }


    // PUT: api/products/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
    {
        var result = await _productService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return NoContent();
    }


    // DELETE: api/products/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}
