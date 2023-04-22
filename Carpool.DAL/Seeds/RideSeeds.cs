using System;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Carpool.DAL.Seeds;

public static class RideSeeds
{
    public static RideEntity Brno_praha = new(
        Start: "Brno",
        Destination: "Praha",
        DriverId: UserSeeds.Jan_novak.Id,
        CarId: CarSeeds.Skoda_superb.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-AAAF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(3,15,0)
    };

    public static RideEntity Ostrava_cb = new(
         Start: "Ostrava",
         Destination: "Ceske Budejovice",
         DriverId: UserSeeds.Jan_novak.Id,
         CarId: CarSeeds.Skoda_superb.Id
         )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-AABF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(5, 30, 0)
    };

    public static RideEntity Olomouc_trinec = new(
        Start: "Olomouc",
        Destination: "Trinec",
        DriverId: UserSeeds.Romana_modrava.Id,
        CarId: CarSeeds.Suzuki_swift.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-AACF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(1, 5, 0)
    };

    public static RideEntity Pardubice_jihlava = new(
        Start: "Pardubice",
        Destination: "Jihlava",
        DriverId: UserSeeds.Romana_modrava.Id,
        CarId: CarSeeds.Suzuki_swift.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-AADF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(1, 30, 0)
    };

    public static RideEntity Plzen_tabor = new(
        Start: "Plzen",
        Destination: "Tabor",
        DriverId: UserSeeds.Romana_modrava.Id,
        CarId: CarSeeds.Citroen_c2.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-AAEF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(1, 20, 0)
    };

    public static RideEntity Teplice_chomutov = new(
        Start: "Teplice",
        Destination: "Chomutov",
        DriverId: UserSeeds.Pavel_zavodnik.Id,
        CarId: CarSeeds.Honda_civic.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-AAFF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(0, 40, 0)
    };

    public static RideEntity Vary_prerov = new(
        Start: "Karlovy Vary",
        Destination: "Prerov",
        DriverId: UserSeeds.Pavel_zavodnik.Id,
        CarId: CarSeeds.Honda_civic.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-ABAF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(5, 20, 0)
    };

    public static RideEntity Zlin_Olomouc = new(
        Start: "Zlin",
        Destination: "Olomouc",
        DriverId: UserSeeds.Pavel_zavodnik.Id,
        CarId: CarSeeds.Toyota_celica.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-ABBF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(1, 0, 0)
    };

    public static RideEntity Prostejov_brod = new(
        Start: "Prostejov",
        Destination: "Uhersky Brod",
        DriverId: UserSeeds.Pavel_zavodnik.Id,
        CarId: CarSeeds.Honda_civic.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-ABCF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(1, 15, 0)
    };

    public static RideEntity Lhota_bucovice = new(
        Start: "Hroznova Lhota",
        Destination: "Bucovice",
        DriverId: UserSeeds.Ota_toman.Id,
        CarId: CarSeeds.Volkswagen_touareg.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-ABDF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(3, 30, 0)
    };

    public static RideEntity Brno_sternberk = new(
        Start: "Brno",
        Destination: "Sternberk",
        DriverId: UserSeeds.Ota_toman.Id,
        CarId: CarSeeds.Volkswagen_touareg.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-ABEF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(2, 20, 0)
    };

    public static RideEntity Otrokovice_brno = new(
        Start: "Otrokovice",
        Destination: "Brno",
        DriverId: UserSeeds.Ota_toman.Id,
        CarId: CarSeeds.Volkswagen_touareg.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-ABFF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(0, 50, 0)
    };

    public static RideEntity Liberec_usti = new(
        Start: "Liberec",
        Destination: "Usti nad Labem",
        DriverId: UserSeeds.Pavel_zavodnik.Id,
        CarId: CarSeeds.Honda_civic.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-ACAF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(0, 45, 0)
    };

    public static RideEntity Hradec_znojmo = new(
        Start: "Hradec Kralove",
        Destination: "Znojmo",
        DriverId: UserSeeds.Pavel_zavodnik.Id,
        CarId: CarSeeds.Toyota_celica.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-ACBF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(3, 40, 0)
    };

    public static RideEntity Krumlov_praha = new(
        Start: "Cesky Krumlov",
        Destination: "Praha",
        DriverId: UserSeeds.Ota_toman.Id,
        CarId: CarSeeds.Volkswagen_touareg.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-ACCF-86E6-BACF71896233"),
        DepartureTime = DateTime.ParseExact("03.05.2022 08:45", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
        Duration = new TimeSpan(2, 10, 0)
    };




    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RideEntity>().HasData(
            Brno_praha,
            Ostrava_cb,
            Olomouc_trinec,
            Pardubice_jihlava,
            Plzen_tabor,
            Teplice_chomutov,
            Vary_prerov,
            Zlin_Olomouc,
            Prostejov_brod,
            Lhota_bucovice,
            Brno_sternberk,
            Otrokovice_brno,
            Liberec_usti,
            Hradec_znojmo,
            Krumlov_praha
        );
    }
}

