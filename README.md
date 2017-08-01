Пакет подключаемых модулей (Plug-in) и действий процессов (Workflow) для расширения стандартного функционала платформы Microsoft Dynamics 365.

# Скачать



# Состав пакета

## FetchXML Tools

Набор компонентов, позволяющих оперировать данными, используя запросы FetchXML.

### Workflow
<table>
<tr>
<td>Assign Team</td>
<td>Назначение группы для всех записей, которые возвращает запрос FetchXML.</td>
</tr><tr>
<td>Assign User</td>
<td>Назначение пользователя для всех записей, которые возвращает запрос FetchXML.</td>
</tr><tr>
<td>Average</td>
<td>Вычисление среднего арифметического числовых значений, возвращаемых запросом FetchXML.</td>
</tr><tr>
<td>Count</td>
<td>Получение количества записей, возвращаемых запросом FetchXML.</td>
</tr><tr>
<td>Median</td>
<td>Вычисление медианы ряда числовых значений, возвращаемых запросом FetchXML.</td>
</tr><tr>
<td>Sum</td>
<td>Вычисление суммы ряда числовых значений, возвращаемых запросом FetchXML.</td>
</tr><tr>
<td>Value</td>
<td>Получение значения атрибута, возвращаемого запросом FetchXML.</td>
</tr>
</table>

## String Tools

Набор компонентов для обработки строк.

### Plug-ins

<table>
<tr>
<td>Format</td>
<td>Формирование значения строкового атрибута на основе строки формата. <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#format">Настройка</a></td>
</tr><tr>
<td>ToFirstTitleCase</td>
<td>Перевод первой буквы строки в верхний регистр. <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#tofirsttitlecase">Настройка</a></td>
</tr><tr>
<td>ToLowerCase</td>
<td>Перевод всех букв строки в нижний регистр. <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#tolowercase">Настройка</a></td>
</tr><tr>
<td>ToTitleCase</td>
<td>Перевод первых букв всех слов строки в верхний регистр. <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#totitlecase">Настройка</a></td>
</tr><tr>
<td>ToUpperCase</td>
<td>Перевод всех букв строки в верхний регистр. <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#touppercase">Настройка</a></td>
</tr>
</table>


### Workflow

<table>
<tr>
<td>Characters</td>
<td>Специальные символы (пробел, перевод строки).</td>
</tr><tr>
<td>Replace</td>
<td>Замена подстроки в строке.</td>
</tr><tr>
<td>Substring</td>
<td>Получение подстроки.</td>
</tr><tr>
<td>ToFirstTitleCase</td>
<td>Перевод первой буквы строки в верхний регистр.</td>
</tr><tr>
<td>ToLowerCase</td>
<td>Перевод всех букв строки в нижний регистр.</td>
</tr><tr>
<td>ToTitleCase</td>
<td>Перевод первых букв всех слов строки в верхний регистр.</td>
</tr><tr>
<td>ToUpperCase</td>
<td>Перевод всех букв строки в верхний регистр.</td>
</tr><tr>
<td>Trim</td>
<td>Обрезание пробелов в начале и конце строки.</td>
</tr>
</table>

## User Tools

Набор компонентов для работы с пользователями системы.

### Workflow

<table>
<tr>
<td>Current</td>
<td>Получение текущих пользователей, кто инициировал запуск процесса и от чьего имени выполняется процесс.</td>
</tr>
</table>

## Variable Tools

Набор компонентов, позволяющих использовать в рамках процесса переменные, а также создавать на их основе строки в формате JSON с многоуровневой вложенностью узлов.

### Workflow
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

### Workflow
<table>
<tr>
<td>POST JSON</td>
<td>Выполнение POST-запроса к удаленному веб-сервису и отправка данных в формате JSON.</td>
</tr><tr>
<td>PUT JSON</td>
<td>Выполнение PUT-запроса к удаленному веб-сервису и отправка данных в формате JSON.</td>
</tr>
</table>