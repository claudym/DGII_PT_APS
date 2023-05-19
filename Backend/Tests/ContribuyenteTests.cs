using DGIIAPP.Infrastructure.Services;
using DGIIAPP.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace DGIIAPP.Tests;

public class ContribuyenteServiceTests : IClassFixture<InMemoryDatabaseFixture> {
    private readonly InMemoryDatabaseFixture _fixture;

    public ContribuyenteServiceTests(InMemoryDatabaseFixture fixture) {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetContribuyente_Should_Return_ListOfContribuyenteDTO() {
        // Arrange
        var loggerMock = new Mock<ILogger<ContribuyenteService>>();
        var service = new ContribuyenteService(_fixture.DbContext, loggerMock.Object);

        // Add some test data to the in-memory database
        _fixture.DbContext.Contribuyentes.AddRange(new[] {
            new Contribuyente { RncCedula = "00112345678", Nombre = "María Fernández", Tipo = "Persona Juridica", Estatus = "Activo" },
            new Contribuyente { RncCedula = "00298765432", Nombre = "Alejandro Medina", Tipo = "Persona Fisica", Estatus = "Inactivo" },
            new Contribuyente { RncCedula = "40256789014", Nombre = "Sofia Herrera", Tipo = "Persona Juridica", Estatus = "Activo" }
        });
        await _fixture.DbContext.SaveChangesAsync();

        // Act
        var result = await service.GetContribuyente();
        
        // Assert
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Count());

        _fixture.Dispose();
    }
}
