# Documentatie proiect - FlorenBooks

## 1. Descriere generala

FlorenBooks este o aplicatie desktop Windows Forms dezvoltata in C# pentru administrarea unei biblioteci/librarii. Aplicatia permite autentificarea utilizatorilor, gestionarea rolurilor, adaugarea cartilor, cumpararea si imprumutarea cartilor, urmarirea imprumuturilor si exportul datelor in format CSV.

Aplicatia foloseste o baza de date PostgreSQL pentru stocarea utilizatorilor, cartilor, cumpararilor si imprumuturilor.

Adauga imagine cu ecranul principal al aplicatiei sau cu fereastra de autentificare.

## 2. Tehnologii folosite

- Limbaj: C#
- Framework: .NET 8
- Interfata grafica: Windows Forms
- Baza de date: PostgreSQL
- Driver baza de date: Npgsql
- Securizare parole: BCrypt.Net-Next
- Format export: CSV

## 3. Roluri in aplicatie

Aplicatia foloseste urmatoarele roluri:

- `user` - utilizator obisnuit
- `libraryAdmin` - administrator biblioteca/librarie
- `borrowAdmin` - administrator imprumuturi
- `superAdmin` - administrator principal

Adauga imagine cu schema rolurilor din aplicatie.

### User

Utilizatorul obisnuit poate:

- vedea toate cartile disponibile in biblioteca
- vedea statusul cartilor: `Disponibila` sau `Imprumutata`
- cumpara carti
- imprumuta carti disponibile
- vedea sectiunea `Cartile mele`
- exporta lista curenta in CSV

Adauga imagine cu dashboard-ul utilizatorului si lista de carti.

### Library Admin

Administratorul bibliotecii poate:

- adauga carti noi
- completa titlul, autorul, editura, anul si pretul
- vedea toate cartile din librarie
- vedea statusul fiecarei carti
- actualiza lista prin butonul `Refresh`

Adauga imagine cu dashboard-ul library admin si formularul de adaugare carte.

### Borrow Admin

Administratorul de imprumuturi poate:

- vedea toate cartile imprumutate
- vedea utilizatorul, titlul cartii, autorul, data imprumutului si data returnarii
- marca o carte ca returnata
- actualiza lista
- exporta datele in CSV

Adauga imagine cu dashboard-ul borrow admin si lista imprumuturilor.

### Super Admin

Super admin-ul poate:

- vedea si administra toti utilizatorii
- modifica email-ul unui utilizator
- modifica rolul unui utilizator
- sterge utilizatori
- vedea toate cartile din biblioteca
- exporta utilizatorii sau cartile in CSV, in functie de vederea curenta

Adauga imagine cu dashboard-ul super admin si butoanele `Utilizatori` / `Carti`.

## 4. Functionalitati principale

### Autentificare si inregistrare

Aplicatia permite crearea conturilor si autentificarea utilizatorilor. Parolele sunt salvate securizat folosind BCrypt.

Adauga imagine cu formularul de login si formularul de inregistrare.

### Administrarea cartilor

Cartile sunt salvate in tabela `books`. Pentru fiecare carte se retin:

- id
- titlu
- autor
- editura
- anul aparitiei
- pret
- data crearii

Statusul cartii este calculat din imprumuturile active:

- `Imprumutata` daca exista un rand in `borrowed_books` cu `return_date IS NULL`
- `Disponibila` daca nu exista imprumut activ

Adauga imagine cu tabelul de carti si coloana `Status`.

### Cumpararea cartilor

Cand un user cumpara o carte, aplicatia salveaza informatia in tabela `purchased_books`.

### Imprumutarea cartilor

Cand un user imprumuta o carte, aplicatia salveaza imprumutul in tabela `borrowed_books`.

O carte deja imprumutata nu mai poate fi imprumutata din nou pana nu este returnata.

### Returnarea cartilor

Borrow admin-ul poate marca o carte imprumutata ca returnata. Aplicatia completeaza `return_date` cu data curenta.

### Cartile mele

Sectiunea `Cartile mele` afiseaza cartile utilizatorului curent:

- cartile cumparate
- cartile imprumutate activ

In aceasta vedere, butoanele de cumparare si imprumut sunt ascunse, deoarece lista este doar pentru vizualizare.

Adauga imagine cu sectiunea `Cartile mele`.

### Export CSV

Aplicatia permite exportul datelor vizibile din tabele in fisiere CSV.

Exportul exclude coloanele cu butoane de actiune, precum `Buy`, `Borrow`, `Delete` sau `Return`.

Exemple de fisiere generate:

- `carti.csv`
- `cartile_mele.csv`
- `utilizatori.csv`
- `carti_biblioteca.csv`
- `imprumuturi.csv`

Adauga imagine cu dialogul de salvare CSV sau cu un fisier CSV deschis.

## 5. Structura proiectului

### Fisiere principale

