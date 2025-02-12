# Pojistenci
Aplikace pro evidenci pojistencu

Nejdříve je nutno vytvořit databázi a spustit na ní skript pro vytvoření procedur a sekvencí.
Skript se nachází v souboru Database script.txt.

Při vytváření databáze je nutno v první migraci změnit:

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Insurers_InsurerId",
                table: "Insurances",
                column: "InsurerId",
                principalTable: "Insurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
na

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Insurers_InsurerId",
                table: "Insurances",
                column: "InsurerId",
                principalTable: "Insurers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

z důvodu nastavení Restrict míto Cascade.
Každé pojištění musím mít pojistitele který spravuje pojištění.

Při prvním spuštění je vytvořen admin účet.
Pro přihlášení stačí zadat admin@example.com, Admin123!.

Autorizace není dokončena. V případě tvorby pojistky je ale nutné být přihlášen jako pojistitel.
