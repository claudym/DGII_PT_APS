using DGIIAPP.Infrastructure.Services;
using DGIIAPP.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace DGIIAPP.Tests;
public class ComprobanteFiscalServiceTests : IClassFixture<InMemoryDatabaseFixture> {
    private readonly InMemoryDatabaseFixture _fixture;

    public ComprobanteFiscalServiceTests(InMemoryDatabaseFixture fixture) {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetComprobantesFiscales_Should_Return_ListOfComprobanteFiscalDTO() {
        // Arrange
        var loggerMock = new Mock<ILogger<ComprobanteFiscalService>>();
        var service = new ComprobanteFiscalService(_fixture.DbContext, loggerMock.Object);
        _fixture.DbContext.ChangeTracker.Clear();
        _fixture.DbContext.ComprobantesFiscales.AddRange(new[]
        {
            new ComprobanteFiscal { NCF = "B0100000005", RncCedula = "00112345678", Monto = 100, Itbis18 = 18 },
            new ComprobanteFiscal { NCF = "B0100003256", RncCedula = "00298765432", Monto = 200, Itbis18 = 36 },
            new ComprobanteFiscal { NCF = "B0100013515", RncCedula = "00298765432", Monto = 300, Itbis18 = 54 }
        });
        await _fixture.DbContext.SaveChangesAsync();

        // Act
        var result = await service.GetComprobantesFiscales();

        // Assert
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Count());
    }

    [Fact]
    public async Task GetTotalITBIS_Should_Return_TotalITBISDTO_ForExistingRncCedula() {
        // Arrange0
        var loggerMock = new Mock<ILogger<ComprobanteFiscalService>>();
        var service = new ComprobanteFiscalService(_fixture.DbContext, loggerMock.Object);
        _fixture.DbContext.ChangeTracker.Clear();
        _fixture.DbContext.Contribuyentes.AddRange(new[]
        {
            new Contribuyente { RncCedula = "00112345678", Nombre = "María Fernández", Tipo = "Persona Juridica", Estatus = "Activo" },
            new Contribuyente { RncCedula = "00298765432", Nombre = "Alejandro Medina", Tipo = "Persona Fisica", Estatus = "Inactivo" },
        });
        _fixture.DbContext.ComprobantesFiscales.AddRange(new[]
        {
            new ComprobanteFiscal { NCF = "B0100000005", RncCedula = "00112345678", Monto = 100, Itbis18 = 18 },
            new ComprobanteFiscal { NCF = "B0100003256", RncCedula = "00298765432", Monto = 200, Itbis18 = 36 },
            new ComprobanteFiscal { NCF = "B0100013515", RncCedula = "00298765432", Monto = 300, Itbis18 = 54 }
        });
        await _fixture.DbContext.SaveChangesAsync();

        // Act
        var result = await service.GetTotalITBIS("00298765432");

        // Assert
        Assert.True(result.Success);
        Assert.Equal("00298765432", result.Data?.RncCedula);
        Assert.Equal("Alejandro Medina", result.Data?.NombreContribuyente);
        Assert.Equal(90, result.Data?.TotalITBIS18);
    }

    [Fact]
    public async Task GetTotalITBIS_Should_ThrowNotFoundException_ForNonExistingRncCedula() {
        // Arrange
        var loggerMock = new Mock<ILogger<ComprobanteFiscalService>>();
        var service = new ComprobanteFiscalService(_fixture.DbContext, loggerMock.Object);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => service.GetTotalITBIS("40263215487"));
    }

    [Fact]
    public async Task GetTotalITBISList_Should_Return_ListOfTotalITBISDTO()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ComprobanteFiscalService>>();
        var service = new ComprobanteFiscalService(_fixture.DbContext, loggerMock.Object);
        _fixture.DbContext.ChangeTracker.Clear();
        _fixture.DbContext.Contribuyentes.AddRange(new[]
        {
            new Contribuyente { RncCedula = "00112345678", Nombre = "María Fernández", Tipo = "Persona Juridica", Estatus = "Activo" },
            new Contribuyente { RncCedula = "00298765432", Nombre = "Alejandro Medina", Tipo = "Persona Fisica", Estatus = "Inactivo" },
        });
        _fixture.DbContext.ComprobantesFiscales.AddRange(new[]
        {
            new ComprobanteFiscal { NCF = "B0100000005", RncCedula = "00112345678", Monto = 100, Itbis18 = 18 },
            new ComprobanteFiscal { NCF = "B0100003256", RncCedula = "00298765432", Monto = 200, Itbis18 = 36 },
            new ComprobanteFiscal { NCF = "B0100013515", RncCedula = "00298765432", Monto = 300, Itbis18 = 54 }
        });

        // Act
        var result = await service.GetTotalITBISList();

        // Assert
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.Count());

        _fixture.Dispose();
    }
}