- `Program.cs` - punctul de intrare al aplicatiei
- `DatabaseHelper.cs` - configurarea si deschiderea conexiunii la PostgreSQL
- `BookRepository.cs` - operatii pentru carti, cumparari si imprumuturi
- `UserRepository.cs` - operatii pentru utilizatori
- `CsvExportHelper.cs` - exportul tabelelor DataGridView in CSV
- `db_setup.sql` - script SQL pentru initializarea bazei de date

### Formulare Windows Forms

- `Authentification.cs` - autentificare
- `SignUp.cs` - creare cont
- `Userdashboard.cs` - dashboard pentru user
- `Librarydashboard.cs` - dashboard pentru library admin
- `Borrowdashboard.cs` - dashboard pentru borrow admin
- `Superdashboard.cs` - dashboard pentru super admin

### Modele

- `Models/User.cs` - model pentru utilizator
- `Models/Book.cs` - model pentru carte
- `Models/BorrowedBook.cs` - model pentru imprumut

## 6. Baza de date

Baza de date folosita este `florenbooksdb`.

Adauga imagine cu diagrama bazei de date sau cu tabelele din PostgreSQL.

### Tabela `users`

Stocheaza conturile utilizatorilor.

Campuri importante:

- `id`
- `email`
- `password`
- `role`
- `created_at`

### Tabela `books`

Stocheaza cartile din biblioteca.

Campuri importante:

- `id`
- `titlu`
- `autor`
- `editura`
- `anul`
- `pret`
- `created_at`

### Tabela `purchased_books`

Stocheaza cartile cumparate de utilizatori.

Campuri importante:

- `id`
- `user_id`
- `book_id`
- `purchase_date`

### Tabela `borrowed_books`

Stocheaza imprumuturile.

Campuri importante:

- `id`
- `user_id`
- `book_id`
- `borrow_date`
- `return_date`

Daca `return_date` este `NULL`, imprumutul este activ.

Adauga imagine cu relatiile dintre tabelele `users`, `books`, `purchased_books` si `borrowed_books`.

## 7. Instalare si rulare

### Cerinte

- Windows
- .NET 8 SDK
- PostgreSQL
- Visual Studio sau alt editor compatibil cu proiecte .NET

### Configurarea bazei de date

1. Se creeaza baza de date PostgreSQL:

```sql
CREATE DATABASE florenbooksdb;
```

2. Se ruleaza scriptul:

```bash
psql -U postgres -f db_setup.sql
```

3. Se verifica sirul de conexiune din `DatabaseHelper.cs`:

```csharp
Host=localhost;Port=5432;Database=florenbooksdb;Username=postgres;Password=psql98dan5;
```

Parola trebuie schimbata daca PostgreSQL foloseste alta parola locala.

### Rulare proiect

Din terminal:

```bash
dotnet build
dotnet run
```

Sau din Visual Studio:

1. Se deschide solutia `WinFormsAppV3FlorenBooksV3.slnx`
2. Se selecteaza proiectul ca startup project
3. Se apasa `Start`

## 8. Fluxuri de utilizare

### Adaugare carte

1. Se autentifica un utilizator cu rol `libraryAdmin`
2. Se completeaza campurile cartii
3. Se apasa `Adauga`
4. Cartea apare in lista din dreapta

Adauga imagine cu procesul de adaugare carte.

### Imprumutare carte

1. Se autentifica un utilizator cu rol `user`
2. Se selecteaza o carte disponibila
3. Se apasa `Borrow`
4. Statusul cartii devine `Imprumutata`

Adauga imagine cu imprumutarea unei carti.

### Returnare carte

1. Se autentifica un utilizator cu rol `borrowAdmin`
2. Se alege imprumutul activ
3. Se apasa `Return`
4. Imprumutul primeste data returnarii

Adauga imagine cu returnarea unei carti imprumutate.

### Vizualizare toate cartile ca super admin

1. Se autentifica un utilizator cu rol `superAdmin`
2. Se apasa `Carti`
3. Se afiseaza lista tuturor cartilor din biblioteca

### Vizualizare cartile mele

1. Se autentifica un utilizator cu rol `user`
2. Se apasa `Cartile mele`
3. Se afiseaza cartile cumparate si cele imprumutate activ

## 9. Observatii

- Aplicatia calculeaza statusul cartilor pe baza imprumuturilor active.
- Exportul CSV exporta doar datele vizibile in tabel.
- Pentru build in timpul dezvoltarii, aplicatia trebuie inchisa daca executabilul este blocat de Windows.
- Unele warning-uri nullable pot aparea la build, dar nu blocheaza compilarea.

## 10. Posibile imbunatatiri viitoare

- Validare mai stricta pentru email si pret
- Cautare si filtrare carti
- Stergere sau editare carti de catre library admin
- Istoric complet pentru cartile imprumutate de user
- Export CSV si pentru Librarydashboard
- Configurarea conexiunii la baza de date prin fisier extern
- Interfata mai moderna si redimensionare imbunatatita
