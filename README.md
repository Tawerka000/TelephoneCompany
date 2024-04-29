# TelephoneCompany
 Реализовать десктопное приложение для работы с абонентами телефонной компании.

В базе данных определить следующие основные таблицы:
1) Таблица Abonent для хранения информации об абонентах (фио - обязательно);
2) Таблица Address для хранения адресов абонентов;
3) Таблица PhoneNumber для хранения номеров (учесть, что существует 3 типа номера - домашний, рабочий, мобильный);
4) Таблица Streets для хранения обслуживаемых компанией улиц.

Требования к возможностям приложения:
1) Основное окно - вывод информации об абонентах в формате таблицы и набор кнопок для прочих возможностей.
Ожидаемый набор кнопок:
- "Поиск" - вызов модального окна "Поиск по номеру"
- "Выгрузить CSV" - для запуска механизма выгрузки CSV
- "Улицы" - для вызова модального окна "Улицы"
Ожидаемые колонки в отображаемой таблицы:
- ФИО абонента
- Улица
- Номер дома
- Номер телефона (домашний)
- Номер телефона (рабочий)
- Номер телефона (мобильный)
Предусмотреть механизмы фильтрации и сортировки отображаемой таблицы по всем колонкам.
2) Модальное окно "Поиск по номеру" - содержит текстовое поле для ввода номера.
При успехе поиска ожидается вывод в таблице только совпавших по критерию поиска абонентов, при отсутствии совпадений ожидается информативное окно с текстом "Нет абонентов, удовлетворяющих критерию поиска".
3) Модальное окно "Улицы" отображает информацию об обслуживаемых улицах и количестве абонентов на каждой из них в табличном формате.
4) Кнопка "Выгрузить CSV" запускает механизм формирования файла report_{текущая дата и время}.csv, в котором содержится информация из таблицы основного окна с учётом фильтров и сортировки.

Особые требования:
1) В качестве фронтенда использовать WPF
2) Использовать для работы с БД dapper.
3) БД - любая
4) Использовать паттерн MVVM
