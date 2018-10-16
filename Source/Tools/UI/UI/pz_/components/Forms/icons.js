/*!
 * @version 1.0.0
 * @name Form Icons Library
 * @description Library for creating icons in the form header.
 */


typeof (PZone) === 'undefined' && (PZone = {});
typeof (PZone.UI) === 'undefined' && (PZone.UI = {});
typeof (PZone.UI.Forms) === 'undefined' && (PZone.UI.Forms = {});


/**
 * 
 * @param {object} icons Список объектов с данными об иконках.
 *
 * iconInfo {
 *      shape : string, // circle | square
 *      tooltip : string,
 *      count : int,
 *      background : string,
 *      border : string,
 *      action : {
 *          entityName : string,
 *          id : string
 *      },
 *      content : string
 * }
 *
 * Пример:
 *
 * var icons = new PZone.UI.Forms.Icons(
 *     {
 *         shape: 'circle', count: 6, background: "#f472d0 url('data:image/png;base64,iVBORw0KGg...VORK5CYII=')",
 *         action: { entityName: 'new_entity', id: '373F93F7-831F-405D-94D3-836DFB9749ED' }
 *     },
 *     { shape: 'circle', border: '2px solid #fa6800', content: 'Info' },
 *     { shape: 'square', border: '2px solid#76608a', background: "url('data:image/png;base64,iVBORw0KGg...VORK5CYII=')" },
 *     { shape: 'circle', background: "#a4c400", content: 'Info' },
 *     { shape: 'square', count: 25, background: "#6d8764 url('data:image/png;base64,iVBORw0KGg...VORK5CYII=')" },
 *     { shape: 'square', background: "#1ba1e2 url('data:image/png;base64,iVBORw0KGg...VORK5CYII=')" },
 *     { background: "url('data:image/png;base64,iVBORw0KGg...VORK5CYII=')" }
 * );
 *
 * icons.show();
 *
 * icons.add({
 *     tooltip: 'New Icon',
 *     action: { entityName: 'new_entity', id: 'FC279F90-ED02-4FB8-88AF-65E8B2031C7F' },
 *     background: "url('" + Xrm.Page.context.prependOrgName('/WebResources/new_/ui/image.png') + "')"
 * });
 */
PZone.UI.Forms.Icons = function () {
    this.icons = arguments;

    var $ = jQuery;
    typeof ($) === 'undefined' && !!parent && ($ = parent.jQuery);
    if (typeof ($) === 'undefined') {
        console.log('Не удалось использовать закладок. Не удалось найти jQuery.');
        return;
    }
    
    var $icons;
    
    (function loadCss() {
        $('html > head').append([
            '<style>',
            '#PZoneIcons { color: black; text-align: right; }',
            '#PZoneIcons li { display: inline-block; width: 48px; height: 48px; margin: 0; padding: 0; list-style: none; box-sizing: border-box; margin-right: 10px; text-align: center; position: relative; background-position: center!important; background-repeat: no-repeat!important; }',
            '#PZoneIcons li.action { cursor: pointer; }',
            '#PZoneIcons li .content { font-size: 16px; line-height: 44px; background: transparent; }',
            '#PZoneIcons li .count { color: white; background: red; width: 18px; height: 18px; display: block; position: absolute; top: -4px; right: -4px; border-radius: 9px; font-size: 11px; text-align: center;  line-height: 18px; }',
            '</style>'
        ].join(''));
    })();


    /**
     * Отображение набора иконок.
     */
    this.show = function () {
        $icons = $('<ul id="PZoneIcons"></ul>');
        $icons.insertBefore('#crmFormHeader');
        for (var i = 0, len = this.icons.length; i < len; i++)
            this.add(this.icons[i]);
    };


    this.add = function(iconInfo) {
        var style = [];
        iconInfo.shape === 'circle' && style.push('border-radius: 24px');
        iconInfo.background && style.push('background: ' + iconInfo.background);
        iconInfo.border && style.push('border: ' + iconInfo.border);
        var styleHtml = style.length > 0 ? ' style="' + style.join(';') + '"' : '',
            tooltipHtml = iconInfo.tooltip ? ' title="' + iconInfo.tooltip + '"' : '',
            actionHtml = iconInfo.action ? ' onclick="Xrm.Utility.openEntityForm(\'' + iconInfo.action.entityName + '\', \'' + iconInfo.action.id + '\')"' : '',
            classHtml = actionHtml.length > 0 ? ' class="action"' : '',
            html = [];
        html.push('<li' + styleHtml + tooltipHtml + actionHtml + classHtml + '><span class="content">' + (iconInfo.content ? iconInfo.content : '&nbsp;') + '</span>');
        iconInfo.count && iconInfo.count > 0 && html.push('<span class="count">' + iconInfo.count + '</span>');
        html.push('</li>');
        $icons.append(html.join(''));
    };
};