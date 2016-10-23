(function() {
    CKEDITOR.dialog.add( 'insertimgDialog', function ( editor ) {
        return {
            title: 'Insert Image',
            minWidth: 400,
            minHeight: 200,

            contents: [
           {
               id: 'tab-basic',
               label: 'Basic Settings',
               elements: [
                   {
                       type: 'text',
                       id: 'ImgTittleField',
                       label: 'Title',

                       setup: function (element) {
                           this.setValue(element.getChild(0).getAttribute("data-title"));
                       },

                       commit: function (element) {
                           element.getChild(0).setAttribute("title", this.getValue());
                           element.getChild(0).setAttribute("data-title", this.getValue());
                           element.getChild(0).getChild(0).setAttribute("alt", this.getValue());
                           element.getChild(2).getChild(0).setText(this.getValue());
                       }
                   },
                    {
                        type: 'text',
                        id: 'ImgUrlField',
                        className: 'ImgUrlField',
                        label: 'ImgUrl',
                        validate: CKEDITOR.dialog.validate.notEmpty("Img Url field cannot be empty."),
                        setup: function (element) {
                            this.setValue(element.getChild(0).getAttribute("href"));
                        },

                        commit: function (element) {
                            element.getChild(0).setAttribute("href", this.getValue());
                        }
                    },
                    {
                        type: 'text',
                        id: 'ImgThumbnailUrlField',
                        className: 'ImgThumbnailUrlField',
                        label: 'ImgThumbnailUrl',
                        validate: CKEDITOR.dialog.validate.notEmpty("Img Thumbnail Url field cannot be empty."),
                        setup: function (element) {
                            this.setValue(element.getChild(0).getChild(0).getAttribute("src"));
                        },

                        commit: function (element) {
                            element.getChild(0).getChild(0).setAttribute("src", this.getValue());
                        }
                    },
                    {
                        type: 'text',
                        id: 'LightboxGroupField',
                        className: 'LightboxGroup',
                        label: 'LightboxGroup',
                        'default': 'gallery',
                        validate: CKEDITOR.dialog.validate.notEmpty("Lightbox Group field cannot be empty."),
                        setup: function (element) {
                            this.setValue(element.getChild(0).getAttribute("data-lightbox"));
                        },

                        commit: function (element) {
                            element.getChild(0).setAttribute("data-lightbox", this.getValue());
                            element.getChild(0).setAttribute("data-lightbox-saved", this.getValue());
                        }
                    },
                    {
                        type: 'text',
                        id: 'ClassField',
                        className: 'Classes',
                        label: 'Class',
                        'default': 'img-fluid',
                        setup: function (element) {                           
                            this.setValue(element.getChild(0).getChild(0).getAttribute("class"));
                        },

                        commit: function (element) {
                            element.getChild(0).getChild(0).setAttribute("class", this.getValue());
                        }
                    },
                    {
                        type: 'text',
                        id: 'StyleField',
                        className: 'Styles',
                        label: 'Custom CSS Styles',
                        'default': 'width: 100%;',
                        setup: function (element) {
                            this.setValue(element.getChild(0).getAttribute("style"));
                        },

                        commit: function (element) {
                            element.getChild(0).setAttribute("style", this.getValue());
                        }
                    },
                    {
                        id: "prev",
                        type: "html",
                        html: 'Preview' + '<div id="cke_lightbox_image_preview" class="ImagePreview" style="border:2px solid #000; height:100px; text-align:center; background-size:contain; background-position:center center; background-repeat:no-repeat;"></div>'
                    },
                    {
                        type: 'button',
                        id: 'selectImgId',
                        label: 'Select Image',
                        title: 'Select',
                        onClick: function () {
                            // this = CKEDITOR.ui.dialog.button
                            $('#selectImg').modal('show');
                            
                        }
                    },
                     {
                         type: 'button',
                         id: 'setPreviewId',
                         label: 'Set Preview',
                         title: 'Set Preview',
                         onClick: function () {
                             // this = CKEDITOR.ui.dialog.button
                             document.getElementById('cke_lightbox_image_preview').style.backgroundSize = "contain";
                             document.getElementById('cke_lightbox_image_preview').style.backgroundImage = "url('" + $('.ImgThumbnailUrlField').find('input').val() + "')";

                         }
                     }
               ]
           }
            ],
            onShow: function () {
                var dialog = this;
                var selection = editor.getSelection();
                var element = selection.getStartElement();

                if (element)
                    element = element.getAscendant('div', true);

                if (!element || element.getName() != 'div') {
                    var html = '<div class="neufrin-img card" ' +
                        'style="' +
                        dialog.getValueOf('tab-basic', 'StyleField') +
                        '"><a  data-lightbox="' +
                        dialog.getValueOf('tab-basic', 'LightboxGroupField') +
                        '" data-lightbox-saved="' +
                        dialog.getValueOf('tab-basic', 'LightboxGroupField') +
                        '" data-title="' +
                        dialog.getValueOf('tab-basic', 'ImgTittleField') +
                        '" ' +
                        'href="' +
                        dialog.getValueOf('tab-basic', 'ImgUrlField') +
                        '" ' +
                        'title="' +
                        dialog.getValueOf('tab-basic', 'ImgTittleField') +
                        '"><img alt="' +
                        dialog.getValueOf('tab-basic', 'ImgTittleField') +
                        '" ' +
                        'class="' +
                        dialog.getValueOf('tab-basic', 'ClassField') +
                        '" ' +
                        'src="' +
                        dialog.getValueOf('tab-basic', 'ImgThumbnailUrlField') +
                        '" style="width:100%;" /></a>' +
                        ' <div class="img-card card-block">' +
                        '<p class="card-text">' +
                        dialog.getValueOf('tab-basic', 'ImgTittleField') +
                        '</p></div></div>';
                    element = CKEDITOR.dom.element.createFromHtml(html);
                    this.insertMode = true;
                }
                else
                    this.insertMode = false;

                this.element = element;
                if (!this.insertMode)
                    this.setupContent(this.element);

               
            },
            onOk: function () {
                var dialog = this;
                var insertimg = this.element;
                this.commitContent(insertimg);
                if (this.insertMode)
                    editor.insertElement(insertimg);
            }
    };
    });
})();