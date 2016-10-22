(function () {

    var pluginName = 'insertimg';

    CKEDITOR.plugins.add(pluginName, {
        icons: pluginName,
        init: function (editor) {

            editor.addCommand(pluginName, new CKEDITOR.dialogCommand(pluginName + 'Dialog'));
            editor.ui.addButton(pluginName, {
                label: 'Insert Image Lighbox',
                command: pluginName,
                toolbar: 'insert'
            });

            if (editor.contextMenu) {
                editor.addMenuGroup('insertimgGroup');
                editor.addMenuItem('insertimgItem', {
                    label: 'Edit Image Lighbox',
                    icon: this.path + 'icons/insertimg.png',
                    command: 'insertimg',
                    group: 'insertimgGroup'
                });

                editor.contextMenu.addListener(function (element) {
                    if (element.getAscendant('div', true).hasClass('neufrin-img')) {
                        return { insertimgItem: CKEDITOR.TRISTATE_OFF };
                    }
                });
            }

            CKEDITOR.dialog.add(pluginName+'Dialog', this.path + 'dialog/insertimg.js');
        }
    });

})();