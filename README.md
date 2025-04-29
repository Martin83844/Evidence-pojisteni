# Evidence pojištění

Tato aplikace byla vytvořena jako osobní projekt pro demonstraci dovedností ve vývoji webových aplikací s odděleným frontendem a backendem. Umožňuje správu údajů o pojištěncích a slouží jako tréninkový fullstack projekt.

---

## Funkce

- Přehledné zobrazení seznamu pojištěnců, pojištění a pojistných událostí
- Přidávání nových pojištěnců, pojištění a pojistných událostí
- Úprava a mazání záznamů
- ReatTime validace formulářů
- REST API komunikace mezi frontendem a backendem

---

## Použité technologie

### Frontend
- React (Vite)
- JavaScript
- Axios
- React Router

### Backend
- ASP.NET Core (C#)
- Entity Framework Core
- MS SQL Server
- REST API

---

### Úprava migrace

Při vytváření databáze je třeba upravit cizí klíč v první migraci následovně:

Změňte cizí klíč v migraci ze `Cascade` na `Restrict`, aby nedocházelo k automatickému mazání závislých záznamů:

migrationBuilder.AddForeignKey(
    name: "FK_Insurances_Insurers_InsurerId",
    table: "Insurances",
    column: "InsurerId",
    principalTable: "Insurers",
    principalColumn: "Id",
    onDelete: ReferentialAction.Cascade);

na:

migrationBuilder.AddForeignKey(
    name: "FK_Insurances_Insurers_InsurerId",
    table: "Insurances",
    column: "InsurerId",
    principalTable: "Insurers",
    principalColumn: "Id",
    onDelete: ReferentialAction.Restrict);

Důvod: Každé pojištění musí mít pojistitele, který ho spravuje. Nastavení Restrict zabraňuje automatickému smazání závislých záznamů.

## Přihlášení

Při prvním spuštění je automaticky vytvořen admin účet:

- **Email:** admin@example.com  
- **Heslo:** Admin123!

Autorizační systém není kompletně dokončen. Pro vytvoření nové pojistky je nutné být přihlášen jako pojišťovatel.

## Spuštění projektu

### Backend (.NET API)

cd Evidence-pojisteni
dotnet run

### Frontend (React)

cd pojistenci_v3.frontend
npm install
npm run dev
