﻿using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels
{
    /// <summary>
    /// // Calabonga: update summary (2019-05-26 01:05 CategoriesController)
    /// </summary>
    public class CategoryViewModel : ViewModelBase
    {
        /// <summary>
        /// Name of the catalog
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description for current catalog
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Total count items in the category
        /// </summary>
        public int ProductsCount { get; set; }
    }
}