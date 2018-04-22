using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WindowFactory.Domain.Models
{
    /// <summary>
    /// Продажа
    /// </summary>
    [Table("Sale", Schema = "dbo")]
    public class Sale
    {
        public int SaleId { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        //public int ProductId { get; set; }
        //public Product Product { get; set; }

        public int? WindowTypeId { get; set; }
        public WindowType WindowType { get; set; }

        public int? NumberOfFlaps { get; set; }

        public int? WindowProfileId { get; set; }
        public WindowProfile WindowProfile { get; set; }

        public int? WindowColorId { get; set; }
        public WindowColor WindowColor { get; set; }

        public int? WindowGlazingId { get; set; }
        public WindowGlazing WindowGlazing { get; set; }

        public int? WindowGlassId { get; set; }
        public WindowGlass WindowGlass { get; set; }

        public int? WindowOpeningLimiterId { get; set; }
        public WindowOpeningLimiter WindowOpeningLimiter { get; set; }


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
        public TypeOfHouse TypeOfHouse { get; set; }

        /// <summary>
        /// Комплектация
        /// </summary>
        public string Complectation { get; set; }

        /// <summary>
        /// Количество товаров
        /// </summary>
        public int NumberOfProducts { get; set; }

        /// <summary>
        /// Стоимость одного окна
        /// </summary>
        public decimal? Cost { get; set; }

        /// <summary>
        /// Общая стоимость 
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        /// Клиент
        /// </summary>
        public int ClientId { get; set; }
        public Client Client { get; set; }

        /// <summary>
        /// Сотрудник / продавец
        /// </summary>
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        /// <summary>
        /// Дата продажи
        /// </summary>
        public DateTime? SaleDate { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Дата удаления записи
        /// </summary>
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
    }

}
