<img src="https://github.com/ZooY/Dynamics365.Tools/blob/develop/Docs/crm2016.png?raw=true" /><img src="https://github.com/ZooY/Dynamics365.Tools/blob/develop/Docs/dynamics365.png?raw=true" align="right" />

Пакет подключаемых модулей (Plug-in) и действий процессов (Workflow) для расширения стандартного функционала платформы Microsoft Dynamics CRM 2016 / Dynamics 365.

# Download

<a href="https://github.com/ZooY/Dynamics365.Tools/releases">Скачать</a> текущую версию пакета компонентов в виде управляемых и неуправляемых решений для CRM, а также в виде архивов с исходным кодом.

# Нашли ошибку?

Компонент работает некорректно - <a href="https://github.com/ZooY/Dynamics365.Tools/issues">напишите об этом</a>.

# Есть предложение?

Есть идеи новых компонентов или улучшения существующих - <a href="https://github.com/ZooY/Dynamics365.Tools/issues">напишите об этом</a>.

# Состав пакета

## Date Tools

Набор компонентов для работы с данными типа дата/время.

Workflow

<table>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#add-days">Add Days</a></td>
<td>Добавление к дате указанное количество дней.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#add-hours">Add Hours</a></td>
<td>Добавление к дате указанное количество часов.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#add-milliseconds">Add Milliseconds</a></td>
<td>Добавление к дате указанное количество миллисекунд.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#add-minutes">Add Minutes</a></td>
<td>Добавление к дате указанное количество минут. </td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#add-months">Add Months</a></td>
<td>Добавление к дате указанное количество месяцев.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#add-seconds">Add Seconds</a></td>
<td>Добавление к дате указанное количество секунд.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#add-years">Add Years</a></td>
<td>Добавление к дате указанное количество лет.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#age">Age</a></td>
<td>Расчет возраста (количества полных лет).</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#create">Create</a></td>
<td>Создание даты из составных частей.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#diff">Diff</a></td>
<td>Разница между двумя датами.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#format">Format</a></td>
<td>Форматирование даты и представление ее в виде строки.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#now">Now</a></td>
<td>Текущая дата.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#parse">Parse</a></td>
<td>Преобразование строки в дату.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Date-Tools#parts">Parts</a></td>
<td>Получение отдельных составных частей даты.</td>
</tr>
</table>

## Debug Tools

Набор компонентов для отладки Workflow.

Workflow

<table>
<tr>
<td>:new: <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Debug-Tools#get-shared-variable">Get Shared Variable</a></td>
<td>Получение значения общей переменной.</td>
</tr>
<tr>
<td>:new: <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Debug-Tools#throw-excaption">Throw Excaption</a></td>
<td>Вызов исключения.</td>
</tr>
</table>

## E-mail Tools

Набор компонентов для работы с сообщениями электронной почты.

Plug-ins

<table>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/Email-Tools#send-e-mail-pre-processing">Send E-mail Pre-Processing</a></td>
<td>Предварительная обработка события отправки электронного письма с помощью действий.</td>
</tr>
</table>

Workflow

<table>
<tr>
<td>Send</td>
<td>Отправка существующего электронного письма.</td>
</tr>
</table>

## Entity Tools

Набор компонентов для работы с различными сущностями.

Workflow

