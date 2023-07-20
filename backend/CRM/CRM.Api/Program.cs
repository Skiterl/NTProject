using AutoMapper;
using CRM.Domain.Aggregates.UserAggregate;
using CRM.Infrastructure.Contexts;
using CRM.Infrastructure.Dtos;
using CRM.Infrastructure.Profiles;
using CRM.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Environment.IsDevelopment() ? builder.Configuration.GetConnectionString("DefaultConnection")
    : Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CRMContext>(builder => builder.UseNpgsql(connectionString));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddAutoMapper(typeof(CustomerProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("/api/customers", async (IMapper mapper, ICustomerRepository customerRepository, CustomerCreateDto customerCreateDto) =>
{
    var customerModel = mapper.Map<Customer>(customerCreateDto);

    await customerRepository.Create(customerModel);

    var customerReadDto = mapper.Map<CustomerReadDto>(customerModel);

    return Results.Created($"api/customers/{customerReadDto.Id}", customerReadDto);
});

app.MapGet("/api/customers/{id:guid}", async (IMapper mapper, Guid id, ICustomerRepository customerRepository) =>
{
    var createdCustomer = await customerRepository.GetById(id);

    return createdCustomer is null ? Results.NotFound(id) : Results.Ok(mapper.Map<CustomerReadDto>(createdCustomer));
});

app.MapGet("/api/customers", async (ICustomerRepository customerRepository, IMapper mapper) =>
{
    var customers = await customerRepository.GetAll();
    return Results.Ok(mapper.Map<IEnumerable<CustomerReadDto>>(customers));
});

app.MapPut("/api/customers/{id:guid}", async (IMapper mapper, ICustomerRepository customerRepository, Guid id, CustomerUpdateDto customerUpdateDto) =>
{
    var updatedCustomer = await customerRepository.GetById(id);

    mapper.Map(customerUpdateDto, updatedCustomer);

    var entry = await customerRepository.Update(updatedCustomer);

    return updatedCustomer is null ? Results.NotFound() : Results.Ok(entry);
});

app.MapDelete("/api/customers/{id:guid}", async (Guid id, ICustomerRepository customerRepository) =>
{
    var updatedCustomer = await customerRepository.Delete(id);
    return updatedCustomer is null ? Results.NotFound() : Results.Ok(updatedCustomer);
});



app.Run();