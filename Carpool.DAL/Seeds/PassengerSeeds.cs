using System;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds;
public static class PassengerSeeds
{
    public static PassengerEntity Passenger_1 = new(
        UserId: UserSeeds.Karel_polacek.Id,
        RideId: RideSeeds.Krumlov_praha.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AAAA-BACF71896233")
    };

    public static PassengerEntity Passenger_2 = new(
        UserId: UserSeeds.Hubert_forman.Id,
        RideId: RideSeeds.Krumlov_praha.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AAAB-BACF71896233")
    };

    public static PassengerEntity Passenger_3 = new(
        UserId: UserSeeds.Herman_stuzka.Id,
        RideId: RideSeeds.Krumlov_praha.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AAAC-BACF71896233")
    };

    public static PassengerEntity Passenger_4 = new(
        UserId: UserSeeds.Anastazie_bohata.Id,
        RideId: RideSeeds.Lhota_bucovice.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AAAD-BACF71896233")
    };

    public static PassengerEntity Passenger_5 = new(
        UserId: UserSeeds.Alfred_rychly.Id,
        RideId: RideSeeds.Lhota_bucovice.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AAAE-BACF71896233")
    };

    public static PassengerEntity Passenger_6 = new(
        UserId: UserSeeds.Kvetoslava_svizna.Id,
        RideId: RideSeeds.Brno_sternberk.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AAAF-BACF71896233")
    };

    public static PassengerEntity Passenger_7 = new(
        UserId: UserSeeds.Hubert_forman.Id,
        RideId: RideSeeds.Zlin_Olomouc.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AABA-BACF71896233")
    };

    public static PassengerEntity Passenger_8 = new(
        UserId: UserSeeds.Karel_polacek.Id,
        RideId: RideSeeds.Zlin_Olomouc.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AABB-BACF71896233")
    };

    public static PassengerEntity Passenger_9 = new(
        UserId: UserSeeds.Romana_modrava.Id,
        RideId: RideSeeds.Zlin_Olomouc.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AABC-BACF71896233")
    };

    public static PassengerEntity Passenger_10 = new(
        UserId: UserSeeds.Anastazie_bohata.Id,
        RideId: RideSeeds.Zlin_Olomouc.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AABD-BACF71896233")
    };

    public static PassengerEntity Passenger_11 = new(
        UserId: UserSeeds.Alfred_rychly.Id,
        RideId: RideSeeds.Vary_prerov.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AABE-BACF71896233")
    };

    public static PassengerEntity Passenger_12 = new(
        UserId: UserSeeds.Herman_stuzka.Id,
        RideId: RideSeeds.Vary_prerov.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AABF-BACF71896233")
    };

    public static PassengerEntity Passenger_13 = new(
        UserId: UserSeeds.Anastazie_bohata.Id,
        RideId: RideSeeds.Plzen_tabor.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AACA-BACF71896233")
    };

    public static PassengerEntity Passenger_14 = new(
        UserId: UserSeeds.Kvetoslava_svizna.Id,
        RideId: RideSeeds.Plzen_tabor.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AACB-BACF71896233")
    };

    public static PassengerEntity Passenger_15 = new(
        UserId: UserSeeds.Hubert_forman.Id,
        RideId: RideSeeds.Pardubice_jihlava.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AACC-BACF71896233")
    };

    public static PassengerEntity Passenger_16 = new(
        UserId: UserSeeds.Jan_novak.Id,
        RideId: RideSeeds.Olomouc_trinec.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AACD-BACF71896233")
    };

    public static PassengerEntity Passenger_17 = new(
        UserId: UserSeeds.Karel_polacek.Id,
        RideId: RideSeeds.Olomouc_trinec.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AACE-BACF71896233")
    };

    public static PassengerEntity Passenger_18 = new(
        UserId: UserSeeds.Hubert_forman.Id,
        RideId: RideSeeds.Olomouc_trinec.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AACF-BACF71896233")
    };

    public static PassengerEntity Passenger_19 = new(
        UserId: UserSeeds.Kvetoslava_svizna.Id,
        RideId: RideSeeds.Liberec_usti.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AADA-BACF71896233")
    };

    public static PassengerEntity Passenger_20 = new(
        UserId: UserSeeds.Alfred_rychly.Id,
        RideId: RideSeeds.Liberec_usti.Id
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-456A-4C54-AADB-BACF71896233")
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PassengerEntity>().HasData(
            Passenger_1,
            Passenger_2,
            Passenger_3,
            Passenger_4,
            Passenger_5,
            Passenger_6,
            Passenger_7,
            Passenger_8,
            Passenger_9,
            Passenger_10,
            Passenger_11,
            Passenger_12,
            Passenger_13,
            Passenger_14,
            Passenger_15,
            Passenger_16,
            Passenger_17,
            Passenger_18,
            Passenger_19,
            Passenger_20
        );
    }
}

