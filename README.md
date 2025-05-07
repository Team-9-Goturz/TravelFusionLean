
# 🌸 Gotorz / TravelFusion – Teknologistak (3. semester øve-projekt)

Velkommen til den tekniske oversigt over Gotorz-projektet 💖  
Her får du et kærligt overblik over, hvordan hele systemet hænger sammen – fra frontend til database og alt derimellem.

---

## Frontend

Brugerfladen er udviklet i **Blazor Server** – perfekt til dig der elsker at kode i C# og stadig lave moderne og responsive løsninger.  
Der anvendes **Razor Pages** til struktur og navigation, og **SignalR** giver os muligheden for dejlig realtidskommunikation – f.eks. til chat.  💅

---

## Backend (API)

I backenden bruger vi **ASP.NET Core Web API** – et sikkert og skalerbart fundament.  
**Entity Framework Core** står for kommunikationen med databasen, og vi har organiseret vores kode med **Dependency Injection** og pæne lagdelte services.  
Data sendes frem og tilbage gennem **DTO’er**, og vi bruger **mappers** til at holde det hele ryddeligt og læsbart.  
Alt i alt en elegant arkitektur – præcis som vi kan li’ det 🌷

---

## Database

Vores database er en klassisk **SQL Server**, hostet på en privat server (on-premise).  
Her gemmer vi alle vores rejsepakker, brugere, fly, hoteller og meget mere.  
**EF Core** sørger for at data flyder mellem app og database helt uden besvær.

---

## Mock API'er

For at kunne teste uden "rigtige" data har vi lavet to mock-API’er:
- **MockFlightsAPI**
- **MockHotelsAPI**

De kører i **Docker-containere** og er hosted i **Azure Container Apps** – supersmart og fleksibelt.  
Images ligger trygt i **Azure Container Registry**, men vi deployer dem manuelt (udvikling 😉).

---

## Containerization

Vi bruger **Docker** til at pakke mock-API'erne.  
Images hostes i **Azure Container Registry**, og herfra kan de trækkes direkte ind i vores **Azure Container Apps**.

---

## Hosting & Deployment

Frontend og backend er hostet i to separate **Azure App Services**, og vi bruger **GitHub Actions** til automatisk deployment.  
Det betyder, at hver gang vi pusher ny kode, så bliver det automatisk bygget og sendt i skyen ☁️  
Mock-API’erne deployes dog manuelt – og det fungerer rigtig fint for os.

---

## Logging & Overvågning

Vi bruger **Azure Logs** til at holde øje med appen – det hjælper os med at finde fejl og optimere brugeroplevelsen.  
Til vores API’er bruger vi **Swagger**, så både udviklere og testere kan lege med dem direkte i browseren 💻

---

## Sikkerhed & Autentificering

Brugerne har roller som **Admin** eller **Customer**, og adgangskoder er naturligvis **hash’et med salt**.  
Vi bruger **CORS** og **HTTPS** til at sikre kommunikationen mellem frontend og backend.  
Vi bruger **JWT eller sessions**, så login bliver endnu mere sikkert og professionelt 🔐

---

Med kærlighed fra team 9
Alberte, Alex, Ea, Hanisah & JOsephine💗

Hvis du læser dette – så husk: du er fantastisk! ✨

