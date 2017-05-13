ko.bindingHandlers['wysiwyg'].defaults = {
    'plugins': ['code'],
    //'toolbar': 'undo redo | bold italic | bullist numlist | link',
    //'menubar': false,
    //'statusbar': false,
    'setup': function(editor) {
        editor.on('init', function(e) {
            console.log('wysiwyg initialised');
        });
    }
};