namespace ProductService.Domain.Constants
{
    public static class ErrorMessages
    {
        // Validation errors
        public const string ProductNameRequired = "El nombre del producto es requerido";
        public const string ProductNameTooLong = "El nombre del producto no puede tener más de 120 caracteres";
        public const string ProductPriceInvalid = "El precio del producto debe ser mayor a 0";
        public const string ProductStockInvalid = "El stock del producto no puede ser negativo";
        public const string ProductDescriptionTooLong = "La descripción del producto no puede tener más de 250 caracteres";

        // Operating errors
        public const string InsufficientStock = "No hay suficiente stock para el producto con ID {0}";
        public const string InvalidQuantity = "La cantidad debe ser mayor a 0";
    }
}