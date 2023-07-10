using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductService.Domain.Entities.Products
{
    public sealed class Product:AggregateRoot
    {
        [Key]
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Price { get; private set; }
        public string CategoryName { get; private set; }

        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Product(Guid id, string name, string description, string price, string categoryName)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            CategoryName = categoryName;
        }
    }
}
