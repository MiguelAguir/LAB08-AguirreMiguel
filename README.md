# LAB08-AguirreMiguel

Laboratorio educativo de consultas LINQ sobre MySQL expuestas como API REST con ASP.NET Core.

## Descripción

API REST que expone 15 endpoints demostrando diferentes operaciones LINQ (Where, Sum, Average, GroupBy, SelectMany, joins, proyecciones) sobre un modelo relacional de clientes, productos, pedidos y detalles de pedidos. Implementa el patrón Repository para acceso a datos con Entity Framework Core y MySQL.

## Tecnologías

- **.NET 9** / ASP.NET Core Web API
- **Entity Framework Core 9** con MySQL (Pomelo + MySql.EntityFrameworkCore)
- **LINQ** (Where, Sum, Average, GroupBy, SelectMany, joins)
- **Repository Pattern**
- **Swagger / OpenAPI**

## Estructura del proyecto

```
LAB08-AguirreMiguel/
  src/
    Core/              -- Entidades del dominio (Client, Product, Order, OrderDetail)
    Application/       -- DTOs para proyecciones personalizadas
    Infrastructure/    -- DbContext, repositorios (IRepository, Repository)
    Presentation/      -- Controlador (LinqController con 15 endpoints)
```

### Modelo de datos

| Entidad      | Descripción                              |
|--------------|------------------------------------------|
| Client       | Cliente con nombre, email                |
| Product      | Producto con nombre, descripción, precio |
| Order        | Pedido asociado a un cliente             |
| OrderDetail  | Detalle del pedido (producto + cantidad) |

## Instalación

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/MiguelAguir/LAB08-AguirreMiguel.git
   cd LAB08-AguirreMiguel
   ```

2. Configurar la cadena de conexión a MySQL en `appsettings.Development.json`.

3. Restaurar paquetes y ejecutar:
   ```bash
   dotnet restore
   dotnet run
   ```

## Endpoints

Todos los endpoints están en `GET /api/linq/*`:

| Ruta                          | Operación LINQ     | Descripción                                    |
|-------------------------------|--------------------|------------------------------------------------|
| /api/linq/clients?name=       | Where              | Buscar clientes por nombre                     |
| /api/linq/products?price=     | Where              | Productos con precio mayor al indicado         |
| /api/linq/order-details?orderId= | Where + Select  | Detalle de un pedido específico                |
| /api/linq/order-quantity?orderId= | Sum            | Cantidad total de productos en un pedido       |
| /api/linq/most-expensive-product | OrderByDescending | Producto más caro                             |
| /api/linq/orders-after-date?date= | Where           | Pedidos posteriores a una fecha                |
| /api/linq/average-product-price | Average          | Precio promedio de productos                   |
| /api/linq/products-without-description | Where      | Productos sin descripción                      |
| /api/linq/client-with-most-orders | GroupBy + Max | Cliente con más pedidos                       |
| /api/linq/all-orders-details | Select              | Todos los detalles de pedidos                  |
| /api/linq/products-by-client?clientId= | SelectMany | Productos comprados por un cliente            |
| /api/linq/clients-by-product?productId= | SelectMany | Clientes que compraron un producto            |
| /api/linq/clients-with-orders | Select + Include   | Clientes con sus pedidos (DTO)                 |
| /api/linq/order-with-details/{orderId} | Include + ThenInclude | Pedido con detalles y producto       |
| /api/linq/clients-total-products | Sum + GroupBy   | Total de productos por cliente                 |
| /api/linq/client-sales        | Sum + OrderBy      | Ventas totales por cliente                     |

## Estado del proyecto

Completado. Laboratorio educativo funcional con fines de demostración de consultas LINQ.
