using System;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Carpool.DAL.Seeds;

public static class CarSeeds
{
    public static CarEntity Skoda_superb = new(
        Manufacturer: "Škoda",
        Type: "Superb",
        OwnerId: UserSeeds.Jan_novak.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-123A-4C54-86E6-BACF71896233"),
        DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
        PhotoUrl = "asdf",
        Capacity = 5
    };

    public static CarEntity Tesla_s = new(
        Manufacturer: "Tesla",
        Type: "S",
        OwnerId: UserSeeds.Karel_polacek.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-86E6-BACF71896233"),
        DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
        PhotoUrl = "sdfg",
        Capacity = 5
    };

    public static CarEntity Bmw_m3 = new(
        Manufacturer: "BMW",
        Type: "M3",
        OwnerId: UserSeeds.Alfred_rychly.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-789A-4C54-86E6-BACF71896233"),
        DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
        PhotoUrl = "dfgh",
        Capacity = 4
    };

    public static CarEntity Citroen_c2 = new(
        Manufacturer: "Citroen",
        Type: "C2",
        OwnerId: UserSeeds.Romana_modrava.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-147A-4C54-86E6-BACF71896233"),
        DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
        PhotoUrl = "fghj",
        Capacity = 4
    };

    public static CarEntity Honda_civic = new(
        Manufacturer: "Honda",
        Type: "Civic",
        OwnerId: UserSeeds.Pavel_zavodnik.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-258A-4C54-86E6-BACF71896233"),
        DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
        PhotoUrl = "ghjk",
        Capacity = 4
    };

    public static CarEntity Ford_focus = new(
        Manufacturer: "Ford",
        Type: "Focus",
        OwnerId: UserSeeds.Pavel_zavodnik.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-369A-4C54-86E6-BACF71896233"),
        DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
        PhotoUrl = "hjkl",
        Capacity = 5
    };

    public static CarEntity Renault_clio = new(
        Manufacturer: "Renault",
        Type: "Clio",
        OwnerId: UserSeeds.Anastazie_bohata.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-AAAA-4C54-86E6-BACF71896233"),
        DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
        PhotoUrl = "qwer",
        Capacity = 4
    };

    public static CarEntity Volkswagen_touareg = new(
        Manufacturer: "Volkswagen",
        Type: "Touareg",
        OwnerId: UserSeeds.Ota_toman.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-BBBA-4C54-86E6-BACF71896233"),
        DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
        PhotoUrl = "wert",
        Capacity = 5
    };

    public static CarEntity Toyota_celica = new(
        Manufacturer: "Toyota",
        Type: "Celica",
        OwnerId: UserSeeds.Pavel_zavodnik.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-CCCA-4C54-86E6-BACF71896233"),
        DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
        PhotoUrl = "ertz",
        Capacity = 4
    };

    public static CarEntity Suzuki_swift = new(
        Manufacturer: "Suzuki",
        Type: "Swift",
        OwnerId: UserSeeds.Romana_modrava.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-DDDA-4C54-86E6-BACF71896233"),
        DateOfFirstRegistration = DateTime.ParseExact("14.04.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture),
        PhotoUrl = "rtzu",
        Capacity = 4
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
            Skoda_superb,
            Tesla_s,
            Bmw_m3,
            Citroen_c2,
            Honda_civic,
            Ford_focus,
            Renault_clio,
            Volkswagen_touareg,
            Toyota_celica,
            Suzuki_swift
        );
    }
}