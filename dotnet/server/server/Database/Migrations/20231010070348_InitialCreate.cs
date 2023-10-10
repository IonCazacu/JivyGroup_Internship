using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConfirmPassword", "Email", "Password", "Username", "Uuid" },
                values: new object[,]
                {
                    { 1, "username1Password!", "username1@mail.com", "username1Password!", "username1", new Guid("c8e68271-3d93-4c44-a782-4927bd83c026") },
                    { 2, "username2Password!", "username2@mail.com", "username2Password!", "username2", new Guid("1796b518-c738-4141-8bb2-9358dd79858e") },
                    { 3, "username3Password!", "username3@mail.com", "username3Password!", "username3", new Guid("44b85378-4bbe-4bcf-b7af-2502d2ad4ae5") },
                    { 4, "username4Password!", "username4@mail.com", "username4Password!", "username4", new Guid("540be087-d6a7-49bb-9d5b-f7743d904e85") },
                    { 5, "username5Password!", "username5@mail.com", "username5Password!", "username5", new Guid("7c6ada17-8bda-422e-9ce0-6654f157905d") },
                    { 6, "username6Password!", "username6@mail.com", "username6Password!", "username6", new Guid("0d7c863b-6c6e-46d7-a246-7289dd2b6559") },
                    { 7, "username7Password!", "username7@mail.com", "username7Password!", "username7", new Guid("b8081fc8-272e-4d61-903a-544c6e3512ff") },
                    { 8, "username8Password!", "username8@mail.com", "username8Password!", "username8", new Guid("80bad392-14b1-4a3c-9524-4555cbb5a7c3") },
                    { 9, "username9Password!", "username9@mail.com", "username9Password!", "username9", new Guid("7dadea58-c77c-4c9a-80d6-3bbaa9fa02cd") },
                    { 10, "username10Password!", "username10@mail.com", "username10Password!", "username10", new Guid("f4c6fdf4-39fc-42d5-b630-57fe3cbe6505") },
                    { 11, "username11Password!", "username11@mail.com", "username11Password!", "username11", new Guid("7525ef9e-b7c0-4fa0-8c5a-3376fba24fe6") },
                    { 12, "username12Password!", "username12@mail.com", "username12Password!", "username12", new Guid("eba3e990-42fb-4e07-a596-6550110ad3a9") },
                    { 13, "username13Password!", "username13@mail.com", "username13Password!", "username13", new Guid("48511a77-c5fa-4ecd-bf0e-be09ffed3a38") },
                    { 14, "username14Password!", "username14@mail.com", "username14Password!", "username14", new Guid("2b639f55-404f-4e17-b176-668d9847f0c4") },
                    { 15, "username15Password!", "username15@mail.com", "username15Password!", "username15", new Guid("3934460e-4729-4f17-9b9b-62229947fe62") },
                    { 16, "username16Password!", "username16@mail.com", "username16Password!", "username16", new Guid("7f165323-faf5-4a3c-a6b7-ec37ecb8d7d9") },
                    { 17, "username17Password!", "username17@mail.com", "username17Password!", "username17", new Guid("294a2ff1-3fee-4ee8-beb6-a0bd3ae8b3fc") },
                    { 18, "username18Password!", "username18@mail.com", "username18Password!", "username18", new Guid("c0a03ae8-9f38-41a5-8702-e093aa763d72") },
                    { 19, "username19Password!", "username19@mail.com", "username19Password!", "username19", new Guid("7999bfbf-7ebd-4372-a4e8-9dad1d311594") },
                    { 20, "username20Password!", "username20@mail.com", "username20Password!", "username20", new Guid("02191fec-004d-4dbd-8445-b90d4b87633f") },
                    { 21, "username21Password!", "username21@mail.com", "username21Password!", "username21", new Guid("4e2ce9de-ee66-4d27-bb5b-82e2b5c99a7e") },
                    { 22, "username22Password!", "username22@mail.com", "username22Password!", "username22", new Guid("269ee745-69de-4489-840d-4ff6fde4ea50") },
                    { 23, "username23Password!", "username23@mail.com", "username23Password!", "username23", new Guid("3e0fd718-aa6e-4e8e-9282-fe57da89a4eb") },
                    { 24, "username24Password!", "username24@mail.com", "username24Password!", "username24", new Guid("44fb6993-1ea0-473c-883b-d0ce33068047") },
                    { 25, "username25Password!", "username25@mail.com", "username25Password!", "username25", new Guid("43684cf7-d65f-4f9f-8000-321eb7b2f5fd") },
                    { 26, "username26Password!", "username26@mail.com", "username26Password!", "username26", new Guid("7af01e90-db13-427e-9f72-50b871050149") },
                    { 27, "username27Password!", "username27@mail.com", "username27Password!", "username27", new Guid("4bbc0525-f08c-4783-a022-98d9dbc0e75e") },
                    { 28, "username28Password!", "username28@mail.com", "username28Password!", "username28", new Guid("ba888d88-b19d-4e3c-bf04-1f385cf40e47") },
                    { 29, "username29Password!", "username29@mail.com", "username29Password!", "username29", new Guid("1ba06e50-8778-4a66-a842-c4d8948c0956") },
                    { 30, "username30Password!", "username30@mail.com", "username30Password!", "username30", new Guid("0e37c606-9ba2-4046-a5f8-a6c915d1098c") },
                    { 31, "username31Password!", "username31@mail.com", "username31Password!", "username31", new Guid("3258b1db-71ab-4809-a1b0-4280ca59d5db") },
                    { 32, "username32Password!", "username32@mail.com", "username32Password!", "username32", new Guid("306a1085-1bd8-49fa-8d30-4a3bd060f2bc") },
                    { 33, "username33Password!", "username33@mail.com", "username33Password!", "username33", new Guid("e92a0b23-d3b4-45f4-948e-c71408eb7940") },
                    { 34, "username34Password!", "username34@mail.com", "username34Password!", "username34", new Guid("7cf4fa8e-422a-49eb-9f1a-88fc4e7386df") },
                    { 35, "username35Password!", "username35@mail.com", "username35Password!", "username35", new Guid("9c97dcf4-bd9d-4b11-8d1c-eed4d72c6889") },
                    { 36, "username36Password!", "username36@mail.com", "username36Password!", "username36", new Guid("d0d37a40-7103-4065-a197-2fe7df363ac6") },
                    { 37, "username37Password!", "username37@mail.com", "username37Password!", "username37", new Guid("b542f8f7-0c54-42d5-810d-621750e9babf") },
                    { 38, "username38Password!", "username38@mail.com", "username38Password!", "username38", new Guid("544dbbf5-1551-43ae-8266-1f80ea57f125") },
                    { 39, "username39Password!", "username39@mail.com", "username39Password!", "username39", new Guid("81f91fe0-486e-4b47-9a89-8052b8145aaf") },
                    { 40, "username40Password!", "username40@mail.com", "username40Password!", "username40", new Guid("a90d7946-6343-476d-a889-d1467f52bd46") },
                    { 41, "username41Password!", "username41@mail.com", "username41Password!", "username41", new Guid("d8c249dd-68d5-43f5-ba50-780e837f15ac") },
                    { 42, "username42Password!", "username42@mail.com", "username42Password!", "username42", new Guid("dfaebe91-8974-455c-a5da-02eea6f6cb30") },
                    { 43, "username43Password!", "username43@mail.com", "username43Password!", "username43", new Guid("382eeb7e-4b5a-472a-ae13-e9521807bb9a") },
                    { 44, "username44Password!", "username44@mail.com", "username44Password!", "username44", new Guid("332d5788-e121-438a-8074-a0c1ab550db9") },
                    { 45, "username45Password!", "username45@mail.com", "username45Password!", "username45", new Guid("d4cfa7cf-7775-4888-b0ec-2a27370c5642") },
                    { 46, "username46Password!", "username46@mail.com", "username46Password!", "username46", new Guid("8b20e872-bd4c-4b73-b053-1940c1fbe8b5") },
                    { 47, "username47Password!", "username47@mail.com", "username47Password!", "username47", new Guid("079ad9dd-17a3-4557-a0c2-e4d1a6035869") },
                    { 48, "username48Password!", "username48@mail.com", "username48Password!", "username48", new Guid("be86d2ca-d3ff-4624-a98a-0f0f0022a619") },
                    { 49, "username49Password!", "username49@mail.com", "username49Password!", "username49", new Guid("8be6970b-6b08-4cf8-8338-3ff9b1282e66") },
                    { 50, "username50Password!", "username50@mail.com", "username50Password!", "username50", new Guid("8d7f5b4e-271c-43d5-af88-d44deb9600fd") },
                    { 51, "username51Password!", "username51@mail.com", "username51Password!", "username51", new Guid("c4dc8c94-3253-4666-9133-cfa92f4541f6") },
                    { 52, "username52Password!", "username52@mail.com", "username52Password!", "username52", new Guid("9ec44e30-1100-405d-9ab9-ce5106014fea") },
                    { 53, "username53Password!", "username53@mail.com", "username53Password!", "username53", new Guid("3354a557-d89b-428e-962a-df2e7bc004b4") },
                    { 54, "username54Password!", "username54@mail.com", "username54Password!", "username54", new Guid("07c17e45-21ec-4aa4-930c-f79db79b7609") },
                    { 55, "username55Password!", "username55@mail.com", "username55Password!", "username55", new Guid("51cc3d8f-396f-4c8f-9b66-99d1ee033c7b") },
                    { 56, "username56Password!", "username56@mail.com", "username56Password!", "username56", new Guid("c6644684-69f8-4a03-9987-717a0b2cf6e2") },
                    { 57, "username57Password!", "username57@mail.com", "username57Password!", "username57", new Guid("e76c6e60-f446-468d-bc9a-3832102682b8") },
                    { 58, "username58Password!", "username58@mail.com", "username58Password!", "username58", new Guid("142ac41e-f3ca-45d4-a86c-5e5f7a07ff2d") },
                    { 59, "username59Password!", "username59@mail.com", "username59Password!", "username59", new Guid("d2a60247-8fbf-42ad-ac7d-2d632ea5505a") },
                    { 60, "username60Password!", "username60@mail.com", "username60Password!", "username60", new Guid("bd62bb4f-5294-4eda-882b-d477f2bf981f") },
                    { 61, "username61Password!", "username61@mail.com", "username61Password!", "username61", new Guid("ef7a317d-0d86-409e-93f1-b983e7183fd8") },
                    { 62, "username62Password!", "username62@mail.com", "username62Password!", "username62", new Guid("52487453-dbfc-4768-8aea-2439f9f84a5a") },
                    { 63, "username63Password!", "username63@mail.com", "username63Password!", "username63", new Guid("8faa29e8-a0f1-44c9-8a21-aa12ac6a7f88") },
                    { 64, "username64Password!", "username64@mail.com", "username64Password!", "username64", new Guid("f9db8d64-6e4c-4444-a9c3-98b024e1c62b") },
                    { 65, "username65Password!", "username65@mail.com", "username65Password!", "username65", new Guid("4adcc301-1e47-4cc9-b7f9-49ee82fb23f7") },
                    { 66, "username66Password!", "username66@mail.com", "username66Password!", "username66", new Guid("bfdd9f8f-f9f5-48fd-ac80-0fbea931e913") },
                    { 67, "username67Password!", "username67@mail.com", "username67Password!", "username67", new Guid("c55603a7-0f36-484d-aefc-17c2ba9f5e55") },
                    { 68, "username68Password!", "username68@mail.com", "username68Password!", "username68", new Guid("5b2e53bf-884e-407c-8481-757e72d003f8") },
                    { 69, "username69Password!", "username69@mail.com", "username69Password!", "username69", new Guid("8d824bf4-c1e6-4035-b786-5874cbb80dc1") },
                    { 70, "username70Password!", "username70@mail.com", "username70Password!", "username70", new Guid("12cc6c5f-b84d-4524-8e49-f287a5877e59") },
                    { 71, "username71Password!", "username71@mail.com", "username71Password!", "username71", new Guid("823557c9-9e9c-40e3-95ff-189c10f461e8") },
                    { 72, "username72Password!", "username72@mail.com", "username72Password!", "username72", new Guid("60799f86-0ba4-4f5c-83bc-81a8e0cc88f0") },
                    { 73, "username73Password!", "username73@mail.com", "username73Password!", "username73", new Guid("0e8b36ba-3554-4512-bc8c-65f3b086d33d") },
                    { 74, "username74Password!", "username74@mail.com", "username74Password!", "username74", new Guid("2fd9c350-0658-4172-ac51-d873308c6104") },
                    { 75, "username75Password!", "username75@mail.com", "username75Password!", "username75", new Guid("209eec98-f0bd-44dc-a184-80fd86626024") },
                    { 76, "username76Password!", "username76@mail.com", "username76Password!", "username76", new Guid("1dad7e75-a5ae-431b-bf9e-25b3787dea6c") },
                    { 77, "username77Password!", "username77@mail.com", "username77Password!", "username77", new Guid("1bc7fc97-c540-45c1-b67f-bba1a960f98e") },
                    { 78, "username78Password!", "username78@mail.com", "username78Password!", "username78", new Guid("38ccd564-93cf-4683-acbd-58368dc6c439") },
                    { 79, "username79Password!", "username79@mail.com", "username79Password!", "username79", new Guid("0237bd0c-d79b-4be0-a993-e5f121eda5fa") },
                    { 80, "username80Password!", "username80@mail.com", "username80Password!", "username80", new Guid("de6db950-e04f-4f56-8c36-f6e5c36f02da") },
                    { 81, "username81Password!", "username81@mail.com", "username81Password!", "username81", new Guid("1ad68046-a9c7-41fa-83df-53ba15c0e6cc") },
                    { 82, "username82Password!", "username82@mail.com", "username82Password!", "username82", new Guid("f394f059-8e86-4962-96be-d816f07d0034") },
                    { 83, "username83Password!", "username83@mail.com", "username83Password!", "username83", new Guid("129c13eb-cbce-49cf-94c1-6350cde7c9a2") },
                    { 84, "username84Password!", "username84@mail.com", "username84Password!", "username84", new Guid("7c923ba8-1ef4-49fa-821c-c4a64b89adb7") },
                    { 85, "username85Password!", "username85@mail.com", "username85Password!", "username85", new Guid("a32e3b1b-92a3-4bc8-b5bb-963d4aac6ef4") },
                    { 86, "username86Password!", "username86@mail.com", "username86Password!", "username86", new Guid("1c23a52f-8b24-4ac1-9480-c6be765cb941") },
                    { 87, "username87Password!", "username87@mail.com", "username87Password!", "username87", new Guid("87667112-7b06-4a0a-a850-0fe817282c1f") },
                    { 88, "username88Password!", "username88@mail.com", "username88Password!", "username88", new Guid("7353fd7c-43f0-491f-91e5-1a90c3eb7ec9") },
                    { 89, "username89Password!", "username89@mail.com", "username89Password!", "username89", new Guid("bafa47f3-2d27-47d8-94d3-e75e9b2c178c") },
                    { 90, "username90Password!", "username90@mail.com", "username90Password!", "username90", new Guid("a985d6ab-88f2-417f-bc0d-9b4c64178800") },
                    { 91, "username91Password!", "username91@mail.com", "username91Password!", "username91", new Guid("cd6e8100-c821-48d2-a637-e1ed8a8a3f22") },
                    { 92, "username92Password!", "username92@mail.com", "username92Password!", "username92", new Guid("4cc59e8a-1672-49a9-8ada-00337bdaaaa8") },
                    { 93, "username93Password!", "username93@mail.com", "username93Password!", "username93", new Guid("07e30477-42d6-4e32-a8b5-91ff80014710") },
                    { 94, "username94Password!", "username94@mail.com", "username94Password!", "username94", new Guid("7f6cb152-0f9b-40db-a900-0f924ccb5caa") },
                    { 95, "username95Password!", "username95@mail.com", "username95Password!", "username95", new Guid("8383f0cf-f1cf-40c1-aab4-49f011bc35e7") },
                    { 96, "username96Password!", "username96@mail.com", "username96Password!", "username96", new Guid("92b0e13a-9da8-4ef5-be25-54f0987afd94") },
                    { 97, "username97Password!", "username97@mail.com", "username97Password!", "username97", new Guid("0a18e7f4-4841-4b32-bfca-48c7aa247403") },
                    { 98, "username98Password!", "username98@mail.com", "username98Password!", "username98", new Guid("af82d65d-5af7-4e36-b367-161cf5f92b9f") },
                    { 99, "username99Password!", "username99@mail.com", "username99Password!", "username99", new Guid("4299a0c7-2793-4533-acbb-e4a866ae3397") },
                    { 100, "username100Password!", "username100@mail.com", "username100Password!", "username100", new Guid("279507ee-738c-4d15-9f9c-ebe5145af68a") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
