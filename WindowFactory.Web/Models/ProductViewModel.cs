using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WindowFactory.Domain.Models;
using WindowFactory.Web.Models.Mapping;

namespace WindowFactory.Web.Models
{
    /// <summary>
    /// Товар
    /// </summary>
    public class ProductViewModel : IHaveCustomMappings
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Количество в наличии / на складе
        /// </summary>
        public int InStock { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>("Product");

            configuration.CreateMap<ProductViewModel, Product>("Product");
        }
    }

}
