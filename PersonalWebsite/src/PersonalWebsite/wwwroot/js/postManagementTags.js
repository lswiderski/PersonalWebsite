function PostTagScope() {
    var self = this;
    this.Init = function () {
        $('#submit-button').click(function () {
            self.PassTags();

            $('#post-form').submit();
        });
        $('#tag-input').on('keydown', function (e) {
            if (e.which == 13) {
                e.preventDefault();
                self.TransformTags();
            }
        });
    };

    this.TransformTags = function () {
        var inputField = $('#tag-input');
        var listOfTags = $('#tag-list');

        //split new tags
        var tags = inputField.val().split(",")

        //create new tag items
        if (tags != [""]) {
            for (var i = 0; i < tags.length; i++) {
                {
                    var newTag = tags[i].replace(/[^a-z\d]/gi, "").replace(/ /g, "").trim();

                    var newTagSpan = '<span class="tag tag-primary" data-tagname="' + newTag + '"><a href="#" class="tag-remove-button" id="tag-' + newTag + '" onclick="PostTag.RemoveTag(\'tag-' + newTag + '\')"><i class="fa fa-times" aria-hidden="true"></i></a>' + newTag + '</span>';
                    listOfTags.append(newTagSpan);
                }
            }

            //clear input
            $('#tag-input').val("");
        }
    }

    this.PassTags = function () {
        var modelTags = $('#Tags')

        var tagList = $('#tag-list').children();

        for (var i = 0; i < tagList.length; i++) {
            var value = tagList[i].dataset['tagname'];
            var customTag = '<input type="hidden" name="Tags[' + i + ']" value="' + value + '">'
            modelTags.append(customTag);
        }
    }
    this.RemoveTag = function (id) {
        $('#' + id).closest('.tag').remove();
    }
}