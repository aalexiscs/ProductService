namespace ProductService.Application.Constants
{
    public static class ErrorMessages
    {
        // Search errors
        public const string ProductNotFound = "Producto no encontrado";
        public const string ProductNotFoundWithId = "Producto con ID {0} no encontrado";

        // Server errors
        public const string InternalServerError = "Error interno del servidor. Por favor, intente nuevamente más tarde.";
        public const string ErrorCreatingProduct = "Error al crear el producto. Por favor, intente nuevamente.";
        public const string ErrorUpdatingProduct = "Error al actualizar el producto. Por favor, intente nuevamente.";
        public const string ErrorDeletingProduct = "Error al eliminar el producto. Por favor, intente nuevamente.";
        public const string ErrorGettingProducts = "Error al obtener los productos. Por favor, intente nuevamente.";
        public const string ErrorGettingProductById = "Error al obtener el producto con ID {0}. Por favor, intente nuevamente.";
    }
}