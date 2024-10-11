using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.FeatureDto;
using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult ProductList()
        {
            var value = _mapper.Map<List<ResultProductDto>>(_productService.TGetListAll());
            return Ok(value);
        }

        [HttpGet("ProductCount")]
        public ActionResult ProductCount()
        {
            return Ok(_productService.TProductCount());
        }

        [HttpGet("TotalPriceByDrinkCategory")]
        public ActionResult TotalPriceByDrinkCategory()
        {
            return Ok(_productService.TTotalPriceByDrinkCategory());
        }
        
        [HttpGet("TotalPriceBySaladCategory")]
        public ActionResult TotalPriceBySaladCategory()
        {
            return Ok(_productService.TTotalPriceBySaladCategory());
        }
        
        [HttpGet("ProductNameByMaxPrice")]
        public ActionResult ProductNameByMaxPrice()
        {
            return Ok(_productService.TProductNameByMaxPrice());
        }
        
        [HttpGet("ProductNameByMinPrice")]
        public ActionResult ProductNameByMinPrice()
        {
            return Ok(_productService.TProductNameByMinPrice());
        }
        
        [HttpGet("ProductAvgPriceByHamburger")]
        public ActionResult ProductAvgPriceByHamburger()
        {
            return Ok(_productService.TProductAvgPriceByHamburger());
        }

        [HttpGet("ProductCountByHamburger")]
        public ActionResult ProductCountByHamburger()
        {
            return Ok(_productService.TProductCountByCategoryNameHamburger());
        }

        [HttpGet("ProductCountByDrink")]
        public ActionResult ProductCountByDrink()
        {
            return Ok(_productService.TProductCountByCategoryNameDrink());
        }
        
        [HttpGet("ProductPriceAvg")]
        public ActionResult ProductPriceAvg()
        {
            return Ok(_productService.TProductPriceAvg());
        } 
        
        [HttpGet("ProductPriceBySteakBurger")]
        public ActionResult ProductPriceBySteakBurger()
        {
            return Ok(_productService.TProductPriceBySteakBurger());
        } 

        [HttpGet("ProductWithCategory")]
        public IActionResult ProductWithCategory()
        {
            var context = new SignalRContext();
            var values = context.Products.Include(x => x.Category).Select(y => new ResultProductWithCategory
            {
                Description = y.Description,
                ImageUrl = y.ImageUrl,
                Price = y.Price,
                ProductId = y.ProductId,
                ProductName = y.ProductName,
                ProductStatus = y.ProductStatus,
                CategoryName = y.Category.CategoryName
            });
            return Ok(values.ToList());
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);
            _productService.TAdd(value);
            return Ok("Ürün Bilgisi Eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.TGetByID(id);
            _productService.TDelete(value);
            return Ok("Ürün Bİlgisi Silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var value = _productService.TGetByID(id);
            return Ok(_mapper.Map<GetProductDto>(value)); 
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            _productService.TUpdate(value);
            return Ok("Ürün Bilgisi Güncellendi");
        }

		[HttpGet("GetLast9Products")]
        public IActionResult GetLast9Products()
        {
            var value = _productService.TGetLast9Products();
            return Ok(value);
        }

	}
}