w przypadku braku utworzonej bazy danych aby załadować bazę danych należy wykonać następujące komendy w konsoli nuget managera
dotnet ef migrations add InitialCreate
dotnet ef database update

aby skorzystac z uprawnien administratora w bazie, nalezy zarejestrowac uzytkownika o podanym emailu: "testemail@gmail.com" a nastepnie wylaczyc serwer i wlaczyc ponownie. Aplikacja w kodzie inicjujacym aplikacje nada mu uprawnienia administratora

