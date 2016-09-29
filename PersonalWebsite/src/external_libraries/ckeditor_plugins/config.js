/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.stylesSet.add('default2', [

    { name: 'Italic Title', element: 'h2', styles: { 'font-style': 'italic' } },
    { name: 'Subtitle', element: 'h3', styles: { 'color': '#aaa', 'font-style': 'italic' } },
    {
        name: 'Special Container',
        element: 'div',
        styles: {
            padding: '5px 10px',
            background: '#eee',
            border: '1px solid #ccc'
        }
    },


    { name: 'Marker', element: 'span', attributes: { 'class': 'marker' } },

    { name: 'Big', element: 'big' },
    { name: 'Small', element: 'small' },
    { name: 'Typewriter', element: 'tt' },

    { name: 'Computer Code', element: 'code' },
    { name: 'Keyboard Phrase', element: 'kbd' },
    { name: 'Sample Text', element: 'samp' },
    { name: 'Variable', element: 'var' },

    { name: 'Deleted Text', element: 'del' },
    { name: 'Inserted Text', element: 'ins' },

    { name: 'Cited Work', element: 'cite' },
    { name: 'Inline Quotation', element: 'q' },

    { name: 'Language: RTL', element: 'span', attributes: { 'dir': 'rtl' } },
    { name: 'Language: LTR', element: 'span', attributes: { 'dir': 'ltr' } },

    /* Object Styles */

    {
        name: 'Styled image (left)',
        element: 'img',
        attributes: { 'class': 'left' }
    },

    {
        name: 'Styled image (right)',
        element: 'img',
        attributes: { 'class': 'right' }
    },
    {
        name: 'Responsive image',
        element: 'img',
        attributes: { 'class': 'img-fluid' }
    },

    {
        name: 'Compact table',
        element: 'table',
        attributes: {
            cellpadding: '5',
            cellspacing: '0',
            border: '1',
            bordercolor: '#ccc'
        },
        styles: {
            'border-collapse': 'collapse'
        }
    },

    { name: 'Borderless Table', element: 'table', styles: { 'border-style': 'hidden', 'background-color': '#E6E6FA' } },
    { name: 'Square Bulleted List', element: 'ul', styles: { 'list-style-type': 'square' } }
]);

CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [
        { name: 'clipboard', groups: ['clipboard', 'undo'] },
        { name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
        { name: 'links', groups: ['lightbox'] },
        { name: 'insert' },
        { name: 'forms' },
        { name: 'tools' },
        { name: 'document', groups: ['mode', 'document', 'doctools'] },
        { name: 'others' },
        '/',
        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
        { name: 'styles' },
        { name: 'colors' },
        { name: 'about' },
        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
    ];


    config.extraPlugins = 'codesnippet,lineutils,widget,prism,lightbox';

    config.extraAllowedContent = 'a[data-lightbox,data-title,data-lightbox-saved]';
    config.stylesSet = 'default2'
    config.allowedContent = true;
    config.tabSpaces = 4;
    config.codeSnippet_languages = {
        csharp: 'csharp',
    };
    config.codeSnippet_theme = 'okaidia';
};
