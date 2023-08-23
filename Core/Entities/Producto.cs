namespace Core.Entities;

public class Producto
{
    public int Id { get; set;}
    public string CodInterno { get; set;}
    public string Nombre { get; set;}
    public int StockMin { get; set;}
    public int StockMax { get; set;}
    public int Stock { get; set;}
    public Double ValorVenta { get; set;}
    public ICollection<Persona> Personas{ get; set;} = new HashSet<Persona>();

    public ICollection<ProductoPersona> ProductoPersonas{ get; set;}
}