<table>
<tr>
<td>Account to GUID</td>
<td>Getting the GUID of the Account entity.</td>
</tr>
<tr>
<td>Create Annotation</td>
<td>Создание примечания для произвольной сущности.</td>
</tr>
<tr>
<td>Current Entity ID</td>
<td>Получение ID текущей записи.</td>
</tr>
<tr>
<td>Delete</td>
<td>Удаление произвольной записи.</td>
</tr>
<tr>
<td>Delete Current</td>
<td>Удаление текущей записи.</td>
</tr>
<tr>
<td>GUID to Account</td>
<td>Действие возвращает сущность "Организация" (account) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Activity</td>
<td>Действие возвращает сущность "Звонок" (phonecall) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Business Unit</td>
<td>Действие возвращает сущность "Подразделение" (businessunit) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Contact</td>
<td>Действие возвращает сущность "Персона" (contact) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Currency</td>
<td>Действие возвращает сущность "Валюта" (transactioncurrency) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Invoice</td>
<td> Действие возвращает сущности "Счет" (invoice) и "Продукт счета" (invoicedetail) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Lead</td>
<td>Действие возвращает сущность "Интерес" (lead) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Marketing List</td>
<td>Действие возвращает сущность "Маркетинговый список" (list) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Opportunity</td>
<td>Действие возвращает сущности "Возможная сделка" (opportunity) и "Продукт возможой сделки" (opportunityproduct) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Order</td>
<td>Действие возвращает сущности "Заказ" (salesorder) и "Продукт заказа" (salesorderdetail) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Product</td>
<td>Действие возвращает сущности "Продукт" (product), "Прайс-лист" (pricelevel), "Продукт прайс-листа" (productpricelevel) и "Единица изменения" (uom) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Queue</td>
<td>Действие возвращает сущности "Очередь" (queue) и "Элемент очереди" (queueitem) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Quote</td>
<td>Действие возвращает сущности "Предложение" (quote) и "Продукт предложения" (quotedetail) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Team</td>
<td>Действие возвращает сущность "Группа пользователей" (team) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to User</td>
<td>Действие возвращает сущность "Пользователь" (systemuser) для указанного GUID.</td>
</tr>
<tr>
<td>GUID to Workflow</td>
<td>Действие возвращает сущность "Бизнес-процесс" (workflow) для указанного GUID.</td>
</tr>
</table>

## FetchXML Tools

Набор компонентов, позволяющих оперировать данными, используя запросы FetchXML.

Workflow

<table>
<tr>
<td>Assign Team</td>
<td>Назначение группы для всех записей, которые возвращает запрос FetchXML.</td>
</tr>
<tr>
<td>Assign User</td>
<td>Назначение пользователя для всех записей, которые возвращает запрос FetchXML.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/FetchXML-Tools#average">Average</a></td>
<td>Вычисление среднего арифметического числовых значений, возвращаемых запросом FetchXML.</td>
</tr>
<tr>
<td>Count</td>
<td>Получение количества записей, возвращаемых запросом FetchXML.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/FetchXML-Tools#median">Median</a></td>
<td>Вычисление медианы ряда числовых значений, возвращаемых запросом FetchXML.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/FetchXML-Tools#sum">Sum</a></td>
<td>Вычисление суммы ряда числовых значений, возвращаемых запросом FetchXML.</td>
</tr>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/FetchXML-Tools#value">Value</a></td>
<td>Получение значения атрибута, возвращаемого запросом FetchXML.</td>
</tr>
</table>

## File Tools

Набор компонентов для работы с файлами.

Workflow

<table>
<tr>
<td>Last Annotation File by Name</td>
<td>Получение последнего файла из примечания по имени этого файла. Порядок примечаний определяется по дате создания примечания.</td>
</tr>
<tr>
<td>Web Resource</td>
<td>Получение содержимого файла веб-ресурса.</td>
</tr>
</table>

## JSON Tools

Набор компонентов для работы со строками в формате JSON.

Workflow

<table>
<tr>
<td>Get Value</td>
<td>Получение значения из строки формата JSON.</td>
</tr>
</table>

## PDF Tools

Набор компонентов для работы с файлами PDF.

Workflow

<table>
<tr>
<td>Fill Template</td>
<td>Заполнение шаблона PDF.</td>
</tr>
</table>

## RabbitMQ Tools

Набор компонентов для работы с RabbitMQ.

Workflow

<table>
<tr>
<td>:new: Send</td>
<td>Отправка сообщения в RabbitMQ.</td>
</tr>
</table>

