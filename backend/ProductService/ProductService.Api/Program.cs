using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ProductService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ProductService.Infrastructure.Profiles;
using ProductService.Domain.Entities.Products;
using ProductService.Infrastructure.Repositories;
using ProductService.Infrastructure.Dtos;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Environment.IsDevelopment() ? builder.Configuration.GetConnectionString("DefaultConnection")
    : Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductServiceContext>(builder => builder.UseNpgsql(connectionString));

builder.Services.AddAutoMapper(typeof(ProductProfile));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(typeof(ProductProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/customers", async (IMapper mapper, IProductRepository productRepository, ProductDto productDto) =>
{
    var productModel = mapper.Map<Product>(productDto);

    await productRepository.Create(productModel);

    return Results.Created($"api/customers/{productModel.Id}", productModel);
});

app.MapGet("/api/customers/{id:guid}", async (IMapper mapper, Guid id, IProductRepository productRepository) =>
{
    var createdProduct = await productRepository.GetById(id);

    return createdProduct is null ? Results.NotFound(id) : Results.Ok(mapper.Map<ProductDto>(createdProduct));
});

app.MapGet("/api/customers", async (IProductRepository productRepository, IMapper mapper) =>
{
    var response = new ResponseDto();
    try
    {
        var products = await productRepository.GetAll();
        response.Result = products;
    }
    catch(Exception ex)
    {
        response.IsSuccess = false;
        response.Errors = new List<string>() { ex.ToString() };
    }
    return response;
});

app.MapPut("/api/customers/{id:guid}", async (IMapper mapper, IProductRepository productRepository, Guid id, ProductDto productDto) =>
{
    var updatedProduct = await productRepository.GetById(id);

    mapper.Map(productDto, updatedProduct);

    var entry = await productRepository.Update(updatedProduct);

    return updatedProduct is null ? Results.NotFound() : Results.Ok(entry);
});

app.MapDelete("/api/customers/{id:guid}", async (Guid id, IProductRepository productRepository) =>
{
    var updatedProduct = await productRepository.Delete(id);
    return updatedProduct is null ? Results.NotFound() : Results.Ok(updatedProduct);
});

app.Run();
