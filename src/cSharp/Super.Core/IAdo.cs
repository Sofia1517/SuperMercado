namespace Super.Core;

public interface IAdo
{
    void AltaCategoria(Categoria categoria);
    List<Categoria> ObtenerCategorias();
    void AltaProducto(Producto producto);
    List<Producto> ObtenerProductos();
    Producto? ObtenerProducto(short id);
    void AltaCajero(Cajero cajero);
    Cajero? CajeroPorPass(uint dni, string pass);
}