## String Tools

Набор компонентов для обработки строк.

Plug-ins

<table>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#format">Format</a></td>
<td>Формирование значения строкового атрибута на основе строки формата.</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#tofirsttitlecase">To First Title Case</a></td>
<td>Перевод первой буквы строки в верхний регистр.</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#tolowercase">To Lower Case</a></td>
<td>Перевод всех букв строки в нижний регистр.</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#totitlecase">To Title Case</a></td>
<td>Перевод первых букв всех слов строки в верхний регистр, а остальных в нижний.</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#touppercase">To Upper Case</a></td>
<td>Перевод всех букв строки в верхний регистр.</td>
</tr>
</table>

Workflow

<table>
<tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#characters">Characters</a></td>
<td>Специальные символы (пробел, перевод строки).</td>
</tr><tr>
<td>:new: <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#jaro-distance">Jaro Distance</a></td>
<td>Сходство Джаро между двумя строками.</td>
</tr><tr>
<td>:new: <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#jaro-winkler-distance">Jaro-Winkler Distance</a></td>
<td>Сходство Джаро—Винклера между двумя строками.</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#replace">Replace</a></td>
<td>Замена подстроки в строке.</td>
</tr><tr>
<td>:new: <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#strings">Strings</a></td>
<td>Специальные строки (GUID).</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#substring">Substring</a></td>
<td>Получение подстроки.</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#to-first-title-case">To First Title Case</a></td>
<td>Перевод первой буквы строки в верхний регистр.</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#to-lower-case">To Lower Case</a></td>
<td>Перевод всех букв строки в нижний регистр.</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#to-title-case">To Title Case</a></td>
<td>Перевод первых букв всех слов строки в верхний регистр.</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#to-upper-case">To Upper Case</a></td>
<td>Перевод всех букв строки в верхний регистр.</td>
</tr><tr>
<td><a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#trim">Trim</a></td>
<td>Обрезание пробелов в начале и конце строки.</td>
</tr>
</table>

## UI

Компоненты пользовательского интерфейса.

Web

<table>
<tr>
<td>:new: <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/UI-Tools#icons">Icons</a></td>
<td>Library for creating icons in the form header.</td>
</tr>
<tr>
<td>:new: <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/UI-Tools#tabs">Tabs</a></td>
<td>Формирование на форме вкладок вместо раскрывающихся разделов.</td>
</tr>
</table>

## User Tools

Набор компонентов для работы с пользователями системы.

Workflow

<table>
<tr>
<td>Current</td>
<td>Получение текущих пользователей, кто инициировал запуск процесса и от чьего имени выполняется процесс.</td>
</tr>
</table>

## Variable Tools

Набор компонентов, позволяющих использовать в рамках процесса переменные, а также создавать на их основе строки в формате JSON с многоуровневой вложенностью узлов.

Workflow
<table>
<tr>
<td>Get Value</td>
<td>Получение значения переменной.</td>
</tr><tr>
<td>Remove Dataset</td>
<td>Удаление всех переменных, относящихся к указанному набору данных.</td>
</tr><tr>
<td>Remove Value</td>
<td>Удаление переменной.</td>
</tr><tr>
<td>Set Value</td>
<td>Установка значения переменной.</td>
</tr><tr>
<td>To JSON</td>
<td>Формирование, на основе всех переменных набора, строки в формате JSON.</td>
</tr>
</table>

## Web Tools

Компоненты, позволяющие выполнять запросы к сервисам удаленных систем.

Workflow
<table>
<tr>
<td>POST JSON</td>
<td>Выполнение POST-запроса к удаленному веб-сервису и отправка данных в формате JSON.</td>
</tr><tr>
<td>PUT JSON</td>
<td>Выполнение PUT-запроса к удаленному веб-сервису и отправка данных в формате JSON.</td>
</tr>
</table>
