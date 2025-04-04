using Ecommerce.Application.Categories.Commands;
using Ecommerce.Application.Categories.DTOs;
using Ecommerce.Application.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

public class CategoriesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories()
    {
        var categories = await Mediator.Send(new ListCategories.Query());
        return HandleResult(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
    {
        var category = await Mediator.Send(new GetCategoryById.Query { Id = id });
        return HandleResult(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryWithoutChildDto>> CreateCategory(
        CreateCategoryDto category
    )
    {
        var result = await Mediator.Send(new CreateCategory.Command { CategoryDto = category });
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryWithoutChildDto>> EditCategory(
        int id,
        EditCategoryDto category
    )
    {
        var result = await Mediator.Send(
            new EditCategory.Command { Id = id, CategoryDto = category }
        );
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> DeleteCategory(int id)
    {
        var result = await Mediator.Send(new DeleteCategory.Command { Id = id });
        return HandleResult(result);
    }

    [HttpGet("subcategories/{id}")]
    public async Task<ActionResult<SubcategoryDto>> GetSubcategoryById(int id)
    {
        var subcategory = await Mediator.Send(new GetSubcategoryById.Query { Id = id });
        return HandleResult(subcategory);
    }

    [HttpPost("subcategories")]
    public async Task<ActionResult<SubcategoryDto>> CreateSubcategory(
        CreateSubcategoryDto subcategory
    )
    {
        var result = await Mediator.Send(
            new CreateSubcategory.Command { SubcategoryDto = subcategory }
        );
        return HandleResult(result);
    }

    [HttpPut("subcategories/{id}")]
    public async Task<ActionResult<SubcategoryDto>> EditSubcategory(
        int id,
        EditSubcategoryDto subcategory
    )
    {
        var result = await Mediator.Send(
            new EditSubcategory.Command { Id = id, SubcategoryDto = subcategory }
        );
        return HandleResult(result);
    }

    [HttpDelete("subcategories/{id}")]
    public async Task<ActionResult<Unit>> DeleteSubcategory(int id)
    {
        var result = await Mediator.Send(new DeleteSubcategory.Command { Id = id });
        return HandleResult(result);
    }
}
