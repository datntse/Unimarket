using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unimarket.API.Helper;
using Unimarket.API.Helpers;
using Unimarket.API.Services;
using Unimarket.Core.Constants;
using Unimarket.Core.Entities;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRole.Admin)]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IItemImageService _itemImageService;
        private readonly IMapper _mapper;
        private readonly IItemCategoryService _itemCategoryService;
        private readonly ICategoryService _categoryService;

        public ItemController( IItemService itemService, IItemImageService itemImageService,
            IItemCategoryService itemCategoryService, ICategoryService categoryService, IMapper mapper)
        {
            _itemService = itemService;
            _itemImageService = itemImageService;
            _mapper = mapper;
            _itemCategoryService = itemCategoryService;
            _categoryService = categoryService;
        }
		[HttpGet("get/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
		{
			try
			{
				var resultItem = await _itemService.FindAsync(id);
				var result = _mapper.Map<ItemDTO>(resultItem);
				var item = new ItemVM
				{
					// chõ này chưa map
					Id = result.Id,
					Name = result.Name,
					Description = result.Description,
					ImageUrl = result.ImageUrl,
                    ProductDetail = result.ProductDetail,
					Price = result.Price,
					Quantity = result.Quantity,
					Status = result.Status,
					CategoryName = _itemCategoryService.Get(cate => cate.ItemId.Equals(result.Id)).Include(cate => cate.Category)
					.Select(_ => _.Category.Name).ToList(),
					SubImageUrl = _itemImageService.Get(image => image.Item.Id.Equals(result.Id)).Select(item => item.ImageUrl).ToList(),
				};

				return Ok(item);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(
    [FromQuery] Helpers.DefaultSearch defaultSearch,
    [FromQuery] List<string> categoryNames,
    [FromQuery] float? minPrice,
    [FromQuery] float? maxPrice,
    [FromQuery] string? keyword) 
        {
            try
            {
                var resultListItem = _itemService.GetAll();

                // Filter by category names if provided
                if (categoryNames != null && categoryNames.Any())
                {
                    var itemIds = _itemCategoryService.Get(cate => categoryNames.Contains(cate.Category.Name))
                                                       .Select(cate => cate.ItemId)
                                                       .Distinct()
                                                       .ToList();

                    resultListItem = resultListItem.Where(item => itemIds.Contains(item.Id));
                }

                // Filter by price range
                if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
                {
                    return BadRequest("Min price cannot be greater than max price.");
                }

                if (minPrice.HasValue)
                {
                    resultListItem = resultListItem.Where(item => item.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    resultListItem = resultListItem.Where(item => item.Price <= maxPrice.Value);
                }

                // Filter by keyword in Name or Description
                if (!string.IsNullOrEmpty(keyword))
                {
                    string searchKeyword = keyword.ToLower(); // Convert keyword to lower case for case-insensitive search

                    resultListItem = resultListItem.Where(item =>
                        item.Name.ToLower().Contains(searchKeyword) ||
                        item.Description.ToLower().Contains(searchKeyword));
                }

                // Sort, paginate, and project to DTO
                var itemList = await resultListItem
                    .Sort(string.IsNullOrEmpty(defaultSearch.sortBy) ? "Id" : defaultSearch.sortBy, defaultSearch.isAscending)
                    .ToPageList(defaultSearch.currentPage, defaultSearch.perPage)
                    .AsNoTracking()
                    .ToListAsync();

                var result = _mapper.Map<List<ItemDTO>>(itemList);

                // Map to ViewModel including additional details
                var result2 = result.Select(_ => new ItemVM
                {
                    Id = _.Id,
                    Name = _.Name,
                    ProductDetail = _.ProductDetail,
                    Description = _.Description,
                    ImageUrl = _.ImageUrl,
                    Price = _.Price,
                    Quantity = _.Quantity,
                    Status = _.Status,
                    CategoryName = _itemCategoryService.Get(cate => cate.ItemId.Equals(_.Id))
                                                        .Include(cate => cate.Category)
                                                        .Select(cate => cate.Category.Name)
                                                        .ToList(),
                    SubImageUrl = _itemImageService.Get(image => image.Item.Id.Equals(_.Id))
                                                    .Select(item => item.ImageUrl)
                                                    .ToList(),
                });

                // Return result with pagination details
                return Ok(new { total = resultListItem.Count(), data = result2, currentPage = defaultSearch.currentPage });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        [HttpPost]
        public async Task<IActionResult> Create(ItemDTO item)
        {
            try
            {
               List<ItemCategory> itemCategoryList = new List<ItemCategory>();   
               List<ItemImage> itemImageList = new List<ItemImage>();

                var _itemAdd = _mapper.Map<Item>(item);
                _itemAdd.CreateAt = DateTime.Now;
                await _itemService.AddAsync(_itemAdd);
                await _itemService.SaveChangeAsync();

                foreach (var imageLink in item.SubImageUrl)
                {
                    ItemImage image = new ItemImage();
                    image.ImageUrl = imageLink;
                    image.Item = _itemAdd;
                    itemImageList.Add(image);
                }

                foreach (var categoryId in item.CategoryId)
                {
                    ItemCategory itemCategory = new ItemCategory();
                    itemCategory.ItemId = _itemAdd.Id;
                    itemCategory.CategoryId = Guid.Parse(categoryId);
                    itemCategoryList.Add(itemCategory);
                }

                await _itemImageService.AddRangce(itemImageList);
                await _itemCategoryService.AddRangce(itemCategoryList);

                await _itemCategoryService.SaveChangeAsync();
                await _itemImageService.SaveChangeAsync();
                return Ok("Add sucessfully");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut] 
        public async Task<IActionResult> Update(ItemDTO item)
        {
            var _item = _mapper.Map<Item>(item);
             _itemService.Update(_item);
            return Ok();
        }


    }
}
