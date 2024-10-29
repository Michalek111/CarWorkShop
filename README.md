# CarWorkShop

CarWorkShop to aplikacja ASP.NET MVC stworzona, aby zarządzać warsztatami samochodowymi. Użytkownicy mogą przeglądać dostępne warsztaty, dodawać własne, a także zarządzać usługami oferowanymi przez poszczególne warsztaty, jak np. wymiana opon, wymiana oleju, czy czyszczenie samochodu.

## Wymagania
- .NET SDK 6.0 lub wyższy
- SQL Server (lub kompatybilna baza danych)
- Visual Studio 2022

## Instalacja
1. Sklonuj repozytorium:
   git clone https://github.com/Michalek111/CarWorkShop.git

2. Przejdź do katalogu projektu:
   cd CarWorkShop

3. Przygotuj bazę danych i zaktualizuj connection string w pliku `appsettings.json`.
   
5. Zainstaluj zależności:
   dotnet restore

6. Uruchom migracje do bazy danych:
   dotnet ef database update

7. Uruchom aplikację:
   dotnet run


## Funkcje
- **Przeglądanie warsztatów**: Możliwość przeglądania warsztatów samochodowych i oferowanych przez nie usług.
- **Dodawanie warsztatów**: Użytkownicy mogą rejestrować własne warsztaty i zarządzać informacjami o nich.
- **Zarządzanie usługami**: Możliwość dodawania i zarządzania usługami w warsztatach, np. wymiana oleju, czyszczenie samochodu itp.
- **Role użytkowników**: Warsztaty mogą być dodawane tylko przez użytkowników z rolą `Owner`, a usługi mogą być zarządzane przez użytkowników będących właścicielami warsztatu lub posiadających rolę `Moderator`.

## Użyte technologie
- **ASP.NET Core MVC** - Framework do tworzenia aplikacji webowych.
- **Entity Framework Core** - ORM do komunikacji z bazą danych.
- **AutoMapper** - Narzędzie do mapowania obiektów.
- **FluentValidation** - Narzędzie do walidacji danych wejściowych.

## Konfiguracja użytkowników i ról
- Warsztaty mogą być tworzone przez użytkowników posiadających rolę `Owner`. Aby przypisać rolę użytkownikowi, należy zaktualizować tabelę `AspNetUserRoles`.
- Usługi w warsztatach mogą być dodawane lub edytowane przez właściciela warsztatu lub użytkowników z rolą `Moderator`.



