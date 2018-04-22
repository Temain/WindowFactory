using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WindowFactory.Domain.Models;
using WindowFactory.Web.Models.Mapping;

namespace WindowFactory.Web.Models
{
    /// <summary>
    /// Продажа
    /// </summary>
    public class SaleViewModel : IHaveCustomMappings
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int SaleId { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductCost { get; set; }

        /// <summary>
        /// Количество товаров
        /// </summary>
        private int _numberOfProducts;
        public int NumberOfProducts
        {
            get
            {
                return _numberOfProducts == 0 ? 1 : _numberOfProducts;
            }
            set
            {
                _numberOfProducts = value;
            }
        }

        /// <summary>
        /// Общая стоимость 
        /// </summary>
        public decimal TotalCost { get; set; }

        public decimal TotalCostView { get; set; }

        /// <summary>
        /// Клиент
        /// </summary>
        public int ClientId { get; set; }

        public string ClientShortName { get; set; }

        public string ClientFullName { get; set; }

        public IEnumerable<ClientViewModel> Clients { get; set; } 

        /// <summary>
        /// Сотрудник / продавец
        /// </summary>
        public int EmployeeId { get; set; }

        public string EmployeeShortName { get; set; }

        public string EmployeeFullName { get; set; }

        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        /// <summary>
        /// Дата продажи
        /// </summary>
        public DateTime? SaleDate { get; set; }


        public int? WindowTypeId { get; set; }
        public string WindowTypeName { get; set; }

        public int? NumberOfFlaps { get; set; }

        public int? WindowProfileId { get; set; }
        public string WindowProfileName { get; set; }

        public int? WindowColorId { get; set; }
        public string WindowColorName { get; set; }

        public int? WindowGlazingId { get; set; }
        public string WindowGlazingName { get; set; }

        public int? WindowGlassId { get; set; }
        public string WindowGlassName { get; set; }

        public int? WindowOpeningLimiterId { get; set; }
        public string WindowOpeningLimiterName { get; set; }

        public bool Microvolving { get; set; }
        public bool MosquitoNet { get; set; }
        public bool WindowSill { get; set; }
        public bool Drainage { get; set; }

        /// <summary>
        /// Ширина каждого окна
        /// </summary>
        public int FirstWidth { get; set; }
        public int SecondWidth { get; set; }
        public int ThirdWidth { get; set; }

        /// <summary>
        /// Высота каждого окна
        /// </summary>
        public int FirstHeight { get; set; }
        public int SecondHeight { get; set; }

        /// <summary>
        /// Установка окон
        /// </summary>
        public bool WindowInstallation { get; set; }

        /// <summary>
        /// Отделка откосов
        /// </summary>
        public bool SlopeFinishing { get; set; }

        /// <summary>
        /// Тип дома
        /// </summary>
        public int? TypeOfHouseId { get; set; }
        public string TypeOfHouseName { get; set; }

        /// <summary>
        /// Комплектация
        /// </summary>
        public string Complectation { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Sale, SaleViewModel>("Sale")
                .ForMember(m => m.EmployeeShortName, opt => opt.MapFrom(s => s.Employee.Person.ShortName))
                .ForMember(m => m.EmployeeFullName, opt => opt.MapFrom(s => s.Employee.Person.FullName))
                .ForMember(m => m.ClientShortName, opt => opt.MapFrom(s => s.Client.Person.ShortName))
                .ForMember(m => m.ClientFullName, opt => opt.MapFrom(s => s.Client.Person.FullName))
                .ForMember(m => m.WindowTypeName, opt => opt.MapFrom(s => s.WindowType.WindowTypeName))
                .ForMember(m => m.WindowProfileName, opt => opt.MapFrom(s => s.WindowProfile.WindowProfileName))
                .ForMember(m => m.WindowColorName, opt => opt.MapFrom(s => s.WindowColor.WindowColorFirst + "/" + s.WindowColor.WindowColorSecond))
                .ForMember(m => m.WindowGlazingName, opt => opt.MapFrom(s => s.WindowGlazing.WindowGlazingName))
                .ForMember(m => m.WindowGlassName, opt => opt.MapFrom(s => s.WindowGlass.WindowGlassName))
                .ForMember(m => m.WindowOpeningLimiterName, opt => opt.MapFrom(s => s.WindowOpeningLimiter.WindowOpeningLimiterName))
                .ForMember(m => m.TypeOfHouseName, opt => opt.MapFrom(s => s.TypeOfHouse.TypeOfHouseName));

            configuration.CreateMap<SaleViewModel, Sale>("Sale");
        }
    }

}
