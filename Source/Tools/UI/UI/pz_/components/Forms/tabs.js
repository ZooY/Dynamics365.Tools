/*!
 * @version 8.0.1.0
 * @name Form Tabs Library
 */


typeof (PZone) === 'undefined' && (PZone = {});
typeof (PZone.UI) === 'undefined' && (PZone.UI = {});
typeof (PZone.UI.Forms) === 'undefined' && (PZone.UI.Forms = {});


PZone.UI.Forms.tabs = function () {
    var $ = jQuery;
    typeof ($) === 'undefined' && !!parent && ($ = parent.jQuery);
    if (typeof ($) === 'undefined') {
        console.log('Не удалось использовать закладок. Не удалось найти jQuery.');
        return;
    }
    var $tabs = $('#PZoneTabs');
    if ($tabs.length > 0)
        return;
    $('head').append([
        '<style>',
        '#PZoneTabs { border-bottom: 1px solid #a5acb5; border-left: 1px solid #DDDDDD; }',
        '#PZoneTabs li { position: relative; display: inline-block; background-color: #F4F4F4; padding: 5px 10px; border-width: 1px 1px 0 0; border-style: solid; border-color: #DDDDDD; text-transform: uppercase; cursor: pointer; }',
        '#PZoneTabs li.selected { background-color: white; }',
        '#PZoneTabs li:hover { background-color: #D7EBF9; }',
        '#crmNotifications { margin-bottom: 20px; }',
        '</style>'
    ].join(''));
    $('#formHeaderContainer').after('<ul id="PZoneTabs"></ul>');
    $tabs = $('#PZoneTabs');
    $tabs.on('click', 'li', function () {
        var $oldTab = $('#PZoneTabs li.selected'),
            oldTabName = $oldTab.attr('data-name'),
            $tab = $(this),
            tabName = $tab.attr('data-name');
        $oldTab.removeClass('selected');
        oldTabName != null && oldTabName != '' && Xrm.Page.ui.tabs.get(oldTabName).setVisible(false);
        $tab.addClass('selected');
        Xrm.Page.ui.tabs.get(tabName).setVisible(true);
        window.setTimeout(function () {
            parent.dispatchEvent(new Event('resize'));
        }, 50);
    });
    Xrm.Page.ui.tabs.forEach(function (tab, index) {
        if (tab.getVisible() === false) return;
        var tabId = 'tab' + index,
            tabName = tab.getName(),
            tabLabel = tab.getLabel();
        $tabs.append('<li data-name=' + tabName + '>' + tabLabel + '</li>');
        tab.setDisplayState('expanded');
        $('#' + tabId + ' > div.ms-crm-InlineTabHeader').hide();
        index !== 0 && tab.setVisible(false);
    });
    $tabs.children().first().addClass('selected');
}