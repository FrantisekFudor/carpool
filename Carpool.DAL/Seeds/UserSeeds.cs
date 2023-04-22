using System;
using Carpool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Seeds;

public static class UserSeeds
{
    public static UserEntity Jan_novak = new(
        Name: "Jan",
        Surname: "Novak"
        )
    {
        Id = Guid.Parse(input: "9A0DCDBC-664A-4C54-86E6-BACF71896233"),
        PhotoUrl = "asdf"
    };

    public static UserEntity Karel_polacek = new(
        Name: "Karel",
        Surname: "Polacek"
        )
    {
        Id = Guid.Parse(input: "544DF3F2-5105-4CF8-8826-F5ED03CB3E80"),
        PhotoUrl = "ghjk"
    };

    public static UserEntity Ota_toman = new(
        Name: "Ota",
        Surname: "Toman"
        )
    {
        Id = Guid.Parse(input: "ABCDF3F2-5105-4CF8-8826-F5ED03CB3E80"),
        PhotoUrl = "qwer"
    };

    public static UserEntity Pavel_zavodnik = new(
        Name: "Pavel",
        Surname: "Zavodnik"
        )
    {
        Id = Guid.Parse(input: "123DF3F2-5105-4CF8-8826-F5ED03CB3E80"),
        PhotoUrl = "tzui"
    };

    public static UserEntity Alfred_rychly = new(
        Name: "Alfred",
        Surname: "Rychly"
        )
    {
        Id = Guid.Parse(input: "456DF3F2-5105-4CF8-8826-F5ED03CB3E80"),
        PhotoUrl = "yxcv"
    };

    public static UserEntity Hubert_forman = new(
        Name: "Hubert",
        Surname: "Forman"
        )
    {
        Id = Guid.Parse(input: "789DF3F2-5105-4CF8-8826-F5ED03CB3E80"),
        PhotoUrl = "bnmy"
    };

    public static UserEntity Herman_stuzka = new(
        Name: "Herman",
        Surname: "Stuzka"
        )
    {
        Id = Guid.Parse(input: "147DF3F2-5105-4CF8-8826-F5ED03CB3E80"),
        PhotoUrl = "poiu"
    };

    public static UserEntity Kvetoslava_svizna = new(
        Name: "Kvetoslava",
        Surname: "Svizna"
        )
    {
        Id = Guid.Parse(input: "258DF3F2-5105-4CF8-8826-F5ED03CB3E80"),
        PhotoUrl = "lkjh"
    };

    public static UserEntity Romana_modrava = new(
        Name: "Romana",
        Surname: "Modrava"
        )
    {
        Id = Guid.Parse(input: "369DF3F2-5105-4CF8-8826-F5ED03CB3E80"),
        PhotoUrl = "ztre"
    };

    public static UserEntity Anastazie_bohata = new(
        Name: "Anastazie",
        Surname: "Bohata"
        )
    {
        Id = Guid.Parse(input: "AAADF3F2-5105-4CF8-8826-F5ED03CB3E80"),
        PhotoUrl = "gfds"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            Jan_novak,
            Karel_polacek,
            Ota_toman,
            Pavel_zavodnik,
            Alfred_rychly,
            Hubert_forman,
            Herman_stuzka,
            Kvetoslava_svizna,
            Romana_modrava,
            Anastazie_bohata
        );
    }
}