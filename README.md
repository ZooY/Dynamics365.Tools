Пакет подключаемых модулей (Plug-in) и действий процессов (Workflow) для расширения стандартного функционала платформы Microsoft Dynamics 365.

# Скачать



# Состав пакета

## FetchXML Tools

Набор компонентов, позволяющих оперировать данными, используя запросы FetchXML.

### Workflow
<dl>
<dt>Assign Team</dt>
<dd>Назначение группы для всех записей, которые возвращает запрос FetchXML.</dd>
<dt>Assign User</dt>
<dd>Назначение пользователя для всех записей, которые возвращает запрос FetchXML.</dd>
<dt>Average</dt>
<dd>Вычисление среднего арифметического числовых значений, возвращаемых запросом FetchXML.</dd>
<dt>Count</dt>
<dd>Получение количества записей, возвращаемых запросом FetchXML.</dd>
<dt>Median</dt>
<dd>Вычисление медианы ряда числовых значений, возвращаемых запросом FetchXML.</dd>
<dt>Sum</dt>
<dd>Вычисление суммы ряда числовых значений, возвращаемых запросом FetchXML.</dd>
<dt>Value</dt>
<dd>Получение значения атрибута, возвращаемого запросом FetchXML.</dd>
</dl>

## String Tools

Набор компонентов для обработки строк.

### Plug-ins

<dl>
<dt>Format</dt>
<dd>Формирование значения строкового атрибута на основе строки формата. <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#format">Настройка</a></dd>
<dt>ToFirstTitleCase</dt>
<dd>Перевод первой буквы строки в верхний регистр. <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#tofirsttitlecase">Настройка</a></dd>
<dt>ToLowerCase</dt>
<dd>Перевод всех букв строки в нижний регистр. <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#tolowercase">Настройка</a></dd>
<dt>ToTitleCase</dt>
<dd>Перевод первых букв всех слов строки в верхний регистр. <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#totitlecase">Настройка</a></dd>
<dt>ToUpperCase</dt>
<dd>Перевод всех букв строки в верхний регистр. <a href="https://github.com/ZooY/Dynamics365.Tools/wiki/String-Tools#touppercase">Настройка</a></dd>
</dl>


### Workflow

<dl>
<dt>Characters</dt>
<dd>Специальные символы (пробел, перевод строки).</dd>
<dt>Replace</dt>
<dd>Замена подстроки в строке.</dd>
<dt>Substring</dt>
<dd>Получение подстроки.</dd>
<dt>ToFirstTitleCase</dt>
<dd>Перевод первой буквы строки в верхний регистр.</dd>
<dt>ToLowerCase</dt>
<dd>Перевод всех букв строки в нижний регистр.</dd>
<dt>ToTitleCase</dt>
<dd>Перевод первых букв всех слов строки в верхний регистр.</dd>
<dt>ToUpperCase</dt>
<dd>Перевод всех букв строки в верхний регистр.</dd>
<dt>Trim</dt>
<dd>Обрезание пробелов в начале и конце строки.</dd>
</dl>

## User Tools

Набор компонентов для работы с пользователями системы.

### Workflow

<dl>
<dt>Current</dt>
<dd>Получение текущих пользователей, кто инициировал запуск процесса и от чьего имени выполняется процесс.</dd>
</dl>

## Variable Tools

Набор компонентов, позволяющих использовать в рамках процесса переменные, а также создавать на их основе строки в формате JSON с многоуровневой вложенностью узлов.

### Workflow
<dl>
<dt>Get Value</dt>
<dd>Получение значения переменной.</dd>
<dt>Remove Dataset</dt>
<dd>Удаление всех переменных, относящихся к указанному набору данных.</dd>
<dt>Remove Value</dt>
<dd>Удаление переменной.</dd>
<dt>Set Value</dt>
<dd>Установка значения переменной.</dd>
<dt>To JSON</dt>
<dd>Формирование, на основе всех переменных набора, строки в формате JSON.</dd>
</dl>

## Web Tools

Компоненты, позволяющие выполнять запросы к сервисам удаленных систем.

### Workflow
<table>
<tr>
<td>POST JSON</td>
<td>Выполнение POST-запроса к удаленному веб-сервису и отправка данных в формате JSON.</td>
</tr>
<td>PUT JSON</td>
<td>Выполнение PUT-запроса к удаленному веб-сервису и отправка данных в формате JSON.</td>
</tr>
</table>