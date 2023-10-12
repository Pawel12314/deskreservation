#1

Poprawiłem mechanizm przyznawania praw administratora w aplikacji. Teraz do tego slużą 3 linki, 1 do przyznania praw, 2 do usunięcia praw, 3 do weryfikacji czy się ma prawa administratora.
W celu zobaczenia zmian, należy wylogować się i ponownie zalogować na koncie aplikacji.

I have corrected mechanism of admin rights granting. Now for this purpose are used 3 links. 1 for rights grant. 2 for revoking rights. 3 for checking if user have rights
In order to see changes, user need to logout and then login again in application

#2

Aplikacja jest ukończona, ale nie wykluczam że ją jeszcze będę rozwijał.

Application is finished, but I do not exclude that i will continue its development

#3

Aplikacja zawiera błąd projektowy, 
który przy innym wykorzystaniu API może stwarzać problemy, 
tj. operacje serwisu dostępu do bazy, nie zwracają numeru Id utworzonego elementu. 
W przypadku testów jednostkowych utrudnia to wykonanie sekwencji operacji na serwisach.

Application have a project design mistake,
which when using it with API may create problems.
i.e. Service operations doesnt returns id of created element
In case of unit tests it make doing database sequence operations harder without id of created element

#4

Zamiast id zwracam obiekt zawierający status operacji i wiadomość przeznaczoną dla front-endowej części aplikacji
która jest wyświetlana w warstwie prezentacji i informuje użytkownika o stanie i błędach wykonywanych przez niego operacji.
Wykorzystałem to rozwiązanie do zabezpieczenia aplikacji przed celowymi lub niecelowymi błędami użytkownika

Instead id I return object containing state of operation and message intended for frontend part of application
which is displaying in presentation layer and informs user about state and errors of done operations.
I used this solution to secure application from intended and not intended mistakes from user.


#5

Aplikacja jest podzielona na część dla administratora i dla zwykłego użytkownika
Kolor niebieski został wykorzystany w części dla administratora, a zielony w części dla zwykłego użytkownika

Application is divided into part for administrators and for normal users.
Blue color is used in administrator part and green in part for normal users